﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public Vector3 highestPosition = new Vector3(0,10,-10);//represents the x,y,z coordinates of the camera compared to this gameobject
    public Vector3 lowestPosition = new Vector3(0,1f,-1f);

    public float maxAngle = 80;
    public float minAngle = 0;

    public float speedToFokus = .1f;//seconds to reach target

    public float scrollSensitivity = 1f;

    public float moveSensitivityX = .3f;
    public float moveSensitivityY = .3f;

    Transform ExternalFokus;//An external gameobject that the camera is fokusing on
    Transform CameraTransform;

    private float cameraRailPosition = .5f;//is between 1f and 0f used to calculate angle and position of cameratransform

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = transform.GetChild(0).transform;   


    }


    public void ClearFokus()
    {
        ExternalFokus = null;
    }

    public void SetFokus(Transform transform)
    {
        ExternalFokus = transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(2))
        {
            ClearFokus();

            Vector2 moveBy = new Vector2();
            moveBy.x = Input.GetAxis("Mouse X") * moveSensitivityX*-1;
            moveBy.y = Input.GetAxis("Mouse Y") * moveSensitivityY*-1;

            Vector3 currentPosition = transform.position;
            currentPosition.x += moveBy.x;
            currentPosition.z += moveBy.y;

            transform.position = currentPosition;
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit rcHit;
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out rcHit, LayerMask.NameToLayer("selectableCharacter")))
            {
                SetFokus(rcHit.collider.transform);
            }
        }

        if (ExternalFokus != null)
        {
            Vector3 fokusPosition = new Vector3(ExternalFokus.position.x,0,ExternalFokus.position.z);
            transform.position = Vector3.Lerp(transform.position, fokusPosition, speedToFokus);
        }

        cameraRailPosition += Input.GetAxis("Mouse ScrollWheel")*scrollSensitivity;
        cameraRailPosition = Mathf.Clamp(cameraRailPosition, 0, 1);

        Vector3 cameraDiffPos = highestPosition - lowestPosition;
        float cameraDiffAngle = maxAngle - minAngle;

        CameraTransform.localPosition = lowestPosition + (highestPosition * cameraRailPosition);
        Vector3 RotationVector = CameraTransform.rotation.eulerAngles;
        RotationVector.x = minAngle + (maxAngle * cameraRailPosition);
        CameraTransform.rotation = Quaternion.Euler(RotationVector);
    }
}