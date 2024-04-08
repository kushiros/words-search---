using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatValue : ScriptableObject
{
    [SerializeField] protected float _float;

    public float GetFloat() {  return _float; }
    public virtual void SetFloat(float _value) {  _float = _value; }
    
}
