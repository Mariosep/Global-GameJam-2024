using System;
using System.Collections;
using System.Collections.Generic;
using AQM.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUIListener : MonoBehaviour
{
    [SerializeField] private GameObject panelContainer;
    [SerializeField] private GameObject dialogPrefab;
    
    private void Awake()
    {
        DialogSystemController.onShowNewDialog += ShowDialogNode;
    }
    
    private void ShowDialogNode(DSDialog node)
    {
        GameObject newDialog = Instantiate(dialogPrefab);
        if (newDialog)
        {
            newDialog.transform.SetParent(panelContainer.transform,false);
            string hexColor = ColorUtility.ToHtmlStringRGBA(node.Actor.bgColor);
            newDialog.GetComponentInChildren<TextMeshProUGUI>().text = "<color=#"+hexColor+">"+node.Actor.fullName+":</color> "+node.Message;
        }
    }

    private void OnDestroy()
    {
        DialogSystemController.onShowNewDialog -= ShowDialogNode;
    }
}
