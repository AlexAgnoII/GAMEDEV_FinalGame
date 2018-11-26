using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour {

    [SerializeField] List<GameObject> vehicles;
    [SerializeField] Transform spawnLocation;
    [SerializeField] float minSpawnTime = 1;
    [SerializeField] float maxSpawnTime = 4;
    [SerializeField] float minVehicleSpeed = 2;
    [SerializeField] float maxVehicleSpeed = 8;
    private float speed;
    private Vector3 directionTowards;
    private int vehicleSize;


	void Start () {
        vehicleSize = vehicles.Count;
        if(string.Equals(this.tag, PrefabTags.TerrainGroup.SOUTH_ROAD))
        {
            directionTowards = Vector3.back;
        }
        else
        {
            directionTowards = Vector3.forward;
        }

        speed = Random.Range(minVehicleSpeed, maxVehicleSpeed);

        StartCoroutine(SpawnVehicle());
	}
	
	private IEnumerator SpawnVehicle()
    {
        GameObject vehicle;
        int vehicleIndex = 0;

        while(true)
        {
            vehicleIndex = Random.Range(0, vehicleSize);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            vehicle = Instantiate(vehicles[vehicleIndex], spawnLocation.position, Quaternion.identity, spawnLocation);
            vehicle.GetComponent<Vehicle>().setDirection(directionTowards);
            vehicle.GetComponent<Vehicle>().setSpeed(speed);
        }
    }
}
