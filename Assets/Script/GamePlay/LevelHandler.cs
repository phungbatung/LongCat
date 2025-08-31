using System.Collections.Generic;
using UnityEngine;

public class LevelHandler
{ 
    
    public Cell[,] Map { get; private set; }

    public Cell Head { get; set; }

    public LevelHandler(int[,] mapData)
    {
        LoadData(mapData);
    }
    public LevelHandler(LevelHandler levelHandler)
    {
        Map = new Cell[levelHandler.Map.GetLength(0), levelHandler.Map.GetLength(1)];
        for (int row = 0; row < levelHandler.Map.GetLength(0); row++)
        {
            for (int column = 0; column < levelHandler.Map.GetLength(1); column++)
            {
                Map[row, column] = levelHandler.Map[row, column].Clone();
                Map[row, column].CellHandler.Setup(this, Map[row, column]);
                if (Map[row, column].CellType == CellType.Head)
                    Head = Map[row, column];
            }
        }
    }

    public void LoadData(int[,] mapData)
    {
        int width = mapData.GetLength(0);
        int height = mapData.GetLength(1);
        Map = new Cell[width, height];
        float offSetX = -(width-1) * 1.0f / 2f;
        float offSetY = (height-1) * 1.0f / 2f;
        //Debug.Log($"OffSetX: {offSetX}, OffSetY: {offSetY}");
        for (int row = 0; row < width; row++)
        {
            for (int column = 0; column < height; column++)
            {
                var cell = new Cell(row, column, column + offSetX, offSetY - row, (CellType)mapData[row, column]);
                cell.CellHandler.Setup(this, cell);
                Map[row, column] = cell;
                if(cell.CellType == CellType.Head)
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
            return nextCell.CellHandler.CanMove();
        }
        return false;
    }

    public Queue<IMovementHandler> MoveInDirection(Direction direction)
    {
        Queue<IMovementHandler> movementQueue = new Queue<IMovementHandler>();

        List<IMovementHandler> movementHandlerList;
        while (CanMove(direction))
        {
            bool canMoveNext = true;
            if (GetNextCellAtDirection(Head, direction, out Cell nextCell))
            {
                movementHandlerList = nextCell.CellHandler.GetMovementHandlers();
                foreach (var movementHandler in movementHandlerList)
                {
                    movementQueue.Enqueue(movementHandler);
                    canMoveNext = movementHandler.DestinationCell.CellHandler.CanMoveNext();
                    Head.SetCellType(CellType.Body);
                    Head = movementHandler.DestinationCell;
                    Head.SetCellType(CellType.Head);
                }
                if (!canMoveNext)
                    break;
            }
            
        }    

        return movementQueue;
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
                if (Map[row, column].CellType == CellType.Empty || Map[row, column].CellType == CellType.StopPoint)
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
        if (CanMove(Direction.Left)) //neu ben trai la empty thi chua thua
            return false;
        if (CanMove(Direction.Right)) // phai
            return false;
        if (CanMove(Direction.Down)) //duoi
            return false;
        if (CanMove(Direction.Up)) //tren
            return false;
        Debug.Log("Lose");
        return true; // neu ca 4 huong deu khong co empty thi thua
    }

    public LevelHandler Clone()
    {
        return new LevelHandler(this);
    }
}
