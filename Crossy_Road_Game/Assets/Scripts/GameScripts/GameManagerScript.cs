using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    private const int LEVEL_1_REQ_STEPS = 20;
    private const int LEVEL_2_REQ_STEPS = 40;
    private const int LEVEL_3_REQ_STEPS = 60;
    private const int LEVEL_4_REQ_STEPS = 80;
    private const int LEVEL_5_REQ_STEPS = 100;

    [SerializeField] private float multiplier = 1.0f;
    [SerializeField] private float additive_speed = 3.0f;
    [SerializeField] private float movingSpeed = 3.0f;
    [SerializeField] private float supa_fast_boi = 50f;
    [SerializeField] private float time_reduction = 0.5f;
    [SerializeField] private float maxSpawnTime = 5.0f;
    [SerializeField] private float minSpawnTime = 2.0f;


    private void Start()
    {
        //set observer..
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_DIFFICULTY_CHANGE, this.checkForNextDifficulty);
    }

    private void OnDestroy()
    {
        //destroy observers..
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_DIFFICULTY_CHANGE);
    }

    private void checkForNextDifficulty(Parameters param)
    {
        int currentSteps = (int) param.GetFloatExtra(EventNames.FinalGameEvents.ON_SEND_CURRENT_STEPS, -1.0f);

        if(currentSteps >= LEVEL_1_REQ_STEPS)
        {
            increaseDifficulty(currentSteps);
        }
        
    }

    private void increaseDifficulty(int steps)
    {
        switch(steps)
        {
            case LEVEL_1_REQ_STEPS: break;
            case LEVEL_2_REQ_STEPS: break;
            case LEVEL_3_REQ_STEPS: break;
            case LEVEL_4_REQ_STEPS: break;
            case LEVEL_5_REQ_STEPS: break;
            default: break; //default level (0-19)
        }
    }

    private void increaseMultiplier()
    {
        this.multiplier += 1.0f;
    }

    private float speedModifier()
    {
        return this.movingSpeed + (additive_speed * this.multiplier);
    }

    private float timeModifier()
    {
        return this.maxSpawnTime - (time_reduction * this.multiplier);
    }

    private void SendDIfficultyChange()
    {

    }


}
