using UnityEngine;

public class RotateToTheMouse : MonoBehaviour
{
    [SerializeField] private StretchToMouse _stretchToMouse;
    [SerializeField] private bool _canRotate = false;
    private RectTransform rectTransform;
    private Canvas canvas;

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

        // Obtener la posición del mouse en coordenadas locales con respecto al Canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out localPoint);

        // Obtener la posición del RectTransform en coordenadas locales
        Vector2 rectPosition = rectTransform.anchoredPosition;

        // Calcula la dirección del mouse respecto al objeto en coordenadas locales
        Vector2 directionToMouse = localPoint - rectPosition;

        // Calcula el ángulo necesario para mirar al mouse
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Establece la rotación del objeto para que mire hacia el mouse
        rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle ));
    }

    public void StartRotation()
    {
        _canRotate = true;
        _stretchToMouse.StartStretch();
    }

    public void EndRotation()
    {
        _canRotate = false;
        _stretchToMouse.EndStretch();
    }
}
