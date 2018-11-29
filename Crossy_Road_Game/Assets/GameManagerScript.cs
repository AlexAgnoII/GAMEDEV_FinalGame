using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    private const float STEPS_REQUIRED_FOR_NEXT_DIFFICULTY = 30.0f; //required steps to increase difficulties.
    private float steps_multiplier = 1.0f; //multiplier for calculating nex difficulty e.g. 30 = 1, 60 = 2 etc.

    private float speed_multiplier = 1.0f;
    private float time_multiplier = 1.0f;
    [SerializeField] private float maxCarSpeed;
    [SerializeField] private float minCarSpeed;
    [SerializeField] private float maxLogSpeed;
    [SerializeField] private float minLogSpeed;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float minSpawnTime;

	
}
