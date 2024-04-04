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

    public void EndRotation()
    {
        _canRotate = false;
        _stretchToMouse.EndStretch();
    }
    private void UpdatePosition()
    {
        if (canvas == null) return;

        // Convertir la posici�n inicial del rat�n a coordenadas locales del Canvas
        Vector2 startLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), startPosition, canvas.worldCamera, out startLocalPoint);

        // Convertir la posici�n actual del rat�n a coordenadas locales del Canvas
        Vector2 currentLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentLocalPoint);

        // Calcular la direcci�n desde el punto de inicio hasta el punto actual del rat�n
        Vector2 directionFromStart = currentLocalPoint - startLocalPoint;

        // Calcular el �ngulo en relaci�n con la posici�n inicial
        float angleFromStart = Mathf.Atan2(directionFromStart.y, directionFromStart.x) * Mathf.Rad2Deg;

        // Restar 90 para que 0 grados sea arriba, -90 derecha, etc.
        float adjustedAngle = angleFromStart - 90;

        // Establecer la rotaci�n local del objeto para que mire hacia la direcci�n calculada
        rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, adjustedAngle));
    }
}
