using UnityEngine;
using UnityEngine.EventSystems;

public class FPMapState
{
    public Cell[,] Map { get; private set; }
    public Cell Head { get; private set; }

    public FPMapState OldMapState { get; private set; }

    public Direction preMoveDirection;
    public FPMapState(Cell[,] map, Cell head)
    {
        Map = new Cell[map.GetLength(0), map.GetLength(1)];
        for(int i=0; i< map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Map[i, j] = map[i,j].Clone();
            }
        }

        Head = head.Clone();
        preMoveDirection = Direction.None;
    }

    public FPMapState(FPMapState oldMapState)
    {
        OldMapState = oldMapState;
        Cell[,] map = oldMapState.Map; 
        Cell head = oldMapState.Head;
        Map = new Cell[map.GetLength(0), map.GetLength(1)];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Map[i, j] = map[i, j].Clone();
            }
        }

        Head = head.Clone();
        preMoveDirection = Direction.None;
    }

    public bool CanMove(Direction direction)
    {
        if (direction == Direction.None) return false;
        if (GetNextCellAtDirection(Head, direction, out Cell nextCell))
        {
            //Debug.Log($"Head: (({Head.X}, {Head.Y})");
            //Debug.Log($"Next cell: (({nextCell.X}, {nextCell.Y}), BlockType: {nextCell.BlockType}");
            if (nextCell.BlockType == CellType.Empty)
                return true;
        }
        return false;
    }

    public Cell MoveInDirection(Direction direction)
    {
        preMoveDirection = direction;
        Cell nextCell = Head;
        if (GetNextCellAtDirection(nextCell, direction, out Cell _nextCell))
        {
            nextCell = _nextCell;
        }
        else
        {
            Debug.LogError($"Check CanMove(direction) is true, but cannot find the next cell at direction: {direction}");
            return null;
        }
        while (nextCell.BlockType == CellType.Empty)
        {
            Head.BlockType = CellType.Body;
            Head = nextCell;
            Head.BlockType = CellType.Head;

            if (GetNextCellAtDirection(Head, direction, out _nextCell))
            {
                nextCell = _nextCell;
            }
            else
            {
                break;
            }

        }
        //Debug.Log($"New Head Target: (({Head.X}, {Head.Y})");
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
                if (Map[row, column].BlockType == CellType.Empty)
                {
                    //Debug.Log($"Check win fail on position: ({row}, {column}");
                    return false;
                }
            }
        }
        //Debug.Log("Win");
        return true;
    }

    public bool CheckLoseCondition()
    {
        if (Head.X > 0 && Map[Head.X - 1, Head.Y].BlockType == CellType.Empty) //neu ben trai la empty thi chua thua
            return false;
        if (Head.X < Map.GetLength(0) && Map[Head.X + 1, Head.Y].BlockType == CellType.Empty) // phai
            return false;
        if (Head.Y > 0 && Map[Head.X, Head.Y - 1].BlockType == CellType.Empty) //duoi
            return false;
        if (Head.Y < Map.GetLength(1) && Map[Head.X, Head.Y + 1].BlockType == CellType.Empty) //tren
            return false;
        //Debug.Log("Lose");
        return true; // neu ca 4 huong deu khong co empty thi thua
    }

    public FPMapState Clone()
    {
        return new FPMapState(this);
    }
}
