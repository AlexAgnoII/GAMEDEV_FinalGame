using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> movingObstacleList;
    [SerializeField] Transform spawnLocation;

    private float minSpawnTime;
    private float maxSpawnTime;
    private float speed;

    private Vector3 directionTowards;
    private int vehicleSize;


	void Start () {
        this.defaultBetaValues(); //remove when balance iss done.

        vehicleSize = movingObstacleList.Count;

        if(string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_ROAD))
            directionTowards = Vector3.back;
        
        else directionTowards = Vector3.forward;

        StartCoroutine(SpawnVehicle());
	}

    private void defaultBetaValues()
    {
        this.minSpawnTime = 1.0f;
        this.maxSpawnTime = 4.0f;
        this.speed = Random.Range(2.0f, 8.0f);
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

    //Sets the values of the vehicle based on the difficulty.
    public void setVehiclesData(float minSpawnTime, float maxSpawnTime, float speed)
    {
        this.minSpawnTime = minSpawnTime;
        this.maxSpawnTime = maxSpawnTime;
        this.speed = speed;
    }
}
