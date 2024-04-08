using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[CreateAssetMenu]
public class EnlargeHeightValue : ScriptableObject
{
    [SerializeField] float _floatX;
    [SerializeField] float _floatY;
    // Start is called before the first frame update
    [SerializeField] PanelSize_ScriptableObject _panel;

    public Action<float,float> widthCellXYSizeEvent;


    private void OnEnable()
    {
        _panel.widthCellXYSizeEvent += SetFloatXY;


    }
    private void OnDisable()
    {
        _panel.widthCellXYSizeEvent -= SetFloatXY;

    }

    private void SetFloatXY(float _valueX,float _valueY)
    {
        _floatX = (((int)_valueX*95/100));
        _floatY = (((int)_valueY*95/100));
        
    
    }


    public void GetActivateEvent()
    {
        widthCellXYSizeEvent.Invoke(_floatX, _floatY);
    }

}
