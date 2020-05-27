public class Tile
{
    public TileGrid grid;
    public int x, y;
    public PlayableCharacter myOccupyingCharacter;

    public bool isWalkable = true;
    public Tile myParentTile = null;

    public int myGCost = 0;
    public int myHCost = 0;
    public int myFCost { get { return myGCost + myHCost; } }

    public Tile (TileGrid grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
}