using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private WordSelection _wordSelection;
    private CellPosition _cellPosition;
    [SerializeField] onClick_scriptableObject ScriptableObject;
    private void Start()
    {
        _cellPosition = gameObject.GetComponent<CellPosition>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ScriptableObject == null) return;
        if (!ScriptableObject.GetWaitForEndButton())
        {
            SetStartButton();
        }
        else { SetEndButton(); }
        

    }

    private void SetStartButton()
    {
        _wordSelection.SetStartPoint(_cellPosition);
        ScriptableObject.ChangeWaitForEndButtonToTrue(GetComponent<CellPosition>());
        
    }

    private void SetEndButton()
    {
        ScriptableObject.ChangeWaitForEndButtonToFalse();
        _wordSelection.SetEndPoint(_cellPosition);
    }

}
