using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {
    private int score = 0;
    private float timer = 4.0f;
    [SerializeField] private Text textScore;
    [SerializeField] private Text timeText;
    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_UPDATE_SCORE, this.iterateScore);
    }
    void Destroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_UPDATE_SCORE);
    }

    // Update is called once per frame
    void Update () {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timeText.text = "" + (int)timer;
        }
        else
        {
            timeText.text = "";
            Parameters updateLineParams = new Parameters();
            updateLineParams.PutExtra(EventNames.ON_TIMER_DONE, 1);
            EventBroadcaster.Instance.PostEvent(EventNames.ON_TIMER_DONE, updateLineParams);
        }
    }

    void iterateScore()
    {
        score++;
        textScore.text = "SCORE: " + score;
    }
}
