using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="holapadre/hola")]
public class onClick_scriptableObject : ScriptableObject
{
    [SerializeField] bool waitForEndbutton;

    public void ChangeWaitForEndButtonToFalse() {  waitForEndbutton = false; }
    public void ChangeWaitForEndButtonToTrue() {  waitForEndbutton = true; }

    public bool GetWaitForEndButton() {  return waitForEndbutton; }
}
