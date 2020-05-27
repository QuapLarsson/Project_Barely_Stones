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
        //double temp0 = 0.09d;
        //double temp1 = 0.13d;
        //activeFace.transform.position = new Vector3(0, (float)temp0/100, (float)temp1/100);

        Debug.Log(activeFace.transform.position);
        
    }

    void Update()
    {

        //TÄnkte lägga till ett byta till combat_face här men det kan man ta senare kanske

    }

}
