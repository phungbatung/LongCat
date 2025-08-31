using UnityEngine;
using UnityEngine.EventSystems;

public class FPMapState
{
    public LevelHandler LevelHandler { get; private set; }

    public FPMapState OldMapState { get; private set; }

    public Direction preMoveDirection;
    public FPMapState(LevelHandler levelHandler)
    {
        LevelHandler = levelHandler.Clone();
    }

    public FPMapState(FPMapState mapState)
    {
        OldMapState = mapState;
        LevelHandler = mapState.LevelHandler.Clone();
    }



    public FPMapState Clone()
    {
        return new FPMapState(this);
    }
}
