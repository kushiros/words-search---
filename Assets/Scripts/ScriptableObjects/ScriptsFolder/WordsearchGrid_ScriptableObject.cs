using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] wordsContainers[] actualWordsList;
    private System.Random random = new System.Random();
    public void SetGrid(CellPosition[][] _grid)
    {
        
        grid = _grid;
    }

    public void AddWordToList(String _word)
    {
        words.Add(_word);
    }
    public void SetList(List<string> _words)
    {
       
        words = _words;
    }
    public void ResetWordsList()
    {
        words.Clear();
        _firstCellOfTheWords.Clear();
        _lastCellOfTheWords.Clear();
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
    public void SearchWordIRemplaced(string word) { 
        foreach(wordsContainers words in actualWordsList)
        {
            if(words.GetWord() == word)
            {
                wordsContainers wordIRemplaced =words.GetWordIRemplaced();

                Debug.Log("yo había reemplazado a " +wordIRemplaced);
                UpdatGrid(words);
                UpdateGridWithTheRemplaceWord(wordIRemplaced);
            }
        }
    }

    private void UpdatGrid(wordsContainers wordFound)
    {
        for(int i = wordFound.GetMinX(); i <= wordFound.GetMaxX(); i++)
        {
            for(int j = wordFound.GetMinY(); j <= wordFound.GetMaxY(); j++)
            {
                grid[i][j].SetLetter((char)('A' + random.Next(26)));
            }
        }

    }
    private void UpdateGridWithTheRemplaceWord(wordsContainers wordToReplace) {
        int startX = wordToReplace.GetFirstCellPositionX();
        int startY = wordToReplace.GetFirstCellPositionY();
        int endX = wordToReplace.GetLastCellPositionX();
        int endY = wordToReplace.GetLastCellPositionY();
        string word = wordToReplace.GetWord();

        // Escribe la palabra a reemplazar
        if (startX == endX)
        {
            // La palabra es vertical
            int step = startY < endY ? 1 : -1; // Determina la dirección del recorrido
            for (int i = 0, y = startY; i < word.Length; i++, y += step)
            {
                grid[startX][y].SetLetter(word[i]);
            }
        }
        else
        {
            // La palabra es horizontal
            int step = startX < endX ? 1 : -1; // Determina la dirección del recorrido
            for (int i = 0, x = startX; i < word.Length; i++, x += step)
            {
                grid[x][startY].SetLetter(word[i]);
            }
        }
    }


}
