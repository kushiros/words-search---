using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class ActivateToolInChidren : ToolsParent
    {
        public Transform ParentObjective;

        protected override void _OnRunAction()
        {
            for (int i = 0; i < ParentObjective.childCount; i++)
            {
                ParentObjective.GetChild(i).GetComponentInChildren<ToolsParent>()._RunAction();
            }
            
        }
    }
}