using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTextController : MonoBehaviour
{
    private TextMeshProUGUI roundText;

    private void Awake()
    {
        roundText = GetComponent<TextMeshProUGUI>();
        RoundChannel.onRoundStarted += OnStartRound;
    }

    private void OnStartRound(RoundController controller)
    {
        roundText.text = "Round " + controller.roundNumber;
    }

    private void OnDestroy()
    {
        RoundChannel.onRoundStarted -= OnStartRound;
    }
}
