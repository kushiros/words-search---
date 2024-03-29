using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellPosition : MonoBehaviour
{
    private char actualChar;
    [SerializeField]private CharController charController;
    [SerializeField] private Image image;
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public char GetLetter()
    { 
       return charController.GetChar();
    
    }
    public void SetLetter(char letter)
    {
        charController.SetChar(letter);
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
    public void SetPosition(int x,int y)
    {
        positionX = x;
        positionY = y;
    }
    public int GetPositionX()
    {
        return positionX;
    }
    public int GetPositionY()
    {
        return positionY;
    }
}
