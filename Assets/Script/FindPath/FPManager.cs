using System;
using System.Collections.Generic;
using UnityEngine;

public class FPManager
{
    public Stack<FPMapState> stateStack = new Stack<FPMapState>();
    public FPManager(LevelHandler levelHanler)
    {
        stateStack.Push(new FPMapState(levelHanler.Map, levelHanler.Head));
    }

    public List<Direction> CalculateFinalPath()
    {
        List<Direction> result = new List<Direction>();

        while (stateStack.Count > 0)
        {
            FPMapState currentState = stateStack.Pop();
            if (currentState.CheckWinCondition())
            {
                while(currentState.preMoveDirection != Direction.None)
                {
                    result.Add(currentState.preMoveDirection);
                    currentState = currentState.OldMapState;
                }
                //Debug.LogError("Đã tìm ra cách để chiến thắng màn chơi này!!!");
                return result;
            }    
            if (currentState.CheckLoseCondition())
            {
                continue;
            }

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (currentState.CanMove(dir))
                {
                    FPMapState newState = currentState.Clone();
                    newState.MoveInDirection(dir);
                    stateStack.Push(newState);
                }    
            }
        }
        Debug.LogError("Không có cách để chiến thắng màn chơi này!!!");
        return result;
    }

}
