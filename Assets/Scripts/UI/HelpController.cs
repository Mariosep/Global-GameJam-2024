using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public RectTransform helpWindow;
    public List<GameObject> steps;

    private Vector2 _defaultOffsetMin;
    private Vector2 _defaultOffsetMax;
    private void Awake()
    {
        _defaultOffsetMin = helpWindow.offsetMin;
        _defaultOffsetMax = helpWindow.offsetMax;
    }

    public void OnCloseButton()
    {
        foreach (var step in steps)
        {
            step.SetActive(false);
        }
        steps[0].SetActive(true);
        helpWindow.offsetMin = _defaultOffsetMin;
        helpWindow.offsetMax = _defaultOffsetMax;
    }
}
