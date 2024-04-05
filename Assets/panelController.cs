using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class panelController : MonoBehaviour
{
    private float originalWidthSize;
    private float originalHeightSize;
    private RectTransform rectTransform;
    private TextMeshProUGUI chars;
    private Image image;
    [SerializeField]private Actual2CellsContainer_ScriptableObject eventSender;
    [SerializeField] ColorController_ScriptableObject EventColorController;
    void Start()
    {
        chars = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        originalHeightSize = rectTransform.sizeDelta.y;
        originalWidthSize = rectTransform.sizeDelta.x;
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
        eventSender.OnCharEvent += HandleCharEvent;
        eventSender.OnResetEvent += HandleResetEvent;
        EventColorController.OnChangeMaterialEvent += ChangeColorMaterial;
    }

    private void OnDisable()
    {
        eventSender.OnCharEvent -= HandleCharEvent;
        eventSender.OnResetEvent -= HandleResetEvent;
        EventColorController.OnChangeMaterialEvent -= ChangeColorMaterial;
    }

    private void HandleCharEvent(char character)
    {
        image.enabled = true; 
        ReSizeRectTransform((rectTransform.sizeDelta.x + originalWidthSize/2));
        AddChar(character);

    }
    private void HandleResetEvent()
    {
        ReSizeRectTransform(originalWidthSize);
        image.enabled = false;
        ResetChars();
    }
    private void ReSizeRectTransform(float _width)
    {
        rectTransform.sizeDelta = new Vector2(_width, rectTransform.sizeDelta.y);
    }
    private void AddChar(Char _char)
    {
        chars.text += _char;
    }
    private void ResetChars()
    {
        chars.text = "";
    }
    private void ChangeColorMaterial(Material _material)
    {
        
        image.material = _material;
    }

}
