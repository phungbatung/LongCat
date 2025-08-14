using System.Collections.Generic;
using UnityEngine;

public class StopPointCellHandler : ICellHandler
{
    public Cell CurrentCell { get; set; }
    public LevelHandler LevelHandler { get; set; }

    public bool CanMove()
    {
        return true;
    }

    public bool CanMoveNext()
    {
        return false;
    }

    public List<IMovementHandler> GetMovementHandlers()
    {
        var listMovementHandler = new List<IMovementHandler>();

        var movementHandler = new EmptyMovementHanlder();
        movementHandler.SetTarget(CurrentCell);

        listMovementHandler.Add(movementHandler);
        return listMovementHandler;
    }


    public void Setup(LevelHandler levelHandler, Cell currentCell)
    {
        LevelHandler = levelHandler;
        CurrentCell = currentCell;
    }
}
