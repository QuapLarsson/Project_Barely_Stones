using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [Header("Save Point Info")]
    public string description;
    public int id;
    //public LayerMask layer;
    [HideInInspector]
    private RaycastHit hit;
    private Vector3 camPos;
    private Vector3 mousePos;
    

    public void OnInteract()
    {
        //SaveMenu.instance.activeSavePoint = this;
        SaveMenu.instance.OpenMenu(this);
    }

    public void Update()
    {
        LeftMouseClick();
    }

    private void LeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            camPos = Camera.main.transform.position;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Hit: " + hit.collider.gameObject.layer + " Mask is: ");
                if (hit.collider.gameObject.layer == 13)
                {
                    //Debug.Log("Success!");
                OnInteract();
            }

        }
        }
    }
}
