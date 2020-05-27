using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facescript : MonoBehaviour
{

    Transform faceBone;
    public string faceBonePath = "Armature/DEF_hip/DEF_spine.001/DEF_spine.002/DEF_neck/DEF_neck.001/DEF_head";

    Vector3 offset;
    Vector3 scale;
    Vector3 rotation;

    GameObject face;

    void Start()
    {
        faceBone = transform.Find(faceBonePath);
    }
    


}
