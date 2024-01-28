using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCanvasController : MonoBehaviour
{
    [SerializeField] public GameObject endCanvas;
    [SerializeField] public Animator animator;
    private AudioSource _audioSource;
    private static readonly int Show = Animator.StringToHash("Show");


    private void Awake()
    {
        RoundChannel.onEndGame += OnEndGame;
        endCanvas.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEndGame()
    {
        endCanvas.SetActive(true);
        _audioSource.Play();
        animator.SetBool(Show, true);
    }

    private void OnDestroy()
    {
        RoundChannel.onEndGame -= OnEndGame;
    }
}
