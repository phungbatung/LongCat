using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public interface ICellHandler
{
    LevelHandler LevelHandler { get; set; }
    Cell CurrentCell { get; set; }
    void Setup(LevelHandler levelHandler, Cell currentCell);
    bool CanMove();
    bool CanMoveNext();
    List<IMovementHandler> GetMovementHandlers();
}
