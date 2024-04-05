using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Words_Controller : MonoBehaviour
{
    [SerializeField] private List<string> words;
    [SerializeField] private List<TextMeshProUGUI> TMProList = new List<TextMeshProUGUI>();
    [SerializeField] WordsearchGrid_ScriptableObject ScriptableObject;
    public event Action<Vector3> findWordPosition;
    [SerializeField] GameObject actualWord;
    [SerializeField] Color colorAtFinish;

    private void Start()
    {

        words = ScriptableObject.GetWordsList();
        GetTMProChildren();
        SetWordsToTMPro();
        ScriptableObject.OnWordFound += ChangeWordColor;
    }
    private void OnDestroy()
    {
        ScriptableObject.OnWordFound -= ChangeWordColor; 
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
                break;
            }
        }
    }
    private void ChangeWordColor(string word)
    {
        int index = words.IndexOf(word);
        if (index != -1 && index < TMProList.Count)
        {
            colorAtFinish = TMProList[index].color;
            colorAtFinish = new Color(colorAtFinish.r, colorAtFinish.g, colorAtFinish.b, 25f / 255f);

            actualWord = TMProList[index].gameObject;
            
            Vector3 centerPosition = TMProList[index].rectTransform.TransformPoint(TMProList[index].rectTransform.rect.center);
            findWordPosition?.Invoke(centerPosition);

        }
    }

    public GameObject GetActualWord()
    {
        return actualWord;
    }
    public Color GetColorAtFinish()
    {
        return colorAtFinish;
    }
}
