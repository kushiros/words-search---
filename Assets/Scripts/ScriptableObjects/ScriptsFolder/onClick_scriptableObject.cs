using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="holapadre/hola")]
public class onClick_scriptableObject : ScriptableObject
{
    [SerializeField] bool waitForEndbutton =false;
    private CellPosition actualCellPosition;
    private int ActualX, ActualY;
    private List<CellPosition> _listCellPosition;
    public void ChangeWaitForEndButtonToFalse() { waitForEndbutton = false; }
    public void ChangeWaitForEndButtonToTrue(CellPosition _cellPosition)
    {
        waitForEndbutton = true;
        actualCellPosition = _cellPosition;
        ActualX = _cellPosition.GetPositionX();
        ActualY = _cellPosition.GetPositionY();
    }

    public bool GetWaitForEndButton() {  return waitForEndbutton; }

    public int GetActualX()
    {
        return ActualX;
    }
    public int GetActualY()
    {
        return ActualY;
    }
    public void AddToListCellPosition(CellPosition cellPosition)
    {
        _listCellPosition.Add(cellPosition);
    }
    public void DeleteAllListCellPosition()
    {
        Debug.Log("llegue al deletealllistcellcontroller");
        foreach(CellPosition cellPosition in _listCellPosition)
        {
            Debug.Log("ayuda");
            cellPosition.GetcharController().ReturnToOriginalColor();
        }
        _listCellPosition.Clear();
    }
}
