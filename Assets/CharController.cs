using System;
using TMPro;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private char actualChar;
    [SerializeField]private TextMeshProUGUI textMesh;

    void Start()
    {
        //generateRandomChar();
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateTextMesh();
    }
    private void generateRandomChar()
    {
        
        System.Random random = new System.Random();
        
        int randomInt = random.Next(0, 26);

        //actualChar = (char)('A' + randomInt);
        actualChar = '\0';
    }
   
    private void UpdateTextMesh()
    {
        if (textMesh != null)
        {
            textMesh.text = actualChar.ToString();
        }
    }
    public void SetChar(char c)
    {
        actualChar = c;
        UpdateTextMesh();
    }
    public char GetChar()
    {
        return actualChar;
    }
}

