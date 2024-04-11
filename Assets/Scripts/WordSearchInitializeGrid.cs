using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

using Color = UnityEngine.Color;

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
        for (int i = 0; i < words.Count; i++)
        {
            bool placed = false;
            while (!placed)
            {
                int row = random.Next(rows);
                int col = random.Next(cols);
                int direction = random.Next(4); // 0: horizontal, 1: vertical, 2: vertical hacia arriba, 3: horizontal hacia la izquierda

                if (CanPlaceWord(words[i], row, col, direction))
                {
                    PlaceWord(words[i], row, col, direction);
                    placed = true;
                }
            }

            // Llama a ModifyPreviousWords cada dos palabras
            if ((i + 1) % 2 == 0)
            {
                ModifyPreviousWords();
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
                    grid[row][col + i].SetColor(UnityEngine.Color.red);
                }
                break;
            case 1: // Vertical hacia abajo
                firstCell = grid[row][col];
                lastCell = grid[row + word.Length - 1][col];
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row + i][col].SetLetter(word[i]);
                    grid[row + i][col].SetColor(UnityEngine.Color.red);
                }
                break;
            case 2: // Vertical hacia arriba
                firstCell = grid[row][col];
                lastCell = grid[row - word.Length + 1][col];
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row - i][col].SetLetter(word[i]);
                    grid[row - i][col].SetColor(UnityEngine.Color.red);
                }
                break;
            case 3: // Horizontal hacia la izquierda
                firstCell = grid[row][col];
                lastCell = grid[row][col - word.Length + 1];
                for (int i = 0; i < word.Length; i++)
                {
                    grid[row][col - i].SetLetter(word[i]);
                    grid[row][col - i].SetColor(UnityEngine.Color.red);
                }
                break;
        }

        if (firstCell != null && lastCell != null)
        {
            _gridScriptableObject.SetFirstCellOfTheWord(firstCell);
            _gridScriptableObject.SetLastCellOfTheWord(lastCell);
        }
    }

    private void ModifyPreviousWords()
    {
        int wordCount = _gridScriptableObject.GetWordsList().Count;
        if (wordCount >= 3)
        {
            CellPosition firstCellOfLastWord = _gridScriptableObject.GetFirstCellOfWord(wordCount - 1);
            CellPosition lastCellOfSecondLastWord = _gridScriptableObject.GetLastCellOfWord(wordCount - 2);
            string newWord = _gridScriptableObject.GetWordsList()[wordCount - 1];


            CellPosition affectedWordCell = (wordCount % 2 == 0) ? lastCellOfSecondLastWord : firstCellOfLastWord;


            int intersectRow = affectedWordCell.GetPositionY();
            int intersectCol = affectedWordCell.GetPositionX();


            for (int i = 0; i < newWord.Length; i++)
            {
                int col = intersectCol + i;

                if (col >= 0 && col < grid[0].Length)
                {
                    grid[intersectRow][col].GetcharController().SetChar(newWord[i]);
                    grid[intersectRow][col].SetColor(UnityEngine.Color.blue); 
                }
            }
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
