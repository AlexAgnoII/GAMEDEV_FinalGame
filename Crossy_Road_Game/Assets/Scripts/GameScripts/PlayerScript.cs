using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private AudioClip hopSound;
    private Animator playerAnimator;
    private AudioSource audioSource;
    private Vector3 movement;
    private bool isHopping;
    private const string HOP_KEY = "Hop";
    private bool isAlive;
    private bool once;
        
    

    private void Start()
    {
        once = true;
        isAlive = true;
        movement = new Vector3(0, 0, 0);
        isHopping = false;

        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        transform.DOMove(transform.position + nextLocation, 0.1f).SetEase(Ease.Flash);
        playHopSound();
        

        //Broadcast to terrain generator.
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.FinalGameEvents.PARAM_PLAYER_X_POSITION, this.transform.position.x);

        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD, parameters);
    }

    private void CharacterRotate(int angle)
    {
        transform.DORotate(new Vector3(0, angle, 0), 0.1f);
    }

    public void FinishedHop()
    {
        isHopping = false;
    }

    private void playHopSound()
    {
        audioSource.clip = hopSound;
        audioSource.Play();
    }

    //for some reason OnCollision enter rarely works, so this will do.
    private void OnTriggerEnter(Collider other)
    {
        //disable camera movement + player movement.
        this.isAlive = false;
        
    }

    public bool getIfAlive()
    {
        return isAlive;
    }


}
