using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu]
public class WordSelection : ScriptableObject
{
    private CellPosition startPoint, endPoint;
    private string CurrentWord = "";
    [SerializeField] WordsearchGrid_ScriptableObject _gridScriptableObject;
    [SerializeField] Color[] _color = new Color[4];
    

    public void SetStartPoint(CellPosition point)
    {
        startPoint = point;
    }
    public void SetEndPoint(CellPosition point)
    {
        endPoint = point;
        CheckPosibleSelectedWord();

    }
    public string GetSelectedWord()
    {
        return "";
    }
    public void Reset()
    {
        startPoint = null;
        endPoint = null;
        CurrentWord = "";
    }
    private void CheckPosibleSelectedWord()
    {
        if (startPoint == null) return;
        if (startPoint.GetPositionX() == endPoint.GetPositionX())
        {
            CheckYAxis();
        }
        else if (startPoint.GetPositionY() == endPoint.GetPositionY())
        {
            
            CheckXAxis();
        }
        Reset();

    }
    private void CheckXAxis()
    {
        int _yAxis = startPoint.GetPositionY();
        for (int i = startPoint.GetPositionX(); i <= endPoint.GetPositionX(); i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            CurrentWord += _grid[_yAxis][i].GetLetter();
        }
        if (_gridScriptableObject.CheckWordsListWithWordObtained(CurrentWord))
        {
            Debug.Log(CurrentWord);
            HighlightWordXAxis();
        }
    }
    private void CheckYAxis()
    {
        int _xAxis = startPoint.GetPositionX();
        for (int i = startPoint.GetPositionY(); i <= endPoint.GetPositionY(); i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            CurrentWord += _grid[i][_xAxis].GetLetter();
        }
        if (_gridScriptableObject.CheckWordsListWithWordObtained(CurrentWord))
        {
            Debug.Log(CurrentWord);
            HighlightWordYAxys();
        }
        
    }
    private void HighlightWordYAxys()
    {
        int random = GetRandomInt();
        int _xAxis = startPoint.GetPositionX();
        for (int i = startPoint.GetPositionY(); i <= endPoint.GetPositionY(); i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[i][_xAxis].SetColor(GetRandomColor(random));
        }
    }
    private void HighlightWordXAxis() 
    {
        int random = GetRandomInt();
        int _yAxis = startPoint.GetPositionY();
        for (int i = startPoint.GetPositionX(); i <= endPoint.GetPositionX(); i++)
        {
            CellPosition[][] _grid = _gridScriptableObject.GetGrid();
            _grid[_yAxis][i].SetColor(GetRandomColor(random));
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
