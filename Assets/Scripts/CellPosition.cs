using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellPosition : MonoBehaviour, IPointerEnterHandler
{
    private char actualChar;
    [SerializeField]private CharController charController;
    [SerializeField] private Image image;
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;
    private bool _firstClick;
    [SerializeField]onClick_scriptableObject onClick;
    [SerializeField] Actual2CellsContainer_ScriptableObject act;
    [SerializeField] private RotateToTheMouse _rotateToTheMouse;

    void Start()
    {
        _rotateToTheMouse = GetComponentInChildren<RotateToTheMouse>();
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
    public void StartRotation(Vector3 _position)
    {
        _rotateToTheMouse.StartRotation(_position);
    }
    public void EndRotation()
    {
        _rotateToTheMouse.EndRotation();
    }
    public void SetFirstClickTrue()
    {
        _firstClick = true;
        onClick.AddToListCellPosition(this);
        act.SetFirstClickPosition(this);
    }
    public void SetFirstClickFalse()
    {
        _firstClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!onClick.GetWaitForEndButton()) return;
        if (!act.CheckThisPositionWithFirst(this)) return;

        onClick.AddToListCellPosition(this);
        Debug.Log("ayudame loco");
        charController.ChangeCharColor();
    }
    public CharController GetcharController()
    {
        return charController;
    }

    public void resetColors()
    {
        onClick.DeleteAllListCellPosition();
        act.Reset();
    }
    
}
