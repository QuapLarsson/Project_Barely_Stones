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

    public bool UseTurn(PlayableCharacter[] playableCharacters, TileGrid tileGrid, Pathfinding pathfinding)
    {
        Tile startTile = tileGrid.GetTileFrom(this.gameObject);
        List<PlayableCharacter> unitsInRange = pathfinding.FindUnitsInRange(startTile, movement, attackRange, playableCharacters);

        if (unitsInRange.Count > 0)
        {
            PlayableCharacter target = SearchForWeakUnit(unitsInRange.ToArray());

            Tile newTile = pathfinding.FindTileInRangeOfUnit(tileGrid.GetTileFrom(this.gameObject), tileGrid.GetTileFrom(target.gameObject), attackRange);

            MoveTo(tileGrid.GetCenterPointOfTile(newTile), tileGrid);
            return true;
        }

        else if (currentAttitude == Attitude.AggressiveSearch)
        {
            PlayableCharacter target = SearchForWeakUnit(playableCharacters);

            Tile targetTile = pathfinding.FindTileInRangeOfUnit(tileGrid.GetTileFrom(this.gameObject), tileGrid.GetTileFrom(target.gameObject), attackRange);

            List<Tile> pathToTarget = pathfinding.FindPath(tileGrid.GetTileFrom(this.gameObject), targetTile);

            MoveTo(tileGrid.GetCenterPointOfTile(pathToTarget[movement - 1]), tileGrid);
            return true;
        }
        return false;
    }

    PlayableCharacter SearchForWeakUnit(PlayableCharacter[] playableCharacters)
    {
        PlayableCharacter weakestUnit = playableCharacters[0];

        for (int i = 0; i < playableCharacters.Length; i++)
        {
            if (playableCharacters[i].fighter.myCurrentHP < weakestUnit.fighter.myCurrentHP)
            {
                weakestUnit = playableCharacters[i];
            }
        }

        return weakestUnit;
    }

    public PlayableCharacter FindAdjacentTarget(PlayableCharacter[] playableCharacters, TileGrid tileGrid, Pathfinding pathfinding)
    {
        List<PlayableCharacter> adj = pathfinding.FindUnitsInRange(tileGrid.GetTileFrom(this.gameObject), 0, 1, playableCharacters);
        if (adj.Count > 0)
        {
            return adj[0];
        }
        return null;
    }
}
