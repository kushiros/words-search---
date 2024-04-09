using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

    
public class DrawScript : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    public Gradient colorA;
    public Gradient colorF;
    bool selectedColor = true;

    public KeyCode drawButton = KeyCode.Mouse0;

    LineRenderer currentLineRenderer;

    List<GameObject> lineRenderers = new List<GameObject>();

    Vector2 lastPos;

    private void Start()
    {
        if (m_camera) m_camera = Camera.main;
    }

    private void Update()
    {
        Drawing();
    }



    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            selectedColor = !selectedColor;
            Debug.Log($"Color is now = {selectedColor}");
        }

        if (Input.GetKeyDown(drawButton) && !Input.GetKey(KeyCode.Space))
        {
            print("keyDown");

            CreateBrush();
        }
        else if (Input.GetKey(drawButton))
        {
            print("KeyDraw");

            PointToMousePos();
        }
        else
        {
            currentLineRenderer = null;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ClearLineRenderers();
        }
    }

    public void ClearLineRenderers()
    {
        foreach (var item in lineRenderers)
        {
            item.SetActive(false);
        }
        lineRenderers.Clear();
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        if (selectedColor)
        {
            currentLineRenderer.colorGradient = colorA;
        }
        else
        {
            currentLineRenderer.colorGradient = colorF;
        }

        lineRenderers.Add(brushInstance);

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        print($"POINT in {pointPos}");
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            print("DRAW");
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
        else
        {
            print($"mousepos= {mousePos} is same as lastpos= {lastPos}");
        }
    }

}