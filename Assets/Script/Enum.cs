using UnityEngine;

public enum BlockType
{
    Empty=0,
    Head=1,
    Body=2,
    Obstacle=3,
    WallLeft=4,
    WallRight=5,
    WallBack=6,
    WallForward=7,
    WallLeftBack=8,
    WallLeftForward=9,
    WallRightBack=10,
    WallRightForward=11,
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
