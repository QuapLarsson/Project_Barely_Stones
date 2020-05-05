using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayableCharacter : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public List<Tile> walkableTiles { get; private set; }
    int movement = 5;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position, TileGrid tileGrid)
    {
        tileGrid.GetTileAt(transform.position).isWalkable = true;
        navMeshAgent.SetDestination(position);
        tileGrid.GetTileAt(position).isWalkable = false;
    }

    public void CalculateWalkableTiles(Pathfinding pathfinding, TileGrid tileGrid)
    {
        walkableTiles = pathfinding.FindAllPaths(tileGrid.GetTileAt(transform.position), movement);
    }
}
