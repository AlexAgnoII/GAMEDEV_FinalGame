using System.Collections;
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
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_DIFFICULTY_CHANGE, this.ChangeDifficulty);

        //Create first set of terrains.
        for (int i = 0; i < MAX_TERRAIN_COUNT; i++)
        {
            SpawnTerrain(null);
        }
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_PLAYER_MOVE_FORWARD);
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_DIFFICULTY_CHANGE);
    }

    private void ChangeDifficulty()
    {
        // current function spawn = next difficulty function spawn.
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
            GameObject terrain, terrainRemove;
            int terrainIndex = Random.Range(0, terrainData_List.Count);
            int terrainInSuccession = Random.Range(terrainData_List[terrainIndex].getMinInSuccession(), 
                                                   terrainData_List[terrainIndex].getMaxInSuccession());

            List<GameObject> terrainKinds = terrainData_List[terrainIndex].getTerrainKinds();
            for (int i = 0; i < terrainInSuccession; i++)
            {

                terrain = Instantiate(terrainKinds[Random.Range(0, terrainKinds.Count)], 
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
