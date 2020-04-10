using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatController : MonoBehaviour
{
    TileGrid tileGrid;
    Pathfinding pathfinding;
    PlayableCharacter selectedUnit;

    int turnCount;
    PlayableCharacter[] unitsToMove;
    Enemy[] enemies;

    bool isAttacking = false;
    List<Tile> enemyTiles;

    //TEMP: Materials for highlighting which unit is selected TODO: Not use material as a highlight for selectedUnit
    Material unitMat;
    public Material unitHighlightMat;
    Material enemyMat;
    public Material enemyHighlightMat;

    void Awake()
    {
        //TEMP: Find the original material for a PlayableCharacter
        unitMat = FindObjectOfType<PlayableCharacter>().gameObject.GetComponent<Renderer>().material;
        enemyMat = FindObjectOfType<Enemy>().gameObject.GetComponent<Renderer>().material;
        enemies = FindObjectsOfType<Enemy>();
    }

    void Start()
    {
        tileGrid = new TileGrid(12, 12, 1f, new Vector3(-6, 0, -6));
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
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (isAttacking)
                {
                    if (Input.GetMouseButtonDown(0))
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

                else
                {
                    if (Input.GetMouseButtonDown(0) && rayHit.collider.GetComponent<PlayableCharacter>() != null)
                    {
                        PlayableCharacter unitHit = rayHit.collider.GetComponent<PlayableCharacter>();

                        //TEMP: Change the material of the selectedUnit and change back the material of the previous selectedUnit
                        if (selectedUnit != null)
                        {
                            selectedUnit.GetComponent<Renderer>().material = unitMat;
                        }

                        selectedUnit = unitHit;
                        selectedUnit.GetComponent<Renderer>().material = unitHighlightMat;
                    }

                    else if (Input.GetMouseButtonDown(1) && selectedUnit != null)
                    {
                        Tile tile = tileGrid.GetTileAt(rayHit.point);

                        if (tile.isWalkable)
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

    public void Attack()
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
