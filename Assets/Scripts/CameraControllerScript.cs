using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public Vector3 highestPosition = new Vector3(0,100,-100);//represents the x,y,z coordinates of the camera compared to this gameobject

    public Vector3 lowestPosition = new Vector3(0,10,-10);

    Transform ExternalFokus;//An external gameobject that the camera is fokusing on
    Transform CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = transform.GetChild(0).transform;   
    }

    // Update is called once per frame
    void Update()
    {
        CameraTransform.LookAt(transform);



    }
}
