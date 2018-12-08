using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> movingObstacleList;
    [SerializeField] Transform spawnLocation;

    private float minSpawnTime;
    private float maxSpawnTime;
    private float speed;

    private Vector3 directionTowards;
    private int logSize;

    private bool shouldSpawn;


    void Start()
    {
        logSize = movingObstacleList.Count;

        if (string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_BOUND_WATER))
            directionTowards = Vector3.back;

        else directionTowards = Vector3.forward;


        StartCoroutine(SpawnLog());
    }

    private void OnDestroy()
    {
    }

    private void defaultBetaValues()
    {
        this.minSpawnTime = 2.0f;
        this.maxSpawnTime = 4.0f;
        this.speed = Random.Range(2.0f, 4.0f);
    }

    private void canSpawn()
    {
        this.shouldSpawn = true;
    }

    private void cannotSpawn()
    {
        this.shouldSpawn = false;
    }


    private IEnumerator SpawnLog()
    {
        GameObject movingObstacle;
        int vehicleIndex = 0;

        while (true)
        {
             vehicleIndex = Random.Range(0, logSize);

             yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
             movingObstacle = Instantiate(movingObstacleList[vehicleIndex], spawnLocation.position, Quaternion.identity, spawnLocation);
             movingObstacle.GetComponent<MovingObstacleScript>().setDirection(directionTowards);
             movingObstacle.GetComponent<MovingObstacleScript>().setSpeed(speed);
        }
    }

    public void setLogData(float minSpawnTime, float maxSpawnTime, float speed)
    {
        this.minSpawnTime = minSpawnTime;
        this.maxSpawnTime = maxSpawnTime;
        this.speed = speed;
    }
}
