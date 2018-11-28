using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {
    private int score = 0;
    [SerializeField] private Text textScore;

    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_UPDATE_SCORE, this.iterateScore);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void iterateScore()
    {
        score++;
        textScore.text = "SCORE: " + score;
    }
}
