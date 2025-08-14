using System.Collections.Generic;
using System;

public static class CellControllerFactory
{
    private static readonly Dictionary<CellType, Func<ICellHandler>> _factoryMap =
        new Dictionary<CellType, Func<ICellHandler>>
        {
            { CellType.Empty,     () => new EmptyCellHandler() },
            { CellType.Head,      () => new HeadCellHandler() },
            { CellType.Body,      () => new BodyCellHandler() },
            { CellType.Obstacle,  () => new ObstacleCellHandler() },
            { CellType.StopPoint, () => new StopPointCellHandler() }
        };

    public static ICellHandler GetCellHandlerByCellType(CellType type)
    {
        if (_factoryMap.TryGetValue(type, out var creator))
        {
            return creator();
        }

        throw new ArgumentException($"Unsupported CellType: {type}");
    }
}
