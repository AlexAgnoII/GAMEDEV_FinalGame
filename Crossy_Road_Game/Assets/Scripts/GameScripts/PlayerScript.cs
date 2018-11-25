using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

    private Animator playerAnimator;
    private Vector3 movement = new Vector3(0,0,0);
    private bool isHopping = false;
    private const string HOP_KEY = "Hop";


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update () {

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
        

        //Broadcast to terrain generator.
        Parameters parameters = new Parameters();
        parameters.PutExtra(EventNames.FinalGameEvents.PARAM_PLAYER_X_POSITION, this.transform.position.x);

        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD, parameters);
    }

    private void CharacterRotate(float angle)
    {
        transform.DORotate(new Vector3(0, angle, 0), 0.1f);
    }

    public void FinishedHop()
    {
        isHopping = false;
    }

}
