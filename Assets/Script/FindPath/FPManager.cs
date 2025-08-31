using System;
using System.Collections.Generic;
using UnityEngine;

public class FPManager
{
    public Stack<FPMapState> stateStack = new Stack<FPMapState>();
    public FPManager(LevelHandler levelHanler)
    {
        stateStack.Push(new FPMapState(levelHanler));
    }

    public List<Direction> CalculateFinalPath()
    {
        List<Direction> result = new List<Direction>();

        while (stateStack.Count > 0)
        {
            FPMapState currentState = stateStack.Pop();
            if (currentState.LevelHandler.CheckWinCondition())
            {
                while(currentState.preMoveDirection != Direction.None)
                {
                    result.Add(currentState.preMoveDirection);
                    currentState = currentState.OldMapState;
                }
                //Debug.LogError("Đã tìm ra cách để chiến thắng màn chơi này!!!");
                return result;
            }    
            if (currentState.LevelHandler.CheckLoseCondition())
            {
                continue;
            }

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (currentState.LevelHandler.CanMove(dir))
                {
                    FPMapState newState = currentState.Clone();
                    newState.preMoveDirection = dir;
                    newState.LevelHandler.MoveInDirection(dir);
                    stateStack.Push(newState);
                }    
            }
        }
        Debug.LogError("Không có cách để chiến thắng màn chơi này!!!");
        return result;
    }

}
