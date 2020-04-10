using UnityEngine;
using UnityEngine.AI;

public class TileGrid
{
    public Tile[,] tileArray { get; private set; }
    float tileSize;
    Vector3 origin;

    public TileGrid(int width, int height, float tileSize, Vector3 origin)
    {
        tileArray = new Tile[width, height];
        this.tileSize = tileSize;
        this.origin = origin;

        for (int x = 0; x < tileArray.GetLength(0); x++)
        {
            for (int y = 0; y < tileArray.GetLength(1); y++)
            {
                tileArray[x, y] = new Tile(this, x, y);

                if (!IsTileWalkable(tileArray[x, y]))
                {
                    tileArray[x, y].isWalkable = false;
                }

                //TEMP: Draw some lines to indicate where the tiles are and what is not walkable
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, float.PositiveInfinity);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, float.PositiveInfinity);

                if (!tileArray[x, y].isWalkable)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y + 1), Color.red, float.PositiveInfinity);
                }
            }
        }
    }

    public Vector3 GetWorldPosition (int x, int y) => new Vector3(x, 0, y) * tileSize + origin;

    public Tile GetTileAt(Vector3 worldCoordinates)
    {
        int x = Mathf.FloorToInt((worldCoordinates - origin).x / tileSize);
        int y = Mathf.FloorToInt((worldCoordinates - origin).z / tileSize);

        return tileArray[x, y];
    }

    public Tile GetTileByDirectionInGrid(Tile sourceTile, Vector2Int targetTileDirection)
    {
        int x = sourceTile.x + targetTileDirection.x;
        int y = sourceTile.y + targetTileDirection.y;

        if (x < 0 || y < 0 || x > tileArray.GetLength(0) || y > tileArray.GetLength(1))
        {
            return null;
        }

        return tileArray[x, y];
    }

    public Tile[] GetAdjacentTiles(Tile tile)
    {
        Tile[] adjacentTiles = new Tile[4];

        adjacentTiles[0] = GetTileByDirectionInGrid(tile, new Vector2Int(0, 1));
        adjacentTiles[1] = GetTileByDirectionInGrid(tile, new Vector2Int(1, 0));
        adjacentTiles[2] = GetTileByDirectionInGrid(tile, new Vector2Int(0, -1));
        adjacentTiles[3] = GetTileByDirectionInGrid(tile, new Vector2Int(-1, 0));

        return adjacentTiles;
    }

    public Vector3 GetCenterPointOfTile(Tile tile)
    {
        //TODO: Check IsTileWalkable's TODO
        Vector3 tileCenter = GetWorldPosition(tile.x, tile.y) + new Vector3(tileSize / 2, 100, tileSize / 2);

        RaycastHit rayHit;
        Ray ray = new Ray(tileCenter, Vector3.down);
        Physics.Raycast(ray, out rayHit, float.PositiveInfinity);

        return rayHit.point;
    }

    bool IsTileWalkable(Tile tile)
    {
        //TODO: Maybe make a better solution for this, right now it spawns a ray that checks where the tile's center coordinate is and if that coordinate is close to a NavMesh
        Vector3 tileCenter = GetCenterPointOfTile(tile);

        if (!NavMesh.SamplePosition(tileCenter, out NavMeshHit navHit, 1f, NavMesh.AllAreas))
        {
            return false;
        }

        return true;
    } 
}