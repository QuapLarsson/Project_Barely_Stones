using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatController : MonoBehaviour
{
    TileGrid tileGrid;
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeight;
    [SerializeField] float tileSize;
    [SerializeField] Vector3 gridOrigin;
    Pathfinding pathfinding;
    PlayableCharacter selectedUnit;

    int turnCount;
    PlayableCharacter[] unitsToMove;
    Enemy[] enemies;

    bool isAttacking = false;
    List<Tile> enemyTiles;

    //TEMP: Materials for highlighting which unit is selected TODO: Not use material as a highlight for selectedUnit
    Material unitMat;
    [SerializeField] Material unitHighlightMat;
    Material enemyMat;
    [SerializeField] Material enemyHighlightMat;

    Ray mouseRay;
    RaycastHit rayHit;

    void Awake()
    {
        //TEMP: Find the original material for a PlayableCharacter
        unitMat = FindObjectOfType<PlayableCharacter>().gameObject.GetComponent<Renderer>().material;
        enemyMat = FindObjectOfType<Enemy>().gameObject.GetComponent<Renderer>().material;
        enemies = FindObjectsOfType<Enemy>();
    }

    void Start()
    {
        tileGrid = new TileGrid(gridWidth, gridHeight, tileSize, gridOrigin);
        pathfinding = new Pathfinding(tileGrid);
        NextTurn();

        foreach (Enemy enemy in enemies)
        {
            tileGrid.GetTileAt(enemy.transform.position).isWalkable = false;
        }

        foreach (PlayableCharacter unit in unitsToMove)
        {
            tileGrid.GetTileAt(unit.transform.position).isWalkable = false;
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            ClickToAttackEnemy();
        }

        else
        {
            ClickToSelectUnit();
            ClickToMoveUnit();
        }
    }

    void ClickToSelectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (rayHit.collider.GetComponent<PlayableCharacter>() != null)
                {
                    PlayableCharacter unitHit = rayHit.collider.GetComponent<PlayableCharacter>();

                    //TEMP: Changes the material of the selectedUnit and changes back the material of the previous selectedUnit
                    if (selectedUnit != null)
                    {
                        selectedUnit.GetComponent<Renderer>().material = unitMat;
                    }

                    selectedUnit = unitHit;
                    selectedUnit.GetComponent<Renderer>().material = unitHighlightMat;
                    selectedUnit.HighlightWalkableTiles(pathfinding, tileGrid);
                }
            }
        }
    }

    void ClickToMoveUnit()
    {
        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                Tile tile = tileGrid.GetTileAt(rayHit.point);

                foreach (Tile walkableTile in selectedUnit.walkableTiles)
                {
                    if (tile == walkableTile)
                    {
                        for (int i = 0; i < unitsToMove.Length; i++)
                        {
                            if (selectedUnit == unitsToMove[i])
                            {
                                unitsToMove[i] = null;

                                Vector3 moveablePosition = tileGrid.GetCenterPointOfTile(tile);
                                selectedUnit.MoveTo(moveablePosition, tileGrid);
                            }
                        }
                    }
                }
            }
        }
    }

    void ClickToAttackEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                Tile tile = tileGrid.GetTileAt(rayHit.point);

                if (selectedUnit != null)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (tileGrid.GetTileAt(enemies[i].transform.position) == tile)
                        {
                            Debug.Log(string.Format("Attacked {0}", enemies[i].name));
                        }
                    }

                    isAttacking = false;

                    foreach (Enemy enemy in enemies)
                    {
                        enemy.GetComponent<Renderer>().material = enemyMat;
                    }
                }
            }
        }
    }

    public void NextTurn()
    {
        unitsToMove = FindObjectsOfType<PlayableCharacter>();

        for (int i = 0; i < unitsToMove.Length; i++)
        {
            unitsToMove[i].GetComponent<Renderer>().material = unitMat;
        }

        selectedUnit = null;
        turnCount++;
        FindObjectOfType<CombatUI>().UpdateText(turnCount);
    }

    public void SearchForEmemies()
    {
        enemyTiles = new List<Tile>();

        Tile[] adjacentTiles = tileGrid.GetAdjacentTiles(tileGrid.GetTileAt(selectedUnit.transform.position));

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = 0; j < adjacentTiles.Length; j++)
            {
                if (tileGrid.GetTileAt(enemies[i].transform.position) == adjacentTiles[j])
                {
                    enemyTiles.Add(tileGrid.GetTileAt(enemies[i].transform.position));
                }
            }
        }

        if (enemyTiles.Count > 0)
        {
            isAttacking = true;

            for (int i = 0; i < enemyTiles.Count; i++)
            {
                for (int j = 0; j < enemies.Length; j++)
                {
                    if (tileGrid.GetTileAt(enemies[j].transform.position) == enemyTiles[i])
                    {
                        enemies[j].GetComponent<Renderer>().material = enemyHighlightMat;
                    }
                }
            }
        }
    }
}
