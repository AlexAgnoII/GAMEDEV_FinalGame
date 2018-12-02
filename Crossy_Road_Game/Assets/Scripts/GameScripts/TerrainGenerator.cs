﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    [SerializeField] private GameObject parentLocation;
    [SerializeField] private int MAX_TERRAIN_COUNT; //60
    [SerializeField] private int minDistanceFromPlayer; //30 //min distance must always be half of max_terrain_count to ensure that player is always at middle.
    [SerializeField] private List<TerrainData> terrainData_List = new List<TerrainData>();
    private Queue<GameObject> currentTerrainsQueue = new Queue<GameObject>();

    private Vector3 currentPosition = new Vector3(1, 0, 0);

    private const int LEVEL_1_REQ_STEPS = 10; //20
    private const int LEVEL_2_REQ_STEPS = 20; //40
    private const int LEVEL_3_REQ_STEPS = 40; //60
    private const int LEVEL_4_REQ_STEPS = 60; //80
    private const int LEVEL_5_REQ_STEPS = 80; //100

    private const int LEVEL_0 = 0;
    private const int LEVEL_1 = 1;
    private const int LEVEL_2 = 2;
    private const int LEVEL_3 = 3;
    private const int LEVEL_4 = 4;
    private const int LEVEL_5 = 5;

    private int currentDifficulty = 0; //0 - 5
                                                     //Game default (For reference lang, this will be our super default settings.
    [SerializeField] private float additive_speed;   //3
    [SerializeField] private float movingSpeed;      //3
    [SerializeField] private float supa_fast_boi;    //50
    [SerializeField] private float time_reduction;   //0.5
    [SerializeField] private float maxSpawnTime;     //5
    [SerializeField] private float minSpawnTime;     //3

    private bool hasFastBoi = false;
    private float newMaxSpeed;  
    private float newMaxSpawnTime;
    private float newMinSpawnTime;


    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD, this.SpawnTerrain);
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_SEND_CURRENT_STEPS, this.checkForNextDifficulty);

        newMinSpawnTime = minSpawnTime;
        newMaxSpeed = speedModifier(1);
        newMaxSpawnTime = this.maxSpawnTime;

       
        //Create first set of terrains.
        for (int i = 0; i < MAX_TERRAIN_COUNT; i++)
        {
            SpawnTerrain(null);
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_SEND_CURRENT_STEPS);
    }


    //this function will serve as the HARDEST TERRAIN GENERATOR EVER.
    public void SpawnTerrain(Parameters param)
    {
        float playerXPos = 0;
        bool isStart = true;

        if(param != null)
        {
            playerXPos = param.GetFloatExtra(EventNames.FinalGameEvents.PARAM_PLAYER_X_POSITION, 0);
            isStart = false;
        }
        
        //When you're half a distance away from the spawning point, start creating and deleting terrains.
        if (currentPosition.x - playerXPos < minDistanceFromPlayer) {
            int terrainIndex;

            switch(currentDifficulty)
            {
                case LEVEL_1: terrainIndex = Random.Range(0, terrainData_List.Count);
                              Level_X_TerrainSpawner(1, 1, terrainIndex, isStart);
                              break;

                case LEVEL_2: terrainIndex = Random.Range(0, terrainData_List.Count);
                              Level_X_TerrainSpawner(1, 2, terrainIndex, isStart);
                              break;

                case LEVEL_3: terrainIndex = Random.Range(0, terrainData_List.Count);
                              Level_X_TerrainSpawner(2, 3, terrainIndex, isStart);
                              break;
                case LEVEL_4: 
                case LEVEL_5: terrainIndex = Random.Range(0, terrainData_List.Count);
                              Level_X_TerrainSpawner(terrainData_List[terrainIndex].getMaxInSuccession(),
                                                    terrainData_List[terrainIndex].getMinInSuccession(),
                                                    terrainIndex,
                                                    isStart);
                              break;
                //first 20 steps
                default: terrainIndex = Random.Range(0, terrainData_List.Count - 1);
                         Level_X_TerrainSpawner(1, 1, terrainIndex , isStart);
                         break;
            }
        }
    }


    private void Level_X_TerrainSpawner(int minSucession, int maxSuccession,int terrainIndex, bool isStart)
    {
        GameObject terrain, terrainRemove;
        int terrainInSuccession = Random.Range(minSucession, maxSuccession);

        List<GameObject> terrainKinds = terrainData_List[terrainIndex].getTerrainKinds();
        for (int i = 0; i < terrainInSuccession; i++)
        {

            terrain = Instantiate(terrainKinds[Random.Range(0, terrainKinds.Count)],
                                      currentPosition,
                                      Quaternion.identity,
                                      parentLocation.transform);

            if (terrain.tag == PrefabTags.TerrainGroup.NORTH_BOUND_ROAD ||
               terrain.tag == PrefabTags.TerrainGroup.SOUTH_BOUND_ROAD)
            {
                float superRare = 2.0f;
                float speed_wish = this.newMaxSpeed;
                if(hasFastBoi && Random.Range(0, 100) < superRare)
                {
                    speed_wish = this.supa_fast_boi;
                }
                terrain.GetComponent<VehicleSpawner>().setVehiclesData(this.minSpawnTime,
                                                                       this.newMaxSpawnTime,
                                                                       Random.Range(this.movingSpeed, speed_wish));
            }
            else if (terrain.tag == PrefabTags.TerrainGroup.NORTH_BOUND_WATER ||
                    terrain.tag == PrefabTags.TerrainGroup.SOUTH_BOUND_WATER)
            {
                terrain.GetComponent<LogSpawner>().setLogData(this.minSpawnTime,
                                                              this.newMaxSpawnTime,
                                                              Random.Range(this.movingSpeed, this.newMaxSpeed));
            }

            currentTerrainsQueue.Enqueue(terrain);

            //If the current terrain spawned exceeds the max terrain count, remove the oldest.
            if (!isStart && currentTerrainsQueue.Count > MAX_TERRAIN_COUNT)
            {
                terrainRemove = currentTerrainsQueue.Dequeue();
                Destroy(terrainRemove);
            }

            currentPosition.x++;
        }
    }

    private void checkForNextDifficulty(Parameters param)
    {
        int currScore = param.GetIntExtra(EventNames.FinalGameEvents.PARAM_PLAYER_SCORE, -1);

        if (currScore >= LEVEL_1_REQ_STEPS)
        {
            increaseDifficulty(currScore);
        }

    }

    private void increaseDifficulty(int currScore)
    {
        switch (currScore)
        {
            case LEVEL_1_REQ_STEPS:
                Debug.Log("Level 1 reached");
                level_1_difficulty();
                break;

            case LEVEL_2_REQ_STEPS:
                Debug.Log("Level 2 reached");
                level_2_difficulty();
                break;

            case LEVEL_3_REQ_STEPS:
                Debug.Log("Level 3 reached");
                level_3_difficulty();
                break;

            case LEVEL_4_REQ_STEPS:
                Debug.Log("Level 4 reached");
                level_4_difficulty();
                break;

            case LEVEL_5_REQ_STEPS:
                Debug.Log("Level 5 reached");
                level_5_difficulty();
                break;
            default: break; //default level (0-19)
        }

        Debug.Log("Current Max speed: " + this.newMaxSpeed);
        Debug.Log("Current Max timeSpawn: " + this.newMaxSpawnTime);
        Debug.Log("Current Min timeSpawn: " + this.newMinSpawnTime);
    }

    private void level_1_difficulty()
    {
        this.increaseCurrentDifficultyValue();
    }

    private void level_2_difficulty()
    {
        this.increaseCurrentDifficultyValue();
        this.newMaxSpawnTime = this.maxTimeModifier(1);
        //this.newMaxSpeed = this.speedModifier(1);

    }

    private void level_3_difficulty()
    {
        this.increaseCurrentDifficultyValue();
        this.newMaxSpawnTime = this.maxTimeModifier(1);
        this.newMaxSpeed = this.speedModifier(2);
    }

    private void level_4_difficulty()
    {
        this.increaseCurrentDifficultyValue();
        this.newMaxSpawnTime = this.maxTimeModifier(2);
        this.newMaxSpeed = this.speedModifier(2);
    }

    private void level_5_difficulty()
    {
        this.increaseCurrentDifficultyValue();
        this.newMaxSpawnTime = this.maxTimeModifier(2);
        this.newMaxSpeed = this.speedModifier(2);
        this.hasFastBoi = true;
    }

    private float speedModifier(float multiplier)
    {
        return this.movingSpeed + (additive_speed * multiplier);
    }

    private float maxTimeModifier(float multiplier)
    {
        return this.maxSpawnTime - (time_reduction * multiplier);
    }

    private float minTimeModifier(float multiplier)
    {
        return this.minSpawnTime - (time_reduction * multiplier);
    }

    private int increaseCurrentDifficultyValue()
    {
        return this.currentDifficulty++;
    }

}
