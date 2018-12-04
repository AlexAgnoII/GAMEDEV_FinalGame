using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {
    private float timer = 6.0f; //Default: 4.0f
    [SerializeField] private Text textScore;
    [SerializeField] private Text timeText;

    private bool once;

    // Use this for initialization
    void Start () {
        once = true;
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_UPDATE_SCORE, this.iterateScore);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_GAME_END, this.showGameOver);
        
    }
    
    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_UPDATE_SCORE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_GAME_END);
    }

    // Update is called once per frame
    void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;


            if (timer > 4.0f)
            {
                timeText.text = "Use WASD/Arrow keys to move Hamsuke!";
            }  else timeText.text = "" + (int)timer;

            // else if (timer > 4.0f && timer < 7.0f)
            // {
            //     timeText.text = "Get to the other side! Dodge All the Cars and Jump on the Logs";
            // }
            // else timeText.text = "Use WASD/Arrow keys to move Hamsuke to get as far as you can!";// + (int)timer;



            if (timer-1 < 0)
            {
                timeText.text = "GO!!";
                
            }
        }
        else
        {
            timeText.text = "";
            if (once) {
                EventBroadcaster.Instance.PostEvent(EventNames.ON_TIMER_DONE);
                once = !once;
            }
        }
    }

    private void showGameOver()
    {
        ViewHandler.Instance.Show("gameOverScreen", true);
    }

    void iterateScore(Parameters param)
    {
        int score = param.GetIntExtra(EventNames.FinalGameEvents.PARAM_PLAYER_SCORE, -1);
        textScore.text = "SCORE: " + score;
    }
}
