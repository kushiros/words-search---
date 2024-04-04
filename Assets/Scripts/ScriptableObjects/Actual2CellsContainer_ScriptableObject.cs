using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actual2CellsContainer_ScriptableObject : ScriptableObject
{
    [SerializeField] CellPosition firstClickPosition, toCheckPosition;
    [SerializeField] int firstClickX,firstClickY,toCheckPositionX,toCheckPositionY;
    [SerializeField] bool check_X_if_True_Y_If_False;

    public void SetFirstClickPosition(CellPosition cellPosition)
    {
        firstClickPosition = cellPosition;
        firstClickX = firstClickPosition.GetPositionX();
        firstClickY =  firstClickPosition.GetPositionY();
    }

    public bool CheckThisPositionWithFirst(CellPosition cellPosition)
    {
        if (firstClickPosition == null) return false;
        if (toCheckPosition == null) return false;
        if (firstClickX == cellPosition.GetPositionX()){
            return true;
        }
        return true;
    }
}
