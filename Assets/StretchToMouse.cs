using UnityEngine;
using UnityEngine.UI;

//[ExecuteAlways]
public class StretchToMouse : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] private bool _canStretch = false;
    private Image _image;
    private Canvas canvas;

    void Start()
    {
        // Obtener el componente RectTransform
        rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();

        // Asigna el componente Canvas principal. Asegúrate de que este script esté adjunto a un objeto que está dentro del Canvas.
        canvas = GetComponentInParent<Canvas>();
    }

    /*void Update()
    {
        if (!_canStretch) return;

        // Convierte la posición del mouse de la pantalla a una posición local en el RectTransform
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out localPoint);

        // Calcula la altura como la distancia en el eje Y desde la posición local del objeto hasta el punto local del mouse
        float height = Mathf.Abs(localPoint.y - rectTransform.anchoredPosition.y);

        // Ajustar la altura del rectTransform
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }*/

    public void StartStretch(Vector3 _position)
    {
        _canStretch = true;
        _image.enabled = true;
    }

    public void EndStretch()
    {
        _canStretch = false;
    }
}
