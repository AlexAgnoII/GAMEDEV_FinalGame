using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleScript : MonoBehaviour {

    [SerializeField] private float distanceOfDeath = 20;
    [SerializeField] private GameObject leftLight;
    [SerializeField] private GameObject rightLight;


    private float speed;
    private Vector3 directionGo = Vector3.forward;
    private bool once = true;

    private bool shouldMove;

    private void Start()
    {
        if(string.Equals(this.tag, PrefabTags.MovingObstacles.VEHICLE))
        {
            EventBroadcaster.Instance.AddObserver(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, this.turnOnLights);
        }
    }

    private void OnDestroy()
    {
        if (string.Equals(this.tag, PrefabTags.MovingObstacles.VEHICLE))
        {
            EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameAudioEvents.ON_DAY_PHASE);
        }
    }

    private void turnOnLights(Parameters param)
    {
        bool night = param.GetBoolExtra(EventNames.FinalGameEvents.PARAM_NIGHT_OR_MORNING, true);


        this.leftLight.GetComponent<Light>().enabled = night;
        this.rightLight.GetComponent<Light>().enabled = night;
    }



    // Update is called once per frame
    void FixedUpdate () {
           if(once)
           {
                EventBroadcaster.Instance.PostEvent(EventNames.FinalGameEvents.ON_VEHICLE_ASK_IF_MORNING);
                once = !once;
           }

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
