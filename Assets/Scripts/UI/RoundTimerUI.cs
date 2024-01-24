using TMPro;
using UnityEngine;

public class RoundTimerUI : MonoBehaviour
{
    private TextMeshProUGUI _timeText;
    private AudioSource _audioSource;
    private Round _currentRound;

    private bool _tictacSoundIsPlaying = false;
    
    private void Awake()
    {
        _timeText = GetComponent<TextMeshProUGUI>();
        _audioSource = GetComponent<AudioSource>();

        RoundChannel.onRoundStarted += OnRoundStarted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;
    }

    private void OnDisable()
    {
        RoundChannel.onRoundStarted -= OnRoundStarted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }

    private void Update()
    {
        if (_currentRound != null)
        {
            if(_currentRound.TimeLeft < 10 && !_tictacSoundIsPlaying)
            {
                _audioSource.Play();
                _tictacSoundIsPlaying = true;
            }
            
            _timeText.SetText(_currentRound.TimeLeft.ToString());
        }
    }

    private void OnRoundStarted(Round roundStarted)
    {
        _currentRound = roundStarted;
    }
    
    private void OnRoundCompleted(Round roundCompleted)
    {
        _tictacSoundIsPlaying = false;
        _audioSource.Stop();
    }
}
