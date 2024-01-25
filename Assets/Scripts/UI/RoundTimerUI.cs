using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimerUI : MonoBehaviour
{
    public Image timerImage;
    public TextMeshProUGUI _timeText;
    
    private AudioSource _audioSource;

    private bool isRunning;
    private bool _tictacSoundIsPlaying = false;
    
    private void Awake()
    {
        timerImage = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        RoundChannel.onRoundStarted += OnRoundStarted;
        RoundChannel.onDecorPhaseStarted += OnDecorPhaseStarted;
        RoundChannel.onDecorPhaseCompleted += OnDecorPhaseCompleted;
    }


    private void OnDisable()
    {
        RoundChannel.onRoundStarted -= OnRoundStarted;
        RoundChannel.onDecorPhaseStarted -= OnDecorPhaseStarted;
        RoundChannel.onDecorPhaseCompleted -= OnDecorPhaseCompleted;
    }

    private void Update()
    {
        if (isRunning)
        {
            if(DecorPhase.Instance.TimeLeft < 10 && !_tictacSoundIsPlaying)
            {
                _audioSource.Play();
                _tictacSoundIsPlaying = true;
            }
            
            _timeText.SetText(DecorPhase.Instance.TimeLeft.ToString());
        }
    }

    private void Show()
    {
        timerImage.enabled = true;
        _timeText.gameObject.SetActive(true);
    }

    private void Hide()
    {
        timerImage.enabled = false;
        _timeText.gameObject.SetActive(false);
    }
    
    private void OnRoundStarted(RoundController roundController)
    {
        Show();
    }
    
    private void OnDecorPhaseStarted()
    {
        isRunning = true;
    }
    
    private void OnDecorPhaseCompleted()
    {
        _tictacSoundIsPlaying = false;
        _audioSource.Stop();
        isRunning = false;
        
        Hide();
    }
}