using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatDiabledRatings : MonoBehaviour
{
    [SerializeField] private GameObject textGo;
    
    // Start is called before the first frame update
    private void Awake()
    {
        RoundChannel.onPreRatePhase += OnShow;
        RoundChannel.onPostRatePhase += OnHide;
    }

    private void OnShow()
    {
        textGo.SetActive(true);
    }
    
    private void OnHide()
    {
        textGo.SetActive(false);
    }

    private void OnDestroy()
    {
        RoundChannel.onPreRatePhase -= OnShow;
        RoundChannel.onPostRatePhase -= OnHide;
    }
}
