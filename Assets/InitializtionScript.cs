using UnityEngine;

public class InitializtionScript : MonoBehaviour
{
    public Words_Controller wordsController;
    public onClick_scriptableObject onClickScriptableObject;

    void Start()
    {
        if (wordsController != null && onClickScriptableObject != null)
        {
            onClickScriptableObject.Initialize(wordsController);
        }
    }
}