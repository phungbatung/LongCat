using System;
using UnityEngine;

[Serializable]
public class Cell
{
    public int X;
    public int Y;
    public float HorPos;
    public float VerPos;
    public CellType BlockType;

    public Cell(int x, int y,float horPos, float verPos, CellType blockType)
    {
        X = x;
        Y = y;
        HorPos = horPos;
        VerPos = verPos;
        BlockType = blockType;
    }   

    public Vector3 GetPosition()
    {
        return new Vector3(HorPos, 0.5f, VerPos);
    }    

    public Cell Clone()
    {
        return new Cell(X, Y, HorPos, VerPos, BlockType);
    }
}
