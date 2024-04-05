using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
[CreateAssetMenu]

public class Actual2CellsContainer_ScriptableObject : ScriptableObject
{
    [SerializeField] CellPosition firstClickPosition, toCheckPosition;
    [SerializeField] int firstClickX,firstClickY,toCheckPositionX,toCheckPositionY;
    [SerializeField] bool check_X_if_True_Y_If_False;

    
    private int _totalCellsChecked;
    public event Action<char> OnCharEvent;
    public event Action OnResetEvent;



    public void SetFirstClickPosition(CellPosition cellPosition)
    {
        firstClickPosition = cellPosition;
        firstClickX = firstClickPosition.GetPositionX();
        firstClickY =  firstClickPosition.GetPositionY();
        OnCharEvent?.Invoke(cellPosition.GetLetter());
        
    }

    public bool CheckThisPositionWithFirst(CellPosition cellPosition)
    {
        if (firstClickPosition == null) return false;
        if (cellPosition == null) return false;
        if (firstClickX == cellPosition.GetPositionX() || firstClickY == cellPosition.GetPositionY())
        {
            check_X_if_True_Y_If_False = true;
            _totalCellsChecked++;
            OnCharEvent?.Invoke(cellPosition.GetLetter());
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
        _totalCellsChecked = 0;
        OnResetEvent?.Invoke();

    }
    public int GetTotalCellsChecked()
    {
        return _totalCellsChecked;
    }
    private void SetColorMaterial()
    {

    }
}
