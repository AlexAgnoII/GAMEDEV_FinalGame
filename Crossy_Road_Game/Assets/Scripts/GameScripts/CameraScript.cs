using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] private Vector3 offSet = new Vector3(-2.5f, 7f , -6f);
    [SerializeField] private Vector3 offSet_nope = new Vector3(0f, 7f, -6f);
    [SerializeField] private float smoothness = 0.15f;

    private void Awake()
    {
        this.transform.position = player.transform.position + offSet;
    }

    void FixedUpdate()
    {

        if(player.GetComponent<PlayerScript>().getIfAlive())
         this.transform.position = Vector3.Lerp(this.transform.position,
                                                player.transform.position + offSet,
                                                smoothness);




    }

}
