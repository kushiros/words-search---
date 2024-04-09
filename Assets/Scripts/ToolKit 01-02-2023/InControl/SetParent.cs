using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class SetParent : ToolsParent
    {
        [Space(20)]
        public Transform objective;
        public Transform newParent;
        public bool localPositionStays;
        public bool localRotationStays;
        public bool localScaleStays;

        protected override void _OnRunAction()
        {
            _SetParent();
        }

        public void _ChangeParent(Transform _newParent)
        {
            _SetParent();
        }

        public void _SetParent()
        {
            Vector3 position = transform.localPosition;
            Quaternion rotation = transform.localRotation;
            Vector3 scale = transform.localScale;
            if (objective == null) objective = this.transform;
            objective.SetParent(newParent);
            if(localPositionStays)  transform.localPosition = position;
            if(localRotationStays)  transform.localRotation = rotation;
            if(localScaleStays)  transform.localScale = scale;
        }

        public void _SetParentWorldRotationStays()
        {
            Quaternion rotation = transform.localRotation;
            transform.SetParent(newParent);
        }

    }
}