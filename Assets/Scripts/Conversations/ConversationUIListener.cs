using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AQM.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUIListener : MonoBehaviour
{
    [SerializeField] private GameObject panelContainer;
    [SerializeField] private GameObject responsePanel;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject responsePrefab;
    
    private DSChoice _currentChoiceNode;
    private Coroutine _choiceCo;
    private bool _stopTimer;
    private RectTransform _panelRectTransform;
    
    private void Awake()
    {
        DialogSystemController.onShowNewDialog += ShowDialogNode;
        DialogSystemController.onShowNewChoiceInTime += ShowChoiceNode;
        _panelRectTransform = panelContainer.GetComponent<RectTransform>();
    }
    
    private void ShowDialogNode(DSDialog node)
    {
        DestroyAllChildren(responsePanel.transform);
        GameObject newDialog = Instantiate(dialogPrefab,panelContainer.transform);
        if (newDialog)
        {
            string hexColor = ColorUtility.ToHtmlStringRGBA(node.Actor.bgColor);
            newDialog.GetComponentInChildren<TextMeshProUGUI>().text = "<color=#"+hexColor+">"+node.Actor.fullName+":</color> "+node.Message;
            LayoutRebuilder.MarkLayoutForRebuild(_panelRectTransform);
        }
    }
    
    private void ShowChoiceNode(DSChoice node, float seconds)
    {
        DestroyAllChildren(responsePanel.transform);
        _currentChoiceNode = node;
        GameObject newChoice = Instantiate(dialogPrefab,panelContainer.transform);
        if (newChoice)
        {
            string hexColor = ColorUtility.ToHtmlStringRGBA(node.Actor.bgColor);
            newChoice.GetComponentInChildren<TextMeshProUGUI>().text = "<color=#"+hexColor+">"+node.Actor.fullName+":</color> "+node.Message;
            LayoutRebuilder.MarkLayoutForRebuild(_panelRectTransform);
            
            InstantiateChoices();

            if (seconds > 0)
            {
                _stopTimer = false;
                if(_choiceCo != null)  StopCoroutine(_choiceCo);
                _choiceCo = StartCoroutine(NoResponseCoroutine(seconds));
            }
        }
    }
    
    private void InstantiateChoices()
    {
        for (int i = 0; i < _currentChoiceNode.Choices.Count; i++)
        {
            string choice = _currentChoiceNode.Choices[i];
                
            //Cast it as a Button, not a game object
            GameObject newButtonGo = Instantiate(responsePrefab);
            if (newButtonGo)
            {
                newButtonGo.transform.SetParent(responsePanel.transform,false);
                Button button = newButtonGo.GetComponent<Button>();
                newButtonGo.GetComponentInChildren<TextMeshProUGUI>().text = choice;
                var saveIndex = i;
                var choiceNode = _currentChoiceNode;
                button.onClick.AddListener(delegate () {  OnChoiceSelected(choiceNode, saveIndex);});
            }
        }
    }
    
    private void DestroyAllChildren(Transform t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => Destroy(c.gameObject));
    }
    
    private IEnumerator NoResponseCoroutine(float choiceTime)
    {
        while (_stopTimer == false)
        {
            choiceTime -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            if (choiceTime <= 0) _stopTimer = true;
        }
        OnChoiceSelected(_currentChoiceNode, -1);
    }
    
    private void OnChoiceSelected(DSChoice choiceNode, int index)
    {
        if(_choiceCo != null) StopCoroutine(_choiceCo);
        choiceNode.onChoiceSelected.Invoke(index);
    }

    private void OnDestroy()
    {
        DialogSystemController.onShowNewDialog -= ShowDialogNode;
        DialogSystemController.onShowNewChoiceInTime -= ShowChoiceNode;
        if(_choiceCo != null)  StopCoroutine(_choiceCo);
    }
}
