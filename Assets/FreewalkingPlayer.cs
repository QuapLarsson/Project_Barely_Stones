using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreewalkingPlayer : MonoBehaviour
{
    [Header("Setup")]
    public NavMeshAgent navmeshAgent;
    public Transform moverTarget;
    public int minY = 20;
    public int maxY = 70;
    public int minX = -50;
    public int maxX = 50;
    public int minZ = -35;
    public int maxZ = 35;
    [HideInInspector]
    private Vector3 speed;
    private float maxSpeed = 0.2f;
    private void FixedUpdate()
    {

        if (Input.GetKey("w"))
        {
            speed = new Vector3(0, 0, 1 * maxSpeed);
            var newZ = Mathf.Clamp(transform.position.z + speed.z, minZ, maxZ);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKey("s"))
        {
            speed = new Vector3(0, 0, -1 * maxSpeed);
            var newZ = Mathf.Clamp(transform.position.z + speed.z, minZ, maxZ);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (Input.GetKey("a"))
        {
            speed = new Vector3(-1 * maxSpeed, 0, 0);
            var newX = Mathf.Clamp(transform.position.x + speed.x, minX, maxX);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("d"))
        {
            speed = new Vector3(1 * maxSpeed, 0, 0);
            var newX = Mathf.Clamp(transform.position.x + speed.x, minX, maxX);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        navmeshAgent.SetDestination(moverTarget.transform.position);
    }

}
