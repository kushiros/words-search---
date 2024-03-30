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
    private string CurrentWord = "";
    [SerializeField] WordsearchGrid_ScriptableObject _gridScriptableObject;
    [SerializeField] Color[] _color = new Color[4];
    

    public void SetStartPoint(CellPosition point)
    {
        _xStartPosition = point.GetPositionX();
        _yStartPosition = point.GetPositionY();
    }
    public void SetEndPoint(CellPosition point)
    {
        _xEndPosition = point.GetPositionX();
        _yEndPosition = point.GetPositionY();
        CheckPosibleSelectedWord();

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
    private void CheckPosibleSelectedWord()
    {
        if (_xStartPosition == _xEndPosition)
        {
            CheckYAxis();
        }
        else if (_yStartPosition == _yEndPosition)
        {
            
            CheckXAxis();
        }
        Reset();

    }
    private void CheckXAxis()
    {

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
            
            HighlightWordXAxis();
        }
    }
    private void CheckYAxis()
    {

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
            
            HighlightWordYAxys();
        }
        
    }
    private void HighlightWordYAxys()
    {
        int random = GetRandomInt();

        for (int i = _yStartPosition; i <= _yEndPosition; i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[i][_xStartPosition].SetColor(GetRandomColor(random));
        }
        for (int i = _yStartPosition; i >= _yEndPosition; i--)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[i][_xStartPosition].SetColor(GetRandomColor(random));
        }

    }
    private void HighlightWordXAxis() 
    {
        int random = GetRandomInt();

        for (int i = _xStartPosition; i <= _xEndPosition; i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[_yStartPosition][i].SetColor(GetRandomColor(random));
        }
        for (int i = _xStartPosition; i >= _xEndPosition; i--)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[_yStartPosition][i].SetColor(GetRandomColor(random));
        }
    }
    private int GetRandomInt()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0,4);
        return randomNumber;
    }
    private Color GetRandomColor(int i)
    {
        return _color[i];
    }
}
