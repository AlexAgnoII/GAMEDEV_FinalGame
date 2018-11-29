using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    private const float STEPS_REQUIRED_FOR_NEXT_DIFFICULTY = 20.0f; //required steps to increase difficulties.
    private float steps_multiplier = 1.0f; //multiplier for calculating nex difficulty e.g. 30 = 1, 60 = 2 etc.

    [SerializeField] private float additive_speed = 3.0f;
    [SerializeField] private float movingSpeed = 3.0f;
    [SerializeField] private float supa_fast_boi = 50f;
    [SerializeField] private float time_reduction = 0.5f;
    [SerializeField] private float maxSpawnTime = 5.0f;
    [SerializeField] private float minSpawnTime = 2.0f;


    private void Start()
    {
        //set observer..
    }

    private void OnDestroy()
    {
        //destroy observers..
    }

    private void increaseDifficulty()
    {
        //has to be a range since we're dealing with a float value.
        //if >= 20 <=39
        //

        //if >= 40 <= 59


        //if >= 60 <= 79


        //if >= 80 <= 99


        //if 100+

    }


    private void SendDIfficultyChange()
    {

    }


}
