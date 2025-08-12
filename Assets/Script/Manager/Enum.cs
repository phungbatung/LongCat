using UnityEngine;

public enum CellType
{
    Empty=0,
    Head=1,
    Body=2,
    Obstacle=3,
    StopPoint=4
}

public enum GameState
{
    WaitingForInput,
    PlayingAnimation,
    Win,
    Lose,
    Pause,
}

public enum Direction
{
    None,
    Left,
    Right,
    Up,
    Down
}
