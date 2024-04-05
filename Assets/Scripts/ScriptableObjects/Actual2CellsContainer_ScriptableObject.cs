using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]

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
        if (cellPosition == null) return false;
        if (firstClickX == cellPosition.GetPositionX()){
            check_X_if_True_Y_If_False = true;
            return true;
        }
        if(firstClickY == cellPosition.GetPositionY())
        {
            check_X_if_True_Y_If_False= false;
            return true;
        }
        return false;
    }
    public void Reset()
    {
        firstClickPosition = null;
        firstClickX = 0; 
        firstClickY = 0;
        toCheckPosition = null;
        toCheckPositionX = 0;
        toCheckPositionY = 0;

    }
}
