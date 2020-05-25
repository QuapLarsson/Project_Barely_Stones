using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform followTarget;
    public float scrollSensitivity = 1f;
    public float maxAngle = 60;
    public float minAngle = 30;
    public float offsetX = 0f;
    public float offsetY = 0f;
    public float offsetZ = 0f;
    public float highestPoint = 10f;
    public Vector3 lookAtOffset;
    private float cameraRailPosition = .5f;
    private Transform CameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameraRailPosition += Input.GetAxis("Mouse ScrollWheel") * -scrollSensitivity;
        cameraRailPosition = Mathf.Clamp(cameraRailPosition, 0, 1);

        transform.position = new Vector3(followTarget.position.x+offsetX, followTarget.position.y+offsetY + (highestPoint * cameraRailPosition), followTarget.position.z+offsetZ);
        
        CameraTransform.LookAt(followTarget.position + lookAtOffset);
    }
}
