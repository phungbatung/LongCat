using System.Collections.Generic;
using UnityEngine;

public class ObstacleCellHandler : ICellHandler
{
    public Cell CurrentCell { get; set; }
    public LevelHandler LevelHandler { get; set; }


    public bool CanMove()
    {
        return false;
    }

    public List<IMovementHandler> GetMovementHandlers()
    {
        return new List<IMovementHandler>();
    }

    public bool CanMoveNext()
    {
        return false;
    }

    public void Setup(LevelHandler levelHandler, Cell currentCell)
    {
        LevelHandler = levelHandler;
        CurrentCell = currentCell;
    }
}
