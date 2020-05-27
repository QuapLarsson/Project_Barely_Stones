﻿using System.Collections;
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
    public BattleManager myBattleManager;

    int turnCount;
    PlayableCharacter[] unitsToMove;
    Enemy[] enemies;

    bool isAttacking = false;
    List<Tile> enemyTiles;

    //TEMP: Materials for highlighting which unit is selected TODO: Not use material as a highlight for selectedUnit
    [SerializeField] GameObject tileHighlighter;
    [SerializeField] GameObject moveableTileHighlighter;
    List<GameObject> moveableTileHighlighters = new List<GameObject>();

    Ray mouseRay;
    RaycastHit rayHit;


    void Awake()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        unitsToMove = FindObjectsOfType<PlayableCharacter>();
        tileGrid = new TileGrid(gridWidth, gridHeight, tileSize, gridOrigin);
        pathfinding = new Pathfinding(tileGrid);
        //NextTurn();

        foreach (Enemy enemy in enemies)
        {
            tileGrid.GetTileAt(enemy.transform.position).isWalkable = false;
        }

        foreach (PlayableCharacter unit in unitsToMove)
        {
            tileGrid.GetTileAt(unit.transform.position).isWalkable = false;
            tileGrid.GetTileAt(unit.transform.position).myOccupyingCharacter = unit;
        }
    }

    void Update()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
        {
            Tile tile = tileGrid.GetTileAt(rayHit.point);
            Vector3 tilePosition = Vector3.zero;
            if (tile != null)
            {
                tilePosition = tileGrid.GetCenterPointOfTile(tile);
            }
            if (tilePosition != Vector3.zero)
            {
                tileHighlighter.transform.position = new Vector3(tilePosition.x, 0.75f, tilePosition.z);
            }
        }

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
            if (Physics.Raycast(mouseRay, out rayHit, float.PositiveInfinity) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (rayHit.collider.GetComponent<PlayableCharacter>() != null)
                {
                    PlayableCharacter unitHit = rayHit.collider.GetComponent<PlayableCharacter>();

                    foreach (PlayableCharacter character in unitsToMove)
                    {
                        if (character == unitHit)
                        {
                            //TEMP: Changes the material of the selectedUnit and changes back the material of the previous selectedUnit
                            selectedUnit = unitHit;
                            selectedUnit.CalculateWalkableTiles(pathfinding, tileGrid);
                            HighlightMoveableTiles(selectedUnit.walkableTiles);
                        }
                    }
                }
            }
        }
    }

    void ClickToMoveUnit()
    {
        if (Input.GetMouseButtonDown(1) && selectedUnit != null)
        {
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
                                Vector3 moveablePosition = tileGrid.GetCenterPointOfTile(tile);
                                selectedUnit.MoveTo(moveablePosition, tileGrid);

                                unitsToMove[i] = null;
                                selectedUnit = null;
                                HideMoveableTiles();
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
                            GameObject temp = enemies[i].gameObject;
                            StartCoroutine(myBattleManager.Init(selectedUnit.GetComponent<Fighter>(), temp));
                        }
                    }

                    isAttacking = false;
                }
            }
        }
    }

    void HighlightMoveableTiles(List<Tile> moveableTiles)
    {
        HideMoveableTiles();

        for (int i = 0; i < moveableTiles.Count; i++)
        {
            Vector3 tilePosition = tileGrid.GetCenterPointOfTile(moveableTiles[i]);
            Vector3 highlightPosition = new Vector3(tilePosition.x, 0.5f, tilePosition.z);

            if (i < moveableTileHighlighters.Count)
            {
                moveableTileHighlighters[i].transform.position = highlightPosition;
                moveableTileHighlighters[i].SetActive(true);
            }

            else
            {
                moveableTileHighlighters.Add(Instantiate(moveableTileHighlighter, highlightPosition, Quaternion.identity));
            }
        }
    }

    void HideMoveableTiles()
    {
        for (int i = 0; i < moveableTileHighlighters.Count; i++)
        {
            moveableTileHighlighters[i].SetActive(false);
        }
    }

    public void NextTurn()
    {
        unitsToMove = FindObjectsOfType<PlayableCharacter>();

        foreach (Enemy enemy in enemies)
        {
            PlayableCharacter target = enemy.FindAdjacentTarget(unitsToMove, tileGrid, pathfinding);
            if (target != null)
            {
                StartCoroutine(myBattleManager.Init(enemy.GetComponent<Fighter>(), target.gameObject));
            }
            else
            {
                enemy.UseTurn(unitsToMove, tileGrid, pathfinding);
            }
        }
        
        selectedUnit = null;
        turnCount++;
        FindObjectOfType<CombatUI>().UpdateText(turnCount);
        HideMoveableTiles();
    }

    public void SearchForEmemies()
    {
        if (selectedUnit == null)
        {
            return;
        }

        enemyTiles = new List<Tile>();

        Tile[] adjacentTiles = tileGrid.GetAdjacentTiles(tileGrid.GetTileFrom(selectedUnit.gameObject));

        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = 0; j < adjacentTiles.Length; j++)
            {
                if (tileGrid.GetTileFrom(enemies[i].gameObject) == adjacentTiles[j])
                {
                    enemyTiles.Add(tileGrid.GetTileFrom(enemies[i].gameObject));
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
                    }
                }
            }
        }
    }
}
