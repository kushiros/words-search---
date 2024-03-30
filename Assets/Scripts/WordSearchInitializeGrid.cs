using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class WordSearchInitializeGrid : MonoBehaviour
{
    private CellPosition[][] grid;
    [SerializeField] private List<string> words = new List<string> { "apple", "banana", "cherry" };
    private List<string> selectedWords;
    [SerializeField] PanelSize_ScriptableObject panelSize;
    int rows = 10;
    int cols = 10;
    private System.Random random = new System.Random();

    public GameObject cellPrefab;

    private WordSelection currentSelection;
    [SerializeField] WordsearchGrid_ScriptableObject _gridScriptableObject;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            panelSize.GetGridSize((int)rectTransform.rect.width, (int)rectTransform.rect.height);
        }
        /*if(!_gridScriptableObject.getChange())
        {
            grid = _gridScriptableObject.GetGrid();
            return;
        }*/
        InitializeGrid();
        _gridScriptableObject.SetList(words);
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
        foreach (string word in words)
        {
            bool placed = false;
            while (!placed)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);
                int direction = random.Next(4); // 0: horizontal, 1: vertical

                if (CanPlaceWord(word, row, col, direction))
                {
                    PlaceWord(word, row, col, direction);
                    placed = true;
                }
            }
        }
    }
    private bool CanPlaceWord(string word, int row, int col, int direction)
    {
        switch(direction){
            case 0:
                if (col + word.Length > cols) return false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[row][col + i].GetLetter() != '\0' && grid[row][col + i].GetLetter() != word[i])
                        return false;
                }
                break;
            case 1:
                if (row + word.Length > rows) return false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[row + i][col].GetLetter() != '\0' && grid[row + i][col].GetLetter() != word[i])
                        return false;
                }
                break;
            case 2: // Vertical hacia arriba
                if (row - word.Length < 0) return false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (grid[row - i][col].GetLetter() != '\0' && grid[row - i][col].GetLetter() != word[i])
                        return false;
                }
                break;
            case 3: // Horizontal hacia la izquierda
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

    private void PlaceWord(string word, int row, int col, int direction)
    {
        switch (direction)
        {
            case 0: // Horizontal hacia la derecha
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row][col + i].SetLetter(word[i]);
                    grid[row][col + i].SetColor(UnityEngine.Color.red);
                }
                break;
            case 1: // Vertical hacia abajo
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row + i][col].SetLetter(word[i]);
                    grid[row + i][col].SetColor(UnityEngine.Color.red);
                }
                break;
            case 2: // Vertical hacia arriba
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row - i][col].SetLetter(word[i]);
                    grid[row - i][col].SetColor(UnityEngine.Color.red);
                }
                break;
            case 3: // Horizontal hacia la izquierda
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row][col - i].SetLetter(word[i]);
                    grid[row][col - i].SetColor(UnityEngine.Color.red);
                }
                break;
        }
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
