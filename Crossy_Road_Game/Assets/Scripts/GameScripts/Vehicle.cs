using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    [SerializeField] private float speed = 100;
    [SerializeField] private float distanceOfDeath = 20;
    private Vector3 directionGo = Vector3.forward;

    private void Start(){}

    // Update is called once per frame
    void FixedUpdate () {
        this.transform.Translate(speed * Time.deltaTime * directionGo);

        if(this.transform.position.z > distanceOfDeath || 
           this.transform.position.z < -distanceOfDeath)
        {
            //kill gameobject once he passed through the entire lane.
            Destroy(gameObject);
        }
	}

    public void setDirection(Vector3 direction)
    {
        this.directionGo = direction;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
