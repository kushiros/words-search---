using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

using Color = UnityEngine.Color;

public class WordSearchInitializeGrid : MonoBehaviour
{
    private CellPosition[][] grid;
    [SerializeField] protected List<string> words = new List<string> { "apple", "banana", "cherry" };
    [SerializeField] private int lastWordModifiedIndex = -1;
    private List<string> selectedWords;
    [SerializeField] PanelSize_ScriptableObject panelSize;
    int rows = 10;
    int cols = 10;
    private System.Random random = new System.Random();
    private bool isFirstTimeInThisPair = false;
    [SerializeField] wordsContainers[] actualWordsList;
    public GameObject cellPrefab;
    [SerializeField]private int actualword;
    private int numberofActualPair;
    
    [SerializeField] WordsearchGrid_ScriptableObject _gridScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        actualword = 0;
        numberofActualPair = 0;
        foreach (wordsContainers word in actualWordsList)
        {
            word.Reset();
        }
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            _gridScriptableObject.ResetWordsList();
            panelSize.SetGridSize((int)rectTransform.rect.width, (int)rectTransform.rect.height);
            cols = panelSize.GetColumnAmount();
            rows = panelSize.GetRowsAmount();
                
        }
        /*if(!_gridScriptableObject.getChange())
        {
            grid = _gridScriptableObject.GetGrid();
            return;
        }*/
        InitializeGrid();
        //_gridScriptableObject.SetList(words);
        PlaceWords();
        FillRandomLetters();
        
        
    }



    private void InitializeGrid()
    {
        
        
        grid = new CellPosition[rows][];

        Vector2 cellSize = panelSize.CalculateVector2();
        Vector3 parentPosition = transform.position;
        for (int i = 0; i < rows; i++)
        {
            grid[i] = new CellPosition[cols];
            for (int j = 0; j < cols; j++)
            {

                float xPosition = parentPosition.x + (j+1) * cellSize.x;
                float yPosition = parentPosition.y - (i+1) * cellSize.y;

                Vector3 cellPosition = new Vector3(xPosition, yPosition, parentPosition.z);

                
                GameObject cellGameObject = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
                CellPosition cellPositionComponent = cellGameObject.GetComponent<CellPosition>();
                cellPositionComponent.SetPosition(j, i);
                
                grid[i][j] = cellPositionComponent;
            }
        }
        _gridScriptableObject.SetGrid(grid);
    }
    private void PlaceWords()
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (i < 2)
            {
                bool placed = false;
                while (!placed)
                {
                    int row = random.Next(rows);
                    int col = random.Next(cols);
                    int direction = random.Next(2); // 0: horizontal, 1: vertical, 2: vertical hacia arriba, 3: horizontal hacia la izquierda

                    if (CanPlaceWord(words[i], row, col, direction))
                    {
                        PlaceWord(words[i], row, col, direction);
                        placed = true;
                    }
                }
            }
            else if(i<12)
            {
                ModifyPreviousWords(i);
            }

            
            
        }
    }

    private bool CanPlaceWord(string word, int row, int col, int direction, int startRow = -1, int startCol = -1)
    {
        int r = (startRow == -1) ? row : startRow;
        int c = (startCol == -1) ? col : startCol;

        switch (direction)
        {
            case 0:
                if (col + word.Length > cols) return false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[row][col + i].GetLetter() != '\0' && grid[row][col + i].GetLetter() != word[i])
                        return false;
                }
                break;
            case 1:
             // Horizontal hacia la izquierda
                if (col - word.Length < 0) return false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[row][col - i].GetLetter() != '\0' && grid[row][col - i].GetLetter() != word[i])
                        return false;
                }
                break;
        }



        return true;
    }
    private bool CanFitWordInGrid(string word, int row, int col, int direction)
    {
        switch (direction)
        {
            case 0: // Horizontal hacia la derecha
                if (col + word.Length > cols) return false;
                break;
            case 1: // Vertical hacia abajo
                if (col - word.Length + 1 < 0) return false;
                break;

            case 2: // Vertical hacia arriba
                if (row - word.Length + 1 < 0) return false;
                break;
            case 3: // Horizontal hacia la izquierda
                if (row + word.Length > rows) return false;
                break;
        }

        return true;
    }


    private void PlaceWord(string word, int row, int col, int direction)
    {
        CellPosition firstCell = null;
        CellPosition lastCell = null;

        switch (direction)
        {
            case 0: // Horizontal hacia la derecha
                firstCell = grid[row][col];
                lastCell = grid[row][col + word.Length - 1];
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row][col + i].SetLetter(word[i]);
                    grid[row][col + i].SetColor(Color.red);
                }
                break;
            case 1: // Vertical hacia abajo

                firstCell = grid[row][col];
                lastCell = grid[row][col - word.Length + 1];
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row][col - i].SetLetter(word[i]);
                    grid[row][col - i].SetColor(Color.red);
                }
                break;
        }

        if (firstCell != null && lastCell != null)
        {
            _gridScriptableObject.SetFirstCellOfTheWord(firstCell);
            _gridScriptableObject.SetLastCellOfTheWord(lastCell);
            if (!_gridScriptableObject.GetWordsList().Contains(word))
            {
                _gridScriptableObject.AddWordToList(word);

                actualWordsList[actualword].SetwordsContainers(word,firstCell,lastCell,actualword);
                actualword++;
            }
        }
    }

    private void ModifyPreviousWords(int index)
    {
        if (index >= 2)
        {
            string newWord = words[index];

            // Alterna entre modificar la primera y la segunda palabra del par anterior.
            int wordToModifyIndex = index - 2 ;
            lastWordModifiedIndex = wordToModifyIndex;

            int firstCellX= actualWordsList[wordToModifyIndex].GetFirstCellPositionY();
            int firstCellY = actualWordsList[wordToModifyIndex].GetFirstCellPositionX();
            int lastCellX = actualWordsList[wordToModifyIndex].GetLastCellPositionY();
            int lastCellY = actualWordsList[wordToModifyIndex ].GetLastCellPositionX();


            // Alterna entre horizontal (0) y vertical (1) cada dos palabras
            int direction = numberofActualPair % 2;

            // Encuentra los extremos del rango en el que podemos insertar la palabra
            int minX, minY, maxX, maxY;
            if (firstCellX < lastCellX)
            {
                minX = firstCellX;
                maxX = lastCellX;
            }
            else
            { 
                
                minX = lastCellX;
                maxX = firstCellX;
            }

            if(firstCellY <lastCellY)
            {
                minY = firstCellY;
                maxY = lastCellY;

            }
            else
            {
                minY=lastCellY;
                maxY= firstCellY;
            }

            int randomX = random.Next(minX, maxX);
            int randomY = random.Next(minY, maxY);

            bool entraaca = false;

            if(direction == 0)
            {
                while((randomY + newWord.Length) > 9)
                {
                    randomY--;
                }
                for (int i = 0; i < newWord.Length; i++)
                {

                    CellPosition currentCell = grid[randomX][randomY+i];
                    currentCell.GetcharController().SetChar(newWord[i]);
                    currentCell.SetColor(Color.red);

                    // Establece la primera y la última celda de la palabra
                    if (i == 0)
                    {
                        firstCellX = randomX;
                        firstCellY = randomY + i;
                    }
                    if (i == newWord.Length - 1)
                    {
                        lastCellX = randomX;
                        lastCellY = randomY + i;
                    }
                }
            }

            if (direction == 1)
            {
                while (randomX + newWord.Length > 9)
                {
                    randomX--;
                }
                for (int i = 0; i < newWord.Length; i++)
                {

                    CellPosition currentCell = grid[randomX+i][randomY];
                    currentCell.GetcharController().SetChar(newWord[i]);
                    currentCell.SetColor(Color.blue);

                    // Establece la primera y la última celda de la palabra
                    if (i == 0)
                    {
                        firstCellX = randomX + i;
                        firstCellY = randomY;
                    }
                    if (i == newWord.Length - 1)
                    {
                        lastCellX = randomX + i;
                        lastCellY = randomY;
                    }
                }
            }

            if (index % 2 != 0)
            {
                numberofActualPair++;
            }

            _gridScriptableObject.AddWordToList(newWord);


            SetWordsContainersInfo(newWord,firstCellX, firstCellY, lastCellX, lastCellY,index,(index-2));
            actualword++;
        }
    }
    private void SetWordsContainersInfo(string word,int firstCellX, int firstCellY, int lastCellX, int lastCellY,int index,int wordIRemplaced)
    {
        actualWordsList[actualword].SetFirstCellPositionX(firstCellX);
        actualWordsList[actualword].SetFirstCellPositionY(firstCellY);
        actualWordsList[actualword].SetLastCellPositionX(lastCellX);
        actualWordsList[actualword].SetLastCellPositionY(lastCellY);
        actualWordsList[actualword].SetString(word);
        actualWordsList[actualword].SetIndex(index);
        actualWordsList[actualword].SetWordIRemplaced(actualWordsList[wordIRemplaced]);
    }
    private void FillRandomLetters()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j].GetLetter() == '\0') // Si la celda está vacía
                {
                    grid[i][ j].SetLetter((char)('A' + random.Next(26)));
                }
            }
        }
    }
   
    
 
}
