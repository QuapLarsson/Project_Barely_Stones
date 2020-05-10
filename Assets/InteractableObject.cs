using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableObject : MonoBehaviour
{
    [Serializable]
    public class ObjectInteractionEvent : UnityEvent { }

    [FormerlySerializedAs("onInteract")]
    [SerializeField]
    private ObjectInteractionEvent m_OnInteract = new ObjectInteractionEvent();

    protected InteractableObject()
    {

    }

    public ObjectInteractionEvent onInteract
    {
        get { return m_OnInteract; }
        set { m_OnInteract = value; }
    }

    private void Act()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        m_OnInteract.Invoke();
    }

    public void TestMessage()
    {
        Debug.Log("Test Message");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)
            && PauseManager.IsPauseState(PauseManager.PauseState.Playing))
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            Vector3 camPos = Camera.main.transform.position;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Hit: " + hit.collider.gameObject.layer + " Object is: " + hit.collider.GetComponentInParent<InteractableObject>());

                if (hit.collider.GetComponentInParent<InteractableObject>() == this)
                {
                    //Debug.Log("Success!");
                    Act();
                }

            }
        }
    }
}
