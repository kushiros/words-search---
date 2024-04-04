using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways]
public class StretchToMouse : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private bool _canStretch = false;
    private Image _image;
    private Canvas canvas;
    private Vector2 startPosition;

    void Start()
    {
        // Obtener el componente RectTransform
        rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        // Asigna el componente Canvas principal. Asegúrate de que este script esté adjunto a un objeto que está dentro del Canvas.
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        if (!_canStretch) return;

        StretchToCurrentMousePosition();
    }

    public void StartStretch(Vector3 _position)
    {
        // Convierte la posición inicial del mouse de la pantalla a una posición local en el RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), _position, canvas.worldCamera, out startPosition);
        _canStretch = true;
        _image.enabled = true;
    }

    public void EndStretch()
    {
        _canStretch = false;
    }

    private void StretchToCurrentMousePosition()
    {
        // Convierte la posición actual del mouse de la pantalla a una posición local en el RectTransform
        Vector2 currentMouseLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentMouseLocalPoint);

        // Calcula la distancia entre la posición inicial y la posición actual del mouse en el espacio local del Canvas
        float distance = Vector2.Distance(startPosition, currentMouseLocalPoint);

        // Ajustar el sizeDelta del RectTransform basado en
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, distance);
    }
}