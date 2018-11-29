using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] float maxCameraZPosition;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float smoothness;
    private Vector3 limitPosition;
    private Vector3 limitOffset;
    private void Awake()
    {
        this.limitPosition = new Vector3(0, 0, 0);
        this.limitOffset = new Vector3(0, 0, 0);
        this.transform.position = player.transform.position + offSet;
    }

    void FixedUpdate()
    {

        if(player != null && player.GetComponent<PlayerScript>().getIfAlive())
        {

            if(this.transform.position.z <= this.maxCameraZPosition && this.transform.position.z >= -this.maxCameraZPosition ||
               player.transform.position.z <= this.maxCameraZPosition && player.transform.position.z >= -this.maxCameraZPosition)
            {
                this.makeCameraFollow();
            }
            else 
            {
                this.makeCameraFollowLimited();
            }

        }

    }

    private void makeCameraFollow()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,
                                       player.transform.position + offSet,
                                       smoothness);
    }

    private void makeCameraFollowLimited()
    {
        this.limitPosition.x = player.transform.position.x;
        this.limitPosition.y = player.transform.position.y;
        this.limitPosition.z = this.transform.position.z;
        this.limitOffset.x = offSet.x;
        this.limitOffset.y = offSet.y;
        this.transform.position = Vector3.Lerp(this.transform.position,
                                               limitPosition + limitOffset,
                                               smoothness);
    }

}
