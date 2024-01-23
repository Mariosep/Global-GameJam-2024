using System;
using System.Collections;
using System.Collections.Generic;
using AQM.Tools;
using UnityEngine;
using UnityEngine.Serialization;

public class ConversationRunner : MonoBehaviour
{
    public ConversationTree conversationTree;
    private AudioSource _source;
    public List<int> times = new List<int>();
    private int _index = 0;
    private bool _conversationEnded;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        DialogSystemController.onConversationEnded += OnConversationEnded;
    }

    private void OnDestroy()
    {
        DialogSystemController.onConversationEnded -= OnConversationEnded;
    }

    private void Start()
    {
        DDEvents.onStartConversation?.Invoke(conversationTree);
        _conversationEnded = false;
        StartCoroutine(NextMessage(times[_index]));
    }

    IEnumerator NextMessage(int time)
    {
        _source.Play();
        yield return new WaitForSeconds(time);
        if (_index + 1 < times.Count )
        {
            _index += 1;
            DialogSystemController.Instance.GetNextNode();
            if(!_conversationEnded) StartCoroutine(NextMessage(times[_index]));
        }
    }

    private void OnConversationEnded()
    {
        _conversationEnded = true;
    }
}
