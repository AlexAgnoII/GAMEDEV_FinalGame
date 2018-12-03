using UnityEngine;

public class SunScript : MonoBehaviour {

    [SerializeField] private AudioClip nightSound;
    bool night = false;
    int DIRECTIONAL_LIGHT_ROTATION = 5;
    float directionalLightRotation;

    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_VEHICLE_ASK_IF_MORNING, this.Tell_Morning_Or_Light);
	}

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.FinalGameEvents.ON_VEHICLE_ASK_IF_MORNING);
    }

    private void Tell_Morning_Or_Light()
    {
        Parameters param = new Parameters();
        param.PutExtra(EventNames.FinalGameEvents.PARAM_NIGHT_OR_MORNING, night);
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, param);
    }

     void FixedUpdate()
    {
        transform.Rotate(new Vector3(DIRECTIONAL_LIGHT_ROTATION * Time.deltaTime, 0, 0));
        directionalLightRotation = transform.rotation.eulerAngles.x;
        checkDayPhase();
    }

    public void checkDayPhase()
    {
        
        //Debug.Log("Checking Day Phase");
        if (directionalLightRotation > 180)
        {
            if(night != true)
            {
                Debug.Log("It's now night!");
                night = true;
                Tell_Morning_Or_Light();
            }
        }
        else
        {
            if(night == true)
            {
                Debug.Log("It's now morning again!");
                night = false;
                Tell_Morning_Or_Light();
                //play morning sound
            }
        }
    }
}
