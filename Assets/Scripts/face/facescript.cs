using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facescript : MonoBehaviour
{

    Transform faceBone;
    public string faceBonePath = "Armature/DEF_hip/DEF_spine/DEF_spine.001/DEF_spine.002/DEF_neck/DEF_neck.001/DEF_head";
    
    [SerializeField]
    GameObject facePrefab;

    GameObject activeFace;




    void Start()
    {
        faceBone = transform.Find(faceBonePath);

        activeFace = Instantiate(facePrefab, faceBone);

        activeFace.transform.position = new Vector3(0, 0.0009f, 0.0013f);
    }

    void Update()
    {

        //TÄnkte lägga till ett byta till combat_face här men det kan man ta senare kanske

    }

}
