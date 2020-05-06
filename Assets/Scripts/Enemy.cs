using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum Attitude { HoldPosition, AggressiveSearch }
    [SerializeField] Attitude currentAttitude;

    NavMeshAgent navMeshAgent;
    [SerializeField] int movement = 4; // TEMP
    [SerializeField] int attackRange = 1; // TEMP

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

    public void UseTurn(PlayableCharacter[] playableCharacters, TileGrid tileGrid, Pathfinding pathfinding)
    {
        List<PlayableCharacter> unitsInRange = pathfinding.FindUnitsInRange(tileGrid.GetTileFrom(this.gameObject), movement, attackRange, playableCharacters);

        if (unitsInRange.Count != 0)
        {
            Tile newTile = pathfinding.FindTileInRangeOfUnit(tileGrid.GetTileFrom(this.gameObject), tileGrid.GetTileFrom(unitsInRange[0].gameObject), attackRange);

            MoveTo(tileGrid.GetCenterPointOfTile(newTile), tileGrid);
        }

        else if (currentAttitude == Attitude.AggressiveSearch)
        {

        }
    }
}
