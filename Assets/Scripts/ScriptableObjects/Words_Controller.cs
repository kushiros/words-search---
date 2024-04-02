using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Words_Controller : MonoBehaviour
{
    [SerializeField] private List<string> words;
    [SerializeField] private List<TextMeshProUGUI> TMProList = new List<TextMeshProUGUI>();
    [SerializeField] WordsearchGrid_ScriptableObject ScriptableObject;

    private void Start()
    {
        words = ScriptableObject.GetWordsList();
        GetTMProChildren();
        SetWordsToTMPro();
    }

    private void GetTMProChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            TextMeshProUGUI tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                TMProList.Add(tmp);
            }
        }
    }


    private void SetWordsToTMPro()
    {
        int index = 0;
        foreach (TextMeshProUGUI tmp in TMProList)
        {
            if (index < words.Count)
            {
                tmp.text = words[index];
                index++;
            }
            else
            {
                break; // Si hay más TextMeshPro que palabras, detener el bucle
            }
        }
    }
}
