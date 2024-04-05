using System;
using TMPro;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private char actualChar;
    private Color originalColor;
    [SerializeField]private TextMeshProUGUI textMesh;

    void Start()
    {
        originalColor = textMesh.color;
        //generateRandomChar();
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateTextMesh();
    }
    private void generateRandomChar()
    {
        
        System.Random random = new System.Random();
        
        int randomInt = random.Next(0, 26);

        //actualChar = (char)('A' + randomInt);
        
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
    public void ChangeCharColor()
    {
        
        LeanTween.move(this.gameObject, this.transform.position, 0.15f).setOnComplete(() =>
        {
            textMesh.color = Color.white;
        });
        
    }

    public void ReturnToOriginalColor()
    {
        textMesh.color = originalColor;
    }

}

