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
        for (int i = 0;(grid.Length) > i;i++)
        {
            for(int j = 0;j < (grid[0].Length);j++) {
                grid[i][j].SetColor(Color.white);
            }
        }
    }
    
}
