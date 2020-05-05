﻿using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    TileGrid currentTileGrid;

    public Pathfinding (TileGrid tileGrid)
    {
        currentTileGrid = tileGrid;
    }

    public List<Tile> FindAllPaths(Tile startTile, int pathLength)
    {
        List<Tile> unsearchedTiles = new List<Tile>();
        List<Tile> validTiles = new List<Tile>();

        //TODO: Make this better
        for (int x = startTile.x - pathLength; x <= startTile.x + pathLength; x++)
        {
            if (x < 0 || x > currentTileGrid.tileArray.GetLength(0) - 1) continue;

            for (int y = startTile.y - pathLength; y <= startTile.y + pathLength; y++)
            {
                if (y < 0 || y > currentTileGrid.tileArray.GetLength(1) - 1) continue;

                if (currentTileGrid.tileArray[x, y] != startTile && currentTileGrid.tileArray[x, y].isWalkable)
                {
                    unsearchedTiles.Add(currentTileGrid.tileArray[x, y]);
                }
            }
        }

        //TODO: Make this more efficient by not having to call FindPath for every tile, instead when already finding the path for another tile add other tiles that also are valid to move to.
        for (int i = 0; i < unsearchedTiles.Count; i++)
        {
            List<Tile> path = FindPath(startTile, unsearchedTiles[i]);

            if (path.Count <= pathLength)
            {
                validTiles.Add(unsearchedTiles[i]);
            }
        }

        return validTiles;
    }

    public List<Tile> FindPath(Tile startTile, Tile goalTile)
    {
        List<Tile> openList = new List<Tile>();
        HashSet<Tile> closedList = new HashSet<Tile>();

        openList.Add(startTile);

        while (openList.Count > 0)
        {
            Tile currentTile = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].myFCost <= currentTile.myFCost && openList[i].myHCost < currentTile.myHCost)
                {
                    currentTile = openList[i];
                }
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            if (currentTile == goalTile)
            {
                return GetFinalPath(startTile, goalTile);
            }

            foreach (Tile neighbourTile in FindTileNeighbours(currentTile))
            {
                if (!neighbourTile.isWalkable || closedList.Contains(neighbourTile))
                {
                    continue;
                }

                int moveCost = currentTile.myGCost + GetManhattanDistance(currentTile, neighbourTile);

                if (moveCost < neighbourTile.myGCost || !openList.Contains(neighbourTile))
                {
                    neighbourTile.myGCost = moveCost;
                    neighbourTile.myHCost = GetManhattanDistance(neighbourTile, goalTile);
                    neighbourTile.myParentTile = currentTile;

                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }

        return null;
    }

    List<Tile> GetFinalPath(Tile aStartTile, Tile aEndTile)
    {
        List<Tile> finalPath = new List<Tile>();
        Tile currentTile = aEndTile;

        while (currentTile != aStartTile)
        {
            finalPath.Add(currentTile);
            currentTile = currentTile.myParentTile;
        }

        finalPath.Reverse();

        return finalPath;
    }

    int GetManhattanDistance(Tile aTileA, Tile aTileB)
    {
        int iX = Mathf.Abs(aTileA.x - aTileB.x);
        int iY = Mathf.Abs(aTileA.y - aTileB.y);

        return iX + iY;
    }

    List<Tile> FindTileNeighbours(Tile aSourceTile)
    {
        List<Tile> neighbourList = new List<Tile>();
        Tile sourceTile = aSourceTile;

        if (sourceTile.y > 0)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.x, sourceTile.y - 1]);
        }

        if (sourceTile.y < currentTileGrid.tileArray.GetLength(1) - 1)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.x, sourceTile.y + 1]);
        }

        if (sourceTile.x > 0)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.x - 1, sourceTile.y]);
        }

        if (sourceTile.x < currentTileGrid.tileArray.GetLength(0) - 1)
        {
            neighbourList.Add(currentTileGrid.tileArray[sourceTile.x + 1, sourceTile.y]);
        }

        return neighbourList;
    }
}
