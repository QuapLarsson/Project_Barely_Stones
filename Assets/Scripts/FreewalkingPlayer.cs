using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreewalkingPlayer : MonoBehaviour
{
    [Header("Setup")]
    public NavMeshAgent navmeshAgent;
    public Transform moverTarget;
    public Animator animator;

    public Transform cameraMover;
    public int minY = 0;
    public int maxY = 100;
    public int minX = 0;
    public int maxX = 50;
    public int minZ = 0;
    public int maxZ = 50;
    public MeshCollider reachCollider;
    [HideInInspector]
    private Vector3 speed;
    private Vector3 previousPosition;
    private Vector3 newPos;
    private float rotationVelocity = 10f;
    //private float maxSpeed = 0.1f;
    float newZ = 0;
    float newX = 0;


    private void FixedUpdate()
    {
        if (Input.GetKey("w") && Input.GetKey("s"))
        {
            newZ = 0;
        }
        else if (Input.GetKey("w"))
        {
            newZ = 1;
        }
        else if (Input.GetKey("s"))
        {
            newZ = -1;
        }
        else
        {
            newZ = 0;
        }

        if (Input.GetKey("a") && Input.GetKey("d"))
        {
            newX = 0;
        }
        else if (Input.GetKey("a"))
        {
            newX = -1;
        }
        else if (Input.GetKey("d"))
        {
            newX = 1;
        }
        else
        {
            newX = 0;
        }

        if (newX != 0 && newZ != 0)
        {
            newX *= 0.71f;
            newZ *= 0.71f;
        }
        newPos = new Vector3(transform.position.x + newX, transform.position.y, transform.position.z + newZ);
        moverTarget.position = newPos;
        navmeshAgent.SetDestination(moverTarget.position);
        
        FaceDirection();
        cameraMover.position = transform.position;
    }

    private void FaceDirection()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((newPos - transform.position)), rotationVelocity);
    }
}
