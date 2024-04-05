using UnityEngine;
using UnityEngine.UI;

public class StretchToMouse : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private bool _canStretch = false;
    private Image _image;
    private Canvas canvas;
    private Vector2 startPosition;
    private BoxCollider2D boxCollider; // Agregar referencia al BoxCollider2D

    [SerializeField] ColorController_ScriptableObject EventColorController;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        boxCollider = GetComponent<BoxCollider2D>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (!_canStretch) return;

        StretchToCurrentMousePosition();
    }

    public void StartStretch(Vector3 _position)
    {
        _image.material = EventColorController.changeColor();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), _position, canvas.worldCamera, out startPosition);
        _canStretch = true;
        _image.enabled = true;
    }

    public void EndStretch(int _intToMultiply,bool correctWord)
    {
        _canStretch = false;
        if (!correctWord)
        {
            _image.enabled = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 87f);
            return;
        }
        
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,(_intToMultiply+1) * 95f);
    }

    private void StretchToCurrentMousePosition()
    {

        Vector2 currentMouseLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentMouseLocalPoint);

        float distance = Vector2.Distance(startPosition, currentMouseLocalPoint);

        if (distance < 87) return;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, distance);

    }



}
