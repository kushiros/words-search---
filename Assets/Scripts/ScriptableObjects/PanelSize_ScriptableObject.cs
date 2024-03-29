using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PanelSize_ScriptableObject : ScriptableObject
{
    [SerializeField] private int _width; 
    [SerializeField] private int _height;
    [SerializeField] private int wordSearchXSize = 10;
    [SerializeField] private int wordSearchYSize = 10;

    public void GetGridSize(int _widthSize,int _heightSize)
    {
        _width = _widthSize;
        _height = _heightSize;
    }
    public Vector2 CalculateVector2() {

        float widthCellSizeX = _width / (float) wordSearchXSize;
        float widthCellSizeyY = _height /(float) wordSearchYSize;
        Vector2 gridSize = new Vector2(widthCellSizeX, widthCellSizeyY);
        return gridSize;
    }


}
