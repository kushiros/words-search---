using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
[CreateAssetMenu]
public class wordsContainers : ScriptableObject
{
    [SerializeField] string word;

    [SerializeField] int firstCellPositionX;
    [SerializeField] int firstCellPositionY;
    [SerializeField] int lastCellPositionX;
    [SerializeField] int lastCellPositionY;
    [SerializeField] int numberInTheList;
    [SerializeField] wordsContainers wordIRemplaced;
    public void SetwordsContainers(string word, CellPosition firstCellPosition, CellPosition lastCellPosition, int numberInTheList)
    {
        this.word = word;
        this.firstCellPositionX = firstCellPosition.GetPositionX();
        this.firstCellPositionY = firstCellPosition.GetPositionY();
        this.lastCellPositionX = lastCellPosition.GetPositionX();
        this.lastCellPositionY = lastCellPosition.GetPositionY();
        this.numberInTheList = numberInTheList;
    }

    public int GetFirstCellPositionX()
    {
        return firstCellPositionX;
    }
    public int GetFirstCellPositionY()
    {
        return firstCellPositionY;
    }
    public int GetLastCellPositionX()
    {
    return lastCellPositionX;
    }
    public int GetLastCellPositionY()
    {
        return lastCellPositionY;
    }

    public void SetFirstCellPositionX(int x)
    {
        firstCellPositionX = x;
    }
    public void SetFirstCellPositionY(int y)
    {
        firstCellPositionY = y;
    }
    public void SetLastCellPositionX(int x)
    {
        lastCellPositionX = x;
    }
    public void SetLastCellPositionY(int y)
    {
        lastCellPositionY = y;
    }
    public void SetString(string text)
    {
        word = text;
    }
    public void SetIndex(int index)
    {
        numberInTheList = index;
    }
    
    public int GetNumberInTheList() {  return numberInTheList; }

    public string GetWord() { return word; }
    public int GetStringSize() { return word.Length; }
    
    public wordsContainers GetWordIRemplaced() 
    {
        if (wordIRemplaced == null)
        {
            throw new InvalidOperationException("No se puede obtener wordIRemplaced porque es null.");
        }
        return wordIRemplaced;
    }
    public void SetWordIRemplaced(wordsContainers i)
    {
        wordIRemplaced = i;
    }
    
    public void Reset()
    {
        word = string.Empty;
        firstCellPositionX = -1;
        firstCellPositionY = -1;
        lastCellPositionX = -1;
        lastCellPositionY = -1;
        numberInTheList = -1;
    }
    public int GetMinX()
    {
        if(firstCellPositionX < lastCellPositionX) return firstCellPositionX;
        return lastCellPositionX;
    }
    public int GetMaxX()
    {
        if(firstCellPositionX > lastCellPositionX)  return firstCellPositionX;
        return lastCellPositionX;
    }
    public int GetMinY()
    {
        if(firstCellPositionY < lastCellPositionY) return firstCellPositionY;
        return lastCellPositionY;
    }
    public int GetMaxY()
    {
        if (firstCellPositionY > lastCellPositionY) return firstCellPositionY;
        return lastCellPositionY;
    }
}
