using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private AudioClip hopSound;
    [SerializeField] private float tweenSpeed;
    private Animator playerAnimator;
    private AudioSource audioSource;
    private SphereCollider myCollider;
    private Vector3 movement;
    private bool isHopping;
    private const string HOP_KEY = "Hop";
    private bool isAlive;
    private bool once;
    private bool onceTrigger;
    private float maxX;


    private void Start()
    {
        
        once = true;
        onceTrigger = true;
        isAlive = true;
        movement = new Vector3(0, 0, 0);
        isHopping = false;
        maxX = 0.0f;

        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        myCollider = GetComponent<SphereCollider>();

    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update () {
        if(isAlive)
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
            Debug.Log("Score: " + maxX);
        }

        //play sound of hop.
    }

    private void FixedUpdate()
    {
        
        
        if(!isAlive && once)
        {
            this.transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(explodePlayer);


            once = false;
        }
    }

    private void explodePlayer()
    {
        Debug.Log("BOOM");
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

        //check if there is collision from a fixed obstacle infront of user.
        if(!hasObstacleInFront(nextLocation))
            transform.DOMove(transform.position + nextLocation, tweenSpeed).SetEase(Ease.Flash).OnComplete(onMoveTweenFinish);
        
        playHopSound();
        

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


            Debug.Log("Killed by: " + other.tag);
            //disable camera movement + player movement.
            this.isAlive = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //On log.
        if(string.Equals(collision.collider.tag, PrefabTags.MovingObstacles.LOG))
        {
            this.transform.parent = collision.collider.transform;


            Debug.Log("Log sound?");
        }
        else
        {
            this.transform.parent = null;

            //on grass
            if (string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.GRASS))
            {
                //grass texture sound.
                Debug.Log("grass sound?");
            }

            //on any road.
            else if (string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.NORTH_BOUND_ROAD) ||
                    string.Equals(collision.collider.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_ROAD))
            {
                //road texture sound
                Debug.Log("road sound?");
            }
        }

        //only allow hopping IF user lands. NOT when animation is finished.
        isHopping = false;
    }




    public bool getIfAlive()
    {
        return isAlive;
    }


}
