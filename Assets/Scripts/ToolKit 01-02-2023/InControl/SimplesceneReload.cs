using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InControl
{
    public class SimplesceneReload : ToolsParent
    {
        public KeyCode restartSceneButton = KeyCode.R; 
        
        void Update()
        {
            if (Input.GetKeyDown(restartSceneButton))
            {
                _OnRunAction();
            }
        }

        protected override void _OnRunAction()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            Debug.Log($"Scene {scene.name} reloaded with {typeof(SimplesceneReload).Name}");
        }
    }
}