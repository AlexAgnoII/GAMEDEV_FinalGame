﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    [SerializeField] private GameObject parentLocation;
    [SerializeField] private int MAX_TERRAIN_COUNT = 30;
    [SerializeField] private int minDistanceFromPlayer = 15; //min distance must always be half of max_terrain_count to ensure that player is always at middle.
    [SerializeField] private List<TerrainData> terrainData_List = new List<TerrainData>();
    [SerializeField] private Queue<GameObject> currentTerrainsQueue = new Queue<GameObject>();

    private Vector3 currentPosition = new Vector3(1, 0, 0);

    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD, this.SpawnTerrain);

        //Create first set of terrains.
        for (int i = 0; i < MAX_TERRAIN_COUNT; i++)
        {
            SpawnTerrain(null);
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD);
    }

    //Fixed starting terrain when the user just spawned.
    private void spawnStartingTerrain()
    {

    }

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
            GameObject terrain, terrainRemove;
            int terrainIndex = Random.Range(0, terrainData_List.Count);
            int terrainInSuccession = Random.Range(terrainData_List[terrainIndex].getMinInSuccession(), 
                                                   terrainData_List[terrainIndex].getMaxInSuccession());

        
            for(int i = 0; i < terrainInSuccession; i++)
            {
                terrain = Instantiate(terrainData_List[terrainIndex].getTerrain(), 
                                          currentPosition, 
                                          Quaternion.identity, 
                                          parentLocation.transform);
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

    }
}
