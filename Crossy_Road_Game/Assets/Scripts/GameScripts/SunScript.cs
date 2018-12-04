using UnityEngine;

public class SunScript : MonoBehaviour {

    [SerializeField] private AudioClip nightSound;
    public const string DAY_PHASE = "DAY_PHASE";
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

    private void dayPhaseChanged()
    {
        Parameters param = new Parameters();
        param.PutExtra(EventNames.FinalGameAudioEvents.CURRENT_DAY_PHASE, night);
        EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.CHANGE_DAY_PHASE, param);
    }
    public void checkDayPhase()
    {
       
        if (directionalLightRotation > 180)
        {
            if(night != true)
            {
                Debug.Log("It's now night!");
                night = true;
                dayPhaseChanged();
                Tell_Morning_Or_Light();
            }
        }
        else
        {
            if(night == true)
            {
                Debug.Log("It's now morning again!");
                night = false;
                dayPhaseChanged();
                Tell_Morning_Or_Light();
            }
        }
    }
}
