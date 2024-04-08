using UnityEngine;
using UnityEngine.UI;

public class StretchToMouse : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private bool _canStretch = false;
    private Image _image;
    private Canvas canvas;
    private Vector2 startPosition;
    

    [SerializeField] ColorController_ScriptableObject EventColorController;
    [SerializeField] EnlargeHeightValue EnlargeHeightValue;
    [SerializeField] float enlargeXValue;
    [SerializeField] float enlargeYValue;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        canvas = GetComponentInParent<Canvas>();
        EnlargeHeightValue.widthCellXYSizeEvent += SetXandY;
        EnlargeHeightValue.GetActivateEvent();
        
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

    public void EndStretch(int _intToMultiply,bool correctWord,bool _isVertival)
    {
        _canStretch = false;
        if (!correctWord)
        {
            _image.enabled = false;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 87f);
            return;
        }
        if (_isVertival)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (_intToMultiply + 1) * enlargeXValue);
            return;
        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (_intToMultiply + 1) * enlargeYValue);

    }

    private void StretchToCurrentMousePosition()
    {

        Vector2 currentMouseLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentMouseLocalPoint);

        float distance = Vector2.Distance(startPosition, currentMouseLocalPoint);

        if (distance < 87) return;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, distance);

    }
    private void SetXandY(float _x, float _y)
    {
        enlargeXValue = _x;
        enlargeYValue = _y;
        EnlargeHeightValue.widthCellXYSizeEvent -= SetXandY;
    }



}
