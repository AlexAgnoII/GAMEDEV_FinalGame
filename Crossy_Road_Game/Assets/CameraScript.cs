using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offSet = new Vector3(-2.5f, 7f , -6f);
    [SerializeField] private float smoothness = 0.15f;

    private void Awake()
    {
        this.transform.position = playerTransform.position + offSet;
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position,
                                               playerTransform.position + offSet,
                                               smoothness);
    }

}
