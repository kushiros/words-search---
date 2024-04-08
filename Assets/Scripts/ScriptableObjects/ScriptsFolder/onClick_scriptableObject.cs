using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName ="holapadre/hola")]
public class onClick_scriptableObject : ScriptableObject
{
    [SerializeField] bool waitForEndbutton =false;
    private CellPosition actualCellPosition;
    private int ActualX, ActualY;
    private List<CellPosition> _listCellPosition;
    public void ChangeWaitForEndButtonToFalse() { waitForEndbutton = false; }
    [SerializeField] Words_Controller Words_Controller;
    [SerializeField] List<GameObject> clonedList;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject _gameObjectOfParticleSystem;
    [SerializeField] GameObject _star;

    public void Initialize(Words_Controller wordsController)
    {
        this.Words_Controller = wordsController;
        Words_Controller.findWordPosition += MoveTo;
        instantiateParticleSystem();
    }
    private void OnDisable()
    {
        if (Words_Controller != null)
        {
            Words_Controller.findWordPosition -= MoveTo;
        }
    }
    public void ChangeWaitForEndButtonToTrue(CellPosition _cellPosition)
    {
        waitForEndbutton = true;
        actualCellPosition = _cellPosition;
        ActualX = _cellPosition.GetPositionX();
        ActualY = _cellPosition.GetPositionY();
    }

    public bool GetWaitForEndButton() {  return waitForEndbutton; }

    public int GetActualX()
    {
        return ActualX;
    }
    public int GetActualY()
    {
        return ActualY;
    }
    public void AddToListCellPosition(CellPosition cellPosition)
    {
        _listCellPosition.Add(cellPosition);
    }
    public void DeleteAllListCellPosition()
    {
        Debug.Log("llegue al deletealllistcellcontroller");
        foreach(CellPosition cellPosition in _listCellPosition)
        {
            Debug.Log("ayuda");
            cellPosition.GetcharController().ReturnToOriginalColor();
        }
        ResetList();
    }
    public void ResetList()
    {
        _listCellPosition.Clear();
    }

    public void DuplicateListItems()
    {

        RestartClonedList();
        foreach (CellPosition cellPosition in _listCellPosition)
        {
            GameObject original = cellPosition.GetCharComponent();
            GameObject clone = Instantiate(original, original.transform.position, original.transform.rotation);
            clone.GetComponent<CharController>().SetChar(cellPosition.GetcharController().GetChar());
            clone.GetComponent<TextMeshProUGUI>().color = Color.black;
            clonedList.Add(clone);

        }

    }
    private void MoveTo(Vector3 worldPosition)
    {
        DuplicateListItems();
        _star.transform.position = worldPosition;
        
        int completedAnimations = 0; // Variable para llevar la cuenta de las animaciones completadas

        foreach (GameObject gameObject in clonedList)
        {
            LeanTween.move(gameObject, worldPosition, 0.5f).setOnComplete(() =>
            {
                completedAnimations++; // Incrementa el contador de animaciones completadas
                Destroy(gameObject);

                // Si todas las animaciones han terminado, ejecuta BounceAnimation
                if (completedAnimations == clonedList.Count)
                {
                    
                    BounceAnimation();
                    
                }
            });
        }


    }
    private void instantiateParticleSystem()
    {
        _star = GameObject.Instantiate(_gameObjectOfParticleSystem);
        _particleSystem = _star.GetComponent<ParticleSystem>();

        
    }
    private void RestartClonedList()
    {
        clonedList.Clear();
    }

    private void BounceAnimation()
    {
        GameObject _gameObject = Words_Controller.GetActualWord();
        LeanTween.scale(_gameObject, Vector3.one * 1.2f, 0.12f) // Aumenta el tamaño para el rebote
        .setEase(LeanTweenType.easeOutBack) // Efecto de rebote
        .setOnComplete(() =>
        {
            LeanTween.scale(_gameObject, Vector3.one, 0.12f).setOnComplete(() => {

                _particleSystem.Play();
            });
            _gameObject.GetComponent<TextMeshProUGUI>().color = Words_Controller.GetColorAtFinish();
        });
    }


}
