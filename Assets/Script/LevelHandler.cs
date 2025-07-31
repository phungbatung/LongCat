using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LevelHandler
{ 
    
    public Cell[,] Map { get; private set; }

    public Cell Head { get; private set; }

    public LevelHandler(int[,] mapData)
    {
        LoadData(mapData);
    }

    public void LoadData(int[,] mapData)
    {
        int width = mapData.GetLength(0);
        int height = mapData.GetLength(1);
        Map = new Cell[width, height];
        float offSetX = -(width-1) * 1.0f / 2f;
        float offSetY = (height-1) * 1.0f / 2f;
        Debug.Log($"OffSetX: {offSetX}, OffSetY: {offSetY}");
        for (int row = 0; row < width; row++)
        {
            for (int column = 0; column < height; column++)
            {
                var cell = new Cell(row, column, column + offSetX, offSetY - row, (BlockType)mapData[row, column]);
                Map[row, column] = cell;
                if(cell.BlockType == BlockType.Head)
                {
                    Head = cell;
                }
            }
        }
    }

    

    public bool CanMove(Direction direction)
    {
        if (GetNextCellAtDirection(Head, direction, out Cell nextCell))
        {
            //Debug.Log($"Head: (({Head.X}, {Head.Y})");
            //Debug.Log($"Next cell: (({nextCell.X}, {nextCell.Y}), BlockType: {nextCell.BlockType}");
            if(nextCell.BlockType == BlockType.Empty)
                return true;
        }
        return false;
    }

    public Cell MoveInDirection(Direction direction)
    {

        Cell nextCell = Head;
        if(GetNextCellAtDirection(nextCell, direction, out Cell _nextCell))
        {
            nextCell = _nextCell;
        }
        else
        {
            Debug.LogError($"Check CanMove(direction) is true, but cannot find the next cell at direction: {direction}");
            return null;
        }
        while (nextCell.BlockType == BlockType.Empty)
        {
            Head.BlockType = BlockType.Body;
            Head = nextCell;
            Head.BlockType = BlockType.Head;

            if(GetNextCellAtDirection(Head, direction, out _nextCell))
            {
                nextCell = _nextCell;
            }
            else
            {
                break;
            }
            
        }
        Debug.Log($"New Head Target: (({Head.X}, {Head.Y})");
        return Head;
    }

    public bool GetNextCellAtDirection(Cell currentCell, Direction direction, out Cell nextCell)
    {
        nextCell = null;

        int newX = currentCell.X;
        int newY = currentCell.Y;

        switch (direction)
        {
            case Direction.Left:
                newY -= 1;
                break;
            case Direction.Right:
                newY += 1;
                break;
            case Direction.Up:
                newX -= 1;
                break;
            case Direction.Down:
                newX += 1;
                break;
        }

        if (newX >= 0 && newX < Map.GetLength(0) && newY >= 0 && newY < Map.GetLength(1))
        {
            nextCell = Map[newX, newY];
            return true;
        }

        return false;
    }

    public bool CheckWinCondition()
    {
        int width = Map.GetLength(0);
        int height = Map.GetLength(1);
        for (int row = 0; row < width; row++)
        {
            for (int column = 0; column < height; column++)
            {
                if (Map[row, column].BlockType == BlockType.Empty)
                {
                    Debug.Log($"Check win fail on position: ({row}, {column}");
                    return false;
                }
            }
        }
        Debug.Log("Win");
        return true;
    }

    public bool CheckLoseCondition()
    {
        if (Head.X > 0 && Map[Head.X - 1, Head.Y].BlockType == BlockType.Empty) //neu ben trai la empty thi chua thua
            return false;
        if (Head.X < Map.GetLength(0) && Map[Head.X + 1, Head.Y].BlockType == BlockType.Empty) // phai
            return false;
        if (Head.Y > 0 && Map[Head.X, Head.Y - 1].BlockType == BlockType.Empty) //duoi
            return false;
        if (Head.Y < Map.GetLength(1) && Map[Head.X, Head.Y + 1].BlockType == BlockType.Empty) //tren
            return false;
        Debug.Log("Lose");
        return true; // neu ca 4 huong deu khong co empty thi thua
    }
}
