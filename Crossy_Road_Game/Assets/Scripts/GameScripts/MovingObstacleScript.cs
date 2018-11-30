using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleScript : MonoBehaviour {

    [SerializeField] private float distanceOfDeath = 20;
    private float speed;
    private Vector3 directionGo = Vector3.forward;

    private bool shouldMove;

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

    private void canMove()
    {
        this.shouldMove = true;
    }

    private void cannotMove()
    {
        this.shouldMove = false;
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
