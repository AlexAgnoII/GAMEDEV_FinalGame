using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> movingObstacleList;
    [SerializeField] Transform spawnLocation;
    [SerializeField] float minSpawnTime = 1;
    [SerializeField] float maxSpawnTime = 4;
    [SerializeField] float minObstacleSpeed = 2;
    [SerializeField] float maxObstacleSpeed = 8;
    private float speed;
    private Vector3 directionTowards;
    private int vehicleSize;


	void Start () {
        vehicleSize = movingObstacleList.Count;
        if(string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_ROAD) ||
           string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_WATER))
        {
            directionTowards = Vector3.back;
        }
        else 
        {
            directionTowards = Vector3.forward;
        }

        if(string.Equals(this.tag, PrefabTags.TerrainGroup.NORTH_BOUND_WATER) ||
           string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_WATER))
        {
            speed = Random.Range(minObstacleSpeed, maxObstacleSpeed - 4);
            minSpawnTime = 2;
        }
        else speed = Random.Range(minObstacleSpeed, maxObstacleSpeed);


        StartCoroutine(SpawnVehicle());
	}
	
	private IEnumerator SpawnVehicle()
    {
        GameObject movingObstacle;
        int vehicleIndex = 0;

        while(true)
        {
            vehicleIndex = Random.Range(0, vehicleSize);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            movingObstacle = Instantiate(movingObstacleList[vehicleIndex], spawnLocation.position, Quaternion.identity, spawnLocation);
            movingObstacle.GetComponent<MovingObstacleScript>().setDirection(directionTowards);
            movingObstacle.GetComponent<MovingObstacleScript>().setSpeed(speed);
        }
    }
}
