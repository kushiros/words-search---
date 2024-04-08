using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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

        }
        else
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (_intToMultiply + 1) * enlargeYValue);
        }
        AnimateImage();
        

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
    private void AnimateImage()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 originalPivot = rectTransform.pivot; // Almacena el valor original del pivot

        // Calcula la posición del objeto antes de cambiar el pivot
        Vector2 originalPosition = rectTransform.anchoredPosition;

        // Cambia el pivot a (0.5, 0.5)
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Calcula el desplazamiento causado por el cambio en el pivot
        Vector2 pivotOffset = new Vector2(
            (0.5f - originalPivot.x) * rectTransform.rect.width,
            (0.5f - originalPivot.y) * rectTransform.rect.height
        );

        // Ajusta la posición para mantener la misma posición visual
        rectTransform.anchoredPosition = originalPosition + pivotOffset;

        Vector3 originalScale = gameObject.transform.localScale;

        // Inicia la animación de escala
        LeanTween.scale(gameObject, originalScale * 1.1f, 0.3f).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, originalScale, 0.2f).setOnComplete(() =>
            {
                // Restaura el valor original del pivot y ajusta la posición nuevamente
                rectTransform.pivot = originalPivot;
                rectTransform.anchoredPosition = originalPosition;
            });
        });
    }





}
