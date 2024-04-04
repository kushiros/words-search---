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
    [SerializeField] Material[] _color = new Material[4];

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.material = GetRandomColor(GetRandomInt());
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

        Vector2 currentMouseLocalPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, canvas.worldCamera, out currentMouseLocalPoint);

        float distance = Vector2.Distance(startPosition, currentMouseLocalPoint);

        if (distance < 87) return;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, distance);

    }
    private int GetRandomInt()
    {
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 4);
        return randomNumber;
    }
    private Material GetRandomColor(int i)
    {
        return _color[i];
    }


}
