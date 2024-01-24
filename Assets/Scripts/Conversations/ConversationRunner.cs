using System;
using System.Collections;
using System.Collections.Generic;
using AQM.Tools;
using UnityEngine;

public class ConversationRunner : MonoBehaviour
{
    public ConversationTree conversationTree;
    private AudioSource _source;

    private void Awake()
    {
        DialogSystemController.onNextMessageShown += OnNextMessage;
        _source = GetComponent<AudioSource>();
    }

    void Start()
    {
        DDEvents.onStartConversation.Invoke(conversationTree);
    }

    private void OnNextMessage(DSNode obj)
    {
        _source.Play();
    }

    private void OnDestroy()
    {
        DialogSystemController.onNextMessageShown -= OnNextMessage;
    }
}
