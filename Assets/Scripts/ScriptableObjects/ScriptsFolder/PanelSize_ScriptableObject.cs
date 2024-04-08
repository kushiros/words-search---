using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PanelSize_ScriptableObject : ScriptableObject
{
    [SerializeField] private int _width; 
    [SerializeField] private int _height;
    [SerializeField] private float widthCellSize;
    [SerializeField] private float heightCellSize;

    [SerializeField] private int wordSearchXSize = 10;
    [SerializeField] private int wordSearchYSize = 10;
    public Action<float,float> widthCellXYSizeEvent;


    public void SetGridSize(int _widthSize,int _heightSize)
    {
        _width = _widthSize;
        _height = _heightSize;
    }
    public Vector2 CalculateVector2() {

        float widthCellSizeX = _width / (float) wordSearchXSize;
        float widthCellSizeyY = _height /(float) wordSearchYSize;
        Vector2 gridSize = new Vector2(widthCellSizeX, widthCellSizeyY);
        widthCellXYSizeEvent.Invoke(widthCellSizeX,widthCellSizeyY);

        return gridSize;

    }

    public int GetColumnAmount() {
        return wordSearchXSize;
    }
    public int GetRowsAmount()
    {
        return wordSearchYSize;
    }


}
