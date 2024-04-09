using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SRC
{
    [CreateAssetMenu(fileName = "GlobalEvent", menuName = "ScriptableObjects/GlobalEvent/Color", order = 1)]
    public class EventScriptableColor : EventScriptableGeneric<Color>
    {
    }
}