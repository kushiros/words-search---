using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace InControl
{

    public class BoundedCurveAttribute : PropertyAttribute
    {
        public Rect bounds;
        public int height;

        //public BoundedCurveAttribute(bool no, int height = 1)
        //{
        //    //this.bounds = _bounds;
        //    this.height = height;
        //}

        public BoundedCurveAttribute(int heightUI = 1, float startX = 0, float startY = 0,  float length = 1, float height = 1)
        {
            this.bounds = new Rect(startX, startY, length, height);
            this.height = heightUI;
        }
    }


    [CustomPropertyDrawer(typeof(BoundedCurveAttribute))]
    public class BoundedCurveDrawer : PropertyDrawer
    {


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            BoundedCurveAttribute boundedCurve = (BoundedCurveAttribute)attribute;
            return EditorGUIUtility.singleLineHeight * boundedCurve.height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            BoundedCurveAttribute boundedCurve = (BoundedCurveAttribute)attribute;

            EditorGUI.BeginProperty(position, label, property);
            property.animationCurveValue = EditorGUI.CurveField(
              position,
              label,
              property.animationCurveValue,
              Color.white,
              boundedCurve.bounds
             );
            EditorGUI.EndProperty();
        }
    }
}