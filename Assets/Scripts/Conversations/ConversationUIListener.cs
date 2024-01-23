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
        //_dialogUIContainer = dialogUIGo.GetComponent<DialogUIContainer>();
        //_dialogCanvasGroup = dialogUIGo.GetComponent<CanvasGroup>();
        DialogSystemController.onShowNewDialog += ShowDialogNode;
    }
    
    private void ShowDialogNode(DSDialog node)
    {
        GameObject newDialog = Instantiate(dialogPrefab);
        if (newDialog)
        {
            newDialog.transform.SetParent(panelContainer.transform,false);
            newDialog.GetComponentInChildren<TextMeshProUGUI>().text = node.Actor.fullName+": "+node.Message;
            newDialog.GetComponentInChildren<Image>().sprite = node.Actor.actorImage;
        }
    }

    private void OnDestroy()
    {
        DialogSystemController.onShowNewDialog -= ShowDialogNode;
    }
}
