using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightScript : MonoBehaviour {

    [SerializeField] private AudioClip nightSound;
    bool night = false;
    int DIRECTIONAL_LIGHT_ROTATION = 30;
    float directionalLightRotation;
    public const string DAY_PHASE = "DAY_PHASE";

    // Use this for initialization
    void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.FinalGameEvents.ON_CHANGE_DIRECTIONAL_LIGHT, this.RotateDirectionalLight);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotateDirectionalLight()
    {
        Debug.Log("Rotating Directional Light");
        transform.Rotate(new Vector3(DIRECTIONAL_LIGHT_ROTATION, 0, 0));
        directionalLightRotation = transform.rotation.eulerAngles.x;
        checkDayPhase();
    }

    public void checkDayPhase()
    {
        Debug.Log("Checking Day Phase");
        if (directionalLightRotation > 180)
        {
            if(night != true)
            {
                Debug.Log("It's now night!");
                night = true;
                Parameters param = new Parameters();
                param.PutExtra(DAY_PHASE, night);

                EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, param);
            }
        }
        else
        {
            if(night == true)
            {
                Debug.Log("It's now morning again!");
                night = false;
                Parameters param = new Parameters();
                param.PutExtra(DAY_PHASE, night);

                EventBroadcaster.Instance.PostEvent(EventNames.FinalGameAudioEvents.ON_DAY_PHASE, param);
                //play morning sound
            }
        }
    }
}
