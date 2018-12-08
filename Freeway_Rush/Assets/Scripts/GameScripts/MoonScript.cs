using UnityEngine;

public class MoonScript : MonoBehaviour
{

    int DIRECTIONAL_LIGHT_ROTATION = 5;
    float directionalLightRotation;

    // Use this for initialization
    void Start()
    {

    }


    void FixedUpdate()
    {
        transform.Rotate(new Vector3(DIRECTIONAL_LIGHT_ROTATION * Time.deltaTime, 0, 0));
    }

}
