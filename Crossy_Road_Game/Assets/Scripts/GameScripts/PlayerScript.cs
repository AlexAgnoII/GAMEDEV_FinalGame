using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private AudioClip hopSound;
    [SerializeField] private float tweenSpeed;
    [SerializeField] private float player_max_z_before_death = 16.0f;
    [SerializeField] private Transform playerHolderTransform;
    private Animator playerAnimator;
    private AudioSource audioSource;
    private SphereCollider myCollider;
    private Vector3 movement;
    private bool isHopping;
    private const string HOP_KEY = "Hop";
    private bool isAlive;
    private bool once;
    private bool onceTrigger;
    private bool killedByVehicle;
    private float maxX;
    private bool canPlay;


    private void Start()
    {
        player_max_z_before_death = 16.0f;
        once = true;
        onceTrigger = true;
        isAlive = true;
        movement = new Vector3(0, 0, 0);
        isHopping = false;
        killedByVehicle = false;
        canPlay = false;
        maxX = 0.0f;

        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<SphereCollider>();
        this.GetComponent<SphereCollider>().enabled = true;
        this.GetComponent<Rigidbody>().useGravity = true;

        EventBroadcaster.Instance.AddObserver(EventNames.ON_TIMER_DONE, this.AllowPlayerToPlay);
        /* EventBroadcaster.Instance.AddObserver(EventNames.ON_PAUSE_NAME, this.DisallowPlayerToPlay);
         EventBroadcaster.Instance.AddObserver(EventNames.ON_RESUME_NAME, this.AllowPlayerToPlay);*/ //depracated for now.

    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_TIMER_DONE);
        /*EventBroadcaster.Instance.RemoveObserver(EventNames.ON_PAUSE_NAME);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_RESUME_NAME);*/ //depracated for now.
    }

    // Update is called once per frame
    void Update () {

        //If went outofbounds with log, dead.
        if(this.transform.position.z < -this.player_max_z_before_death ||
            this.transform.position.z > this.player_max_z_before_death)
        {
            this.isAlive = false;
        }

        if(this.isAlive && this.canPlay)
        {
            if(Input.GetKeyDown(KeyCode.W) && !isHopping)
            {
                CharacterMove(new Vector3(1, 0, genWholeNumPosition(transform.position.z)));
                CharacterRotate(0);
            }
            else if (Input.GetKeyDown(KeyCode.S) && !isHopping)
            {
                CharacterMove(new Vector3(-1, 0, genWholeNumPosition(transform.position.z)));
                CharacterRotate(180);
            }
            else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
            {
                CharacterMove(new Vector3(0, 0, 1));
                CharacterRotate(-90);
            }
            else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
            {
                CharacterMove(new Vector3(0, 0, -1));
                CharacterRotate(90);
            }
        }
    }


    //sends a signal to whomever will receive it to update score.
    private void onMoveTweenFinish()
    {
        //get score and pass to UI via broadcast.
        if(maxX < Math.Floor(this.transform.position.x) && maxX >= 0)
        {
            maxX = (float) Math.Floor(this.transform.position.x);

            Parameters param = new Parameters();
            param.PutExtra(EventNames.FinalGameEvents.PARAM_PLAYER_SCORE, (int) maxX);

            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_UPDATE_SCORE, param);
            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_SEND_CURRENT_STEPS, param);
        }

        //play sound of hop.
        
    }

    private void FixedUpdate()
    {

        //Player DEATH
        if(!isAlive && once)
        {
            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_DEATH_SOUND);
            this.GetComponent<Rigidbody>().useGravity = false;
            this.transform.DOScale(new Vector3(0, 0, 0), 0.1f).OnComplete(explodePlayer);
            once = false;

            StartCoroutine(this.ConfirmIAmDead());
        }
    }

    private IEnumerator ConfirmIAmDead()
    {
        yield return new WaitForSeconds(1.5f);
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_GAME_END);
    }

    private void explodePlayer()
    {
        //since player scales down to 0
        //Make rigid body gravity false
        //disable collider. (thiss enures particle effect will work perfectly without hitting player's collider.
        this.GetComponent<SphereCollider>().enabled = false;

        if (killedByVehicle)
            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_EXPLOD_FROM_CAR);
        else
            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_SPLASH_FROM_WATER);
    }

    private float genWholeNumPosition(float notWholeZ)
    {
        float wholeZ = 0;
        if (transform.position.z % 1 != 0)
        {
            wholeZ = Mathf.Round(notWholeZ) - notWholeZ;
        }

        return wholeZ;
    }

    private void CharacterMove(Vector3 nextLocation)
    {
        playerAnimator.SetTrigger(HOP_KEY);
        isHopping = true;
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_HOPPING_SOUND);
        //check if there is collision from a fixed obstacle infront of user.
        if (!hasObstacleInFront(nextLocation))
            transform.DOMove(transform.position + nextLocation, tweenSpeed).SetEase(Ease.Flash).OnComplete(onMoveTweenFinish);
        
        //Broadcast to terrain generator.
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.FinalGameEvents.PARAM_PLAYER_X_POSITION, this.transform.position.x);

        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD, parameters);
    }

    private bool hasObstacleInFront(Vector3 nextLocation)
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, 
                           nextLocation, out hit, 
                           1.0f)) 
        {

            if(string.Equals(hit.collider.tag, PrefabTags.FixedObstacles.FIXED_OBSTACLES))
                return true;
        }

        return false;
    }

    private void CharacterRotate(int angle)
    {
        transform.DORotate(new Vector3(0, angle, 0), tweenSpeed);
    }

    //for animation event.
    public void FinishedHop()
    {
        this.myCollider.center = new Vector3(0, -0.02f, 0);
    }

    //for animation event.
    public void JumpPeak()
    {
        this.myCollider.center = new Vector3(0, 1.0f, 0);
    }

    private void playHopSound()
    {
        audioSource.clip = hopSound;
        audioSource.Play();
    }

    //for some reason OnCollision enter rarely works, so this will do.
    private void OnTriggerEnter(Collider other)
    {
        if(onceTrigger)
        {
            onceTrigger = !onceTrigger;

            switch (other.tag)
            {
                
                case "VEHICLE": EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_CRASH_SOUND);
                    killedByVehicle = true;
                    break;
                case "NORTH_BOUND_WATER":
                case "SOUTH_BOUND_WATER":
                    EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_SPLASH_SOUND);
                    killedByVehicle = false;
                    this.transform.parent = this.playerHolderTransform;
                    break;
            }
            //disable camera movement + player movement.
            this.isAlive = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //On log.
        if(string.Equals(collision.collider.tag, PrefabTags.MovingObstacles.LOG))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_LOG_SOUND);
            this.transform.parent = collision.collider.transform;
        }
        else
        {
            this.transform.parent = this.playerHolderTransform;

            //on grass
            if (string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.GRASS))
            {
                //grass texture sound.
                EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_GRASS_SOUND);
            }

            //on any road.
            else if (string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.NORTH_BOUND_ROAD) ||
                    string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_ROAD))
            {
                //road texture sound
                EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_ROAD_SOUND);
            }
        }

        //only allow hopping IF user lands. NOT when animation is finished.
        isHopping = false;
    }

    private void AllowPlayerToPlay()
    {
        this.canPlay = true;
    }

    private void DisallowPlayerToPlay()
    {
        this.canPlay = false;
    }


    public bool getIfAlive()
    {
        return isAlive;
    }


}
