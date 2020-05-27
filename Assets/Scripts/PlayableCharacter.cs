using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayableCharacter : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public List<Tile> walkableTiles { get; private set; }
    int movement = 5; // TEMP

    void Awake()
    {
        navMeshAgent = gameObject.GetComponentInChildren<NavMeshAgent>();
    }
    
    private void Update()
    {
    }

    public void MoveTo(Vector3 position, TileGrid tileGrid)
    {
        tileGrid.GetTileAt(transform.position).isWalkable = true;
        tileGrid.GetTileAt(transform.position).myOccupyingCharacter = null;
        navMeshAgent.SetDestination(position);
        tileGrid.GetTileAt(position).isWalkable = false;
        tileGrid.GetTileAt(position).myOccupyingCharacter = this;
    }

    public void CalculateWalkableTiles(Pathfinding pathfinding, TileGrid tileGrid)
    {
        walkableTiles = pathfinding.FindAllPaths(tileGrid.GetTileAt(transform.position), movement);
    }
}
