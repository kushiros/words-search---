using UnityEngine;

public class RotateToTheMouse : MonoBehaviour
{
    [SerializeField] private StretchToMouse _stretchToMouse;
    [SerializeField] private bool _canRotate = false;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 startPosition;


    private void Start()
    {
        _stretchToMouse = GetComponentInChildren<StretchToMouse>();
        rectTransform = GetComponent<RectTransform>();

        // Obtener la referencia al Canvas
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (!_canRotate) return;
        UpdatePosition();

        
    }

    public void StartRotation(Vector3 _Position)
    {
        _canRotate = true;
        startPosition = _Position;
        _stretchToMouse.StartStretch(_Position);
    }

    public void EndRotation(int _intToMultiplyTheStretch,bool correctWord)
    {
        _canRotate = false;
        bool isVertical = Mathf.Abs(rectTransform.localRotation.eulerAngles.z % 180) < 45;
        _stretchToMouse.EndStretch(_intToMultiplyTheStretch,correctWord,isVertical);
    }
    private void UpdatePosition()
    {
        if (canvas == null) return;

        
        Vector2 startLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), startPosition, canvas.worldCamera, out startLocalPoint);

        
        Vector2 currentLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentLocalPoint);

   
        Vector2 directionFromStart = currentLocalPoint - startLocalPoint;

        float angleFromStart = Mathf.Atan2(directionFromStart.y, directionFromStart.x) * Mathf.Rad2Deg;

        angleFromStart -= 90;

        float roundedAngle = Mathf.Round(angleFromStart / 90) * 90;

        rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, roundedAngle));
    }
}
