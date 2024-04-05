using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu]
public class WordSelection : ScriptableObject
{
    private int _xStartPosition, _yStartPosition;
    private int _xEndPosition, _yEndPosition;
    private CellPosition startCell, endCell;
    private string CurrentWord = "";
    [SerializeField] WordsearchGrid_ScriptableObject _gridScriptableObject;
    

    

    public void SetStartPoint(CellPosition point)
    {
        startCell = point;
        point.GetcharController().ChangeCharColor();
        _xStartPosition = point.GetPositionX();
        _yStartPosition = point.GetPositionY();
        Vector3 _position = point.transform.position;
        point.StartRotation(_position);
        point.SetFirstClickTrue();
    }
    public void SetEndPoint(CellPosition point)
    {
        
        endCell = point;
        
        _xEndPosition = point.GetPositionX();
        _yEndPosition = point.GetPositionY();
        
        bool _toReturn = CheckPosibleSelectedWord();
        startCell.EndRotation(_toReturn);
        startCell.SetFirstClickFalse();
        startCell.resetColors();
        Reset();

    }
    public string GetSelectedWord()
    {
        return "";
    }
    public void Reset()
    {
        _xEndPosition = 0;
        _yEndPosition = 0;
        _xStartPosition= 0;
        _yStartPosition= 0;
        CurrentWord = "";
    }
    private bool CheckPosibleSelectedWord()
    {
        bool _toReturn = false;
        if (_xStartPosition == _xEndPosition)
        {
            _toReturn = CheckYAxis();
        }
        else if (_yStartPosition == _yEndPosition)
        {

            _toReturn = CheckXAxis();
        }

        
        return _toReturn;

    }
    private bool CheckXAxis()
    {
        bool _toReturn = false;
        if(_xStartPosition <  _xEndPosition)
        {
            for (int i = _xStartPosition; i <= _xEndPosition; i++)
            {
                CellPosition[][] _grid = _gridScriptableObject.GetGrid();
                CurrentWord += _grid[_yStartPosition][i].GetLetter();
            }
        }
        else
        {
            for (int i = _xStartPosition; i >= _xEndPosition; i--)
            {
                CellPosition[][] _grid = _gridScriptableObject.GetGrid();
                CurrentWord += _grid[_yStartPosition][i].GetLetter();
                Debug.Log(CurrentWord);
            }
        }
        
        if (_gridScriptableObject.CheckWordsListWithWordObtained(CurrentWord))
        {
            _toReturn = true;
            
        }

        return _toReturn;
    }
    private bool CheckYAxis()
    {
        bool _toReturn = false;
        if (_yStartPosition < _yEndPosition)
        {
            for (int i = _yStartPosition; i <= _yEndPosition; i++)
            {
                CellPosition[][] _grid = _gridScriptableObject.GetGrid();
                CurrentWord += _grid[i][_xStartPosition].GetLetter();

            }
        }
        else
        {
            for (int i = _yStartPosition; i >= _yEndPosition; i--)
            {
                CellPosition[][] _grid = _gridScriptableObject.GetGrid();
                CurrentWord += _grid[i][_xStartPosition].GetLetter();
                Debug.Log(CurrentWord);
            }
        }
        Debug.Log(CurrentWord);
        if (_gridScriptableObject.CheckWordsListWithWordObtained(CurrentWord))
        {

            _toReturn= true;
        }
        return _toReturn;

    }

    
}
