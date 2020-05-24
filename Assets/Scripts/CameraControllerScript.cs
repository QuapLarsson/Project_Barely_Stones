using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    Vector3 rememberedCameraPos;
    Quaternion rememberedCameraRot;
    public bool m_cameraIsMoving = false;

    private float cameraRailPosition = .5f;//is between 1f and 0f used to calculate angle and position of cameratransform

    private float lastClick = 0;
    private float doubleClickTimeLimit = 0.3f;//tid under vilken man måste klicka 2 gånger för att dubbelklick ska registreras, i sekunder.

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

    public bool DetectDoubleClick()//Måste köras varje frame för att den ska kunna upptäcka dubbelklick
    {
        bool returnbool = false;
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())//om klick
        {
            if (lastClick == 0)
                lastClick = Time.time;
            else
            {
                if ((Time.time - lastClick) < doubleClickTimeLimit)
                {
                    returnbool = true;
                }
            }

            lastClick = Time.time;
        }

        return returnbool;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(2) && !EventSystem.current.IsPointerOverGameObject())
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

        if (DetectDoubleClick())
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

        cameraRailPosition += Input.GetAxis("Mouse ScrollWheel")*-scrollSensitivity;
        cameraRailPosition = Mathf.Clamp(cameraRailPosition, 0, 1);

        Vector3 cameraDiffPos = highestPosition - lowestPosition;
        float cameraDiffAngle = maxAngle - minAngle;

        CameraTransform.localPosition = lowestPosition + (highestPosition * cameraRailPosition);
        Vector3 RotationVector = CameraTransform.rotation.eulerAngles;
        RotationVector.x = minAngle + (maxAngle * cameraRailPosition);
        CameraTransform.rotation = Quaternion.Euler(RotationVector);
    }

    public void ShiftCameraToBattle(Transform a_FirstFighterPos, Transform a_SecondFighterPos)
    {
        rememberedCameraPos = transform.position;
        rememberedCameraRot = transform.rotation;

        Vector3 averagePos = Vector3.zero;
        averagePos.x = (a_FirstFighterPos.position.x + a_SecondFighterPos.position.x) / 2;
        averagePos.y = (a_FirstFighterPos.position.y + a_SecondFighterPos.position.y) / 2;
        averagePos.z = (a_FirstFighterPos.position.z + a_SecondFighterPos.position.z) / 2;

        StartCoroutine(LerpFromTo(transform.position, averagePos, 1f));
    }

    public void RestoreCamera()
    {
        StartCoroutine(ReturnLerp(transform.position, rememberedCameraPos, rememberedCameraRot, 1f));
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration)
    {
        m_cameraIsMoving = true;
        Transform temp = transform;
        Vector3 cameraRight = transform.right;
        //Get right direction in world space coord
        cameraRight = transform.InverseTransformDirection(cameraRight);
        //Remove up/down rotation
        cameraRight.y = 0;
        cameraRight.Normalize();
        Vector3 target = pos2 + (cameraRight * 2);
        temp.position = target;
        temp.LookAt(pos2, Vector3.up);
        StartCoroutine(RotateFromTo(transform.rotation, temp.rotation, duration));

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, target, t / duration);
            yield return 0;
        }
        transform.position = target;
        m_cameraIsMoving = false;
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator ReturnLerp(Vector3 pos1, Vector3 returnPos, Quaternion returnRot, float duration)
    {
        m_cameraIsMoving = true;
        StartCoroutine(RotateFromTo(transform.rotation, returnRot, duration));

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(pos1, returnPos, t / duration);
            yield return 0;
        }
        transform.position = transform.position;
        m_cameraIsMoving = false;
    }

    IEnumerator RotateFromTo(Quaternion rot1, Quaternion rot2, float duration)
    {
        transform.rotation = Quaternion.RotateTowards(rot1, rot2, 1f * Time.deltaTime);
        /*for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            //transform.rotation = Quaternion.Lerp(rot1, rot2, t / duration);
            yield return 0;
        }*/
        yield return 0;
        transform.rotation = rot2;
    }
}
