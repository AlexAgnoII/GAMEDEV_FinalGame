using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {
    private float timer = 4.0f;
    [SerializeField] private Text textScore;
    [SerializeField] private Text timeText;
    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_UPDATE_SCORE, this.iterateScore);
    }
    void Destroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_UPDATE_SCORE);
    }

    // Update is called once per frame
    void Update () {
        if (timer > 0)
        {
            Debug.Log(timer);
            timer -= Time.deltaTime;
            timeText.text = "" + (int)timer;
            if (timer-1 < 0)
            {
                timeText.text = "GO!!";
                EventBroadcaster.Instance.PostEvent(EventNames.ON_TIMER_DONE);
            }
        }
        else
        {
            timeText.text = "";
        }
    }

    void iterateScore(Parameters param)
    {
        timeText.text = "";
        int score = param.GetIntExtra(EventNames.FinalGameEvents.PARAM_PLAYER_SCORE, -1);
        textScore.text = "SCORE: " + score;
    }
}
