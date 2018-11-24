using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    [SerializeField] private GameObject parentLocation;
    [SerializeField] private const int MAX_TERRAIN_COUNT = 20;    
    [SerializeField] private List<TerrainData> terrainData_List = new List<TerrainData>();
    [SerializeField] private Queue<GameObject> currentTerrainsQueue = new Queue<GameObject>();
    

    private Vector3 currentPosition = new Vector3(0, 0, 0);



    
    void Start () {

        /*for(int i = 0; i < MAX_TERRAIN_COUNT; i++)
        {
            SpawnTerrain();
        }*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SpawnTerrain();
        }
    }

    private void SpawnTerrain()
    {
        GameObject terrain, terrainRemove;
        int terrainIndex = Random.Range(0, terrainData_List.Count);
        int terrainInSuccession = Random.Range(0, terrainData_List[terrainIndex].getMaxInSuccession());

        

        for(int i = 0; i < terrainInSuccession; i++)
        {
            terrain = Instantiate(terrainData_List[terrainIndex].getTerrain(), currentPosition, Quaternion.identity);
            terrain.transform.parent = parentLocation.transform;
            currentTerrainsQueue.Enqueue(terrain);

            //If the current terrain spawned exceeds the max terrain count, remove the oldest.
            if (currentTerrainsQueue.Count > MAX_TERRAIN_COUNT)
            {
                terrainRemove = currentTerrainsQueue.Dequeue();
                Destroy(terrainRemove);
            }

            currentPosition.x++;
        }
    }
}
