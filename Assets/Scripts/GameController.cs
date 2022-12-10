using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Cell[] cells;    
    [SerializeField] private UnityEvent tileSpawnEvent;
    [SerializeField] private UnityEvent gameOverEvent;
    [SerializeField] private UnityEvent winGameEvent;
    [SerializeField] private UnityEvent<int> updateScoreEvent;
    [SerializeField] private UnityEvent moveTileEvent;
    [SerializeField] private Color32[] tileBackgroundColor;
    [SerializeField] private Color32[] tileTextColor;
    const int targetScore = 2048;

    private bool isMoved = false;    

    private void Start()
    {
        tileSpawnEvent.Invoke();
        tileSpawnEvent.Invoke();
    }
    
    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {            
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {            
            MoveRight();
        }        
    }

    private void PaintTile(Tile tile)
    {
        int colorIndex = Convert.ToInt32(Math.Log(tile.Value));        
        
        Color backgroundColor = tileBackgroundColor[colorIndex];
        tile.GetComponent<Image>().color = backgroundColor;        

        Color textColor = tileTextColor[colorIndex];
        tile.GetComponentInChildren<Text>().color = textColor;
    }

    public void NewTile(int cellIndex, Tile tile)
    {
        PaintTile(tile);
        cells[cellIndex].Tile = tile;
    }

    private void SpawnNewTile()
    {
        if (isMoved)
        {
            tileSpawnEvent.Invoke();
            isMoved = false;
        }
    }

    private void CheckGameOver()
    {
        bool hasEmptyCell = false;
        for (int i = 0; i < cells.Length; ++i)
        {
            if (cells[i].transform.childCount == 0)
            {
                hasEmptyCell = true;
                break;
            }
        }

        if (hasEmptyCell)
        {
            return;
        }

        bool moveable = false;
        foreach (Cell cell in cells)
        {
            if (cell.Tile == null)
            {
                continue;
            }

            if (cell.Right != null && cell.Right.Tile != null && (cell.Tile.Value == cell.Right.Tile.Value) )
            {
                moveable = true;
                break;
            }

            if (cell.Down != null && cell.Down.Tile != null && (cell.Tile.Value == cell.Down.Tile.Value))
            {
                moveable = true;
                break;
            }
        }


        if (!hasEmptyCell && !moveable)
        {
            gameOverEvent.Invoke();
        }
    }

    private void MoveUp()
    {        
        for(int i = 0; i < cells.Length; ++i)
        {
            MoveUp(cells[i]);
        }

        if (isMoved)
        {
            moveTileEvent.Invoke();
        }

        CheckGameOver();
        SpawnNewTile();
    }

    private void MoveDown()
    {        
        for (int i = cells.Length-1; i >= 0; --i)
        {
            MoveDown(cells[i]);
        }

        if (isMoved)
        {
            moveTileEvent.Invoke();
        }

        CheckGameOver();
        SpawnNewTile();
    }

    private void MoveLeft()
    {        
        for (int i = 0; i < cells.Length; ++i)
        {
            MoveLeft(cells[i]);
        }

        if (isMoved)
        {
            moveTileEvent.Invoke();
        }

        CheckGameOver();
        SpawnNewTile();
    }

    private void MoveRight()
    {        
        for (int i = cells.Length - 1; i >= 0; --i)
        {
            MoveRight(cells[i]);
        }

        if (isMoved)
        {
            moveTileEvent.Invoke();
        }

        CheckGameOver();
        SpawnNewTile();
    }

    private void Move(Cell currentCell, Cell nextCell)
    {
        if (currentCell != nextCell)
        {
            nextCell.Tile.transform.SetParent(currentCell.transform);
            currentCell.Tile = nextCell.Tile;
            nextCell.Tile = null;
            isMoved = true;
        }        
    }

    private void Double(Cell currentCell)
    {
        currentCell.Tile.Value *= 2;
        updateScoreEvent.Invoke(currentCell.Tile.Value);
        PaintTile(currentCell.Tile);
        Destroy(currentCell.Tile.transform.parent.GetChild(0).gameObject);

        if (currentCell.Tile.Value == targetScore)
        {
            winGameEvent.Invoke();
        }
    }
   
    private void MoveUp(Cell currentCell)
    {           
        Cell nextCell = currentCell.Down;        
        while (nextCell && nextCell.Tile == null)
        {
            nextCell = nextCell.Down;
        }

        if (nextCell == null)
            return;

        if (currentCell.Tile != null)
        {                        
            if (currentCell.Tile.Value == nextCell.Tile.Value)
            {
                Move(currentCell, nextCell);
                Double(currentCell);
            }            
            else
            {
                Move(currentCell.Down, nextCell);
            }
        }
        else
        {
            Move(currentCell, nextCell);
            MoveUp(currentCell);
        }
    }

    private void MoveDown(Cell currentCell)
    {
        Cell nextCell = currentCell.Up;
        while (nextCell && nextCell.Tile == null)
        {
            nextCell = nextCell.Up;
        }

        if (nextCell == null)
            return;

        if (currentCell.Tile != null)
        {
            if (currentCell.Tile.Value == nextCell.Tile.Value)
            {
                Move(currentCell, nextCell);
                Double(currentCell);
            }
            else
            {
                Move(currentCell.Up, nextCell);
            }
        }
        else
        {
            Move(currentCell, nextCell);
            MoveDown(currentCell);
        }
    }

    private void MoveLeft(Cell currentCell)
    {
        Cell nextCell = currentCell.Right;
        while (nextCell && nextCell.Tile == null)
        {
            nextCell = nextCell.Right;
        }

        if (nextCell == null)
            return;

        if (currentCell.Tile != null)
        {
            if (currentCell.Tile.Value == nextCell.Tile.Value)
            {
                Move(currentCell, nextCell);
                Double(currentCell);
            }
            else
            {
                Move(currentCell.Right, nextCell);
            }
        }
        else
        {
            Move(currentCell, nextCell);
            MoveLeft(currentCell);
        }
    }

    private void MoveRight(Cell currentCell)
    {
        Cell nextCell = currentCell.Left;
        while (nextCell && nextCell.Tile == null)
        {
            nextCell = nextCell.Left;
        }

        if (nextCell == null)
            return;

        if (currentCell.Tile != null)
        {
            if (currentCell.Tile.Value == nextCell.Tile.Value)
            {
                Move(currentCell, nextCell);
                Double(currentCell);
            }
            else
            {
                Move(currentCell.Left, nextCell);
            }
        }
        else
        {
            Move(currentCell, nextCell);
            MoveRight(currentCell);
        }
    }
}
