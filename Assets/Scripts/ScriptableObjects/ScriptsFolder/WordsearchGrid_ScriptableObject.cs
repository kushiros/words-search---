using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu]
public class WordsearchGrid_ScriptableObject : ScriptableObject
{
    public bool change;
    [SerializeField] CellPosition[][] grid;
    [SerializeField] private List<string> words;
    public event Action<string> OnWordFound;
    [SerializeField] List<CellPosition> _firstCellOfTheWords;
    [SerializeField] List<CellPosition> _lastCellOfTheWords;
    

    public void SetGrid(CellPosition[][] _grid)
    {
        
        grid = _grid;
    }
    public void SetList(List<string> _words)
    {
       
        words = _words;
    }
    public CellPosition[][] GetGrid() 
    { 
       return grid; 
        
    }
    public bool getChange() {  return change; }

    public bool CheckWordsListWithWordObtained(string _string)
    {
        bool toReturn = false;
        foreach (string word in words)
        {
            if (word == _string)
            {
                OnWordFound?.Invoke(_string);
                
                return true;
            }
        }
        return toReturn;
    }

    public List<string>  GetWordsList()
    {
        return words;
    }
    public void ReturnColorToWhite()
    {
        Color color = new Color(254f/255f, 228f/255f, 197f/255f);
        for (int i = 0;(grid.Length) > i;i++)
        {
            for(int j = 0;j < (grid[0].Length);j++) {
                grid[i][j].SetColor(color);
            }
        }
    }
    public void SetFirstCellOfTheWord(CellPosition _cellPosition)
    {
        _firstCellOfTheWords.Add(_cellPosition);
    }
    public void SetLastCellOfTheWord(CellPosition _cellPosition)
    {
        _lastCellOfTheWords.Add(_cellPosition);
    }
    public CellPosition GetFirstCellOfWord(int index)
    {
        if (index >= 0 && index < _firstCellOfTheWords.Count)
        {
            return _firstCellOfTheWords[index];
        }
        return null;
    }

    public CellPosition GetLastCellOfWord(int index)
    {
        if (index >= 0 && index < _lastCellOfTheWords.Count)
        {
            return _lastCellOfTheWords[index];
        }
        return null;
    }


}
