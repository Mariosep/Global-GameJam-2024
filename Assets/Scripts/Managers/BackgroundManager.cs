using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class BackgroundManager : MonoBehaviour
{
    public TMP_Dropdown dropdownMenu;
    public RoundData round;
    private RectTransform _rectTransform;
    private Image _image;

    public void Awake()
    {
        GameObject imageGo = GameObject.FindGameObjectWithTag("View Image BG");
        _rectTransform = imageGo.GetComponent<RectTransform>();
        _image = imageGo.GetComponent<Image>();

        PopulateList();
    }

    public void ActionToCall(int selectedIndex)
    {
        SetBgImage(selectedIndex);
    }

    private void SetBgImage(int selectedIndex)
    {
        BackgroundData bgData = round.backgrounds[selectedIndex];
        _image.sprite = bgData.bg;
        _rectTransform.transform.localScale = bgData.scale;
        SetRect(_rectTransform,bgData.left, bgData.top, bgData.right, bgData.bottom );
    }
    
    public static void SetRect(RectTransform trs, float left, float top, float right, float bottom)
    {
        trs.offsetMin = new Vector2(left, bottom);
        trs.offsetMax = new Vector2(-right, -top);
    }

    private void PopulateList()
    {
        dropdownMenu.ClearOptions();
        if (round.backgrounds.Count > 0)
        {
            SetBgImage(0);
            foreach (var background in round.backgrounds)
            {
                dropdownMenu.options.Add(new TMP_Dropdown.OptionData(background.title, background.bg));
            }
            dropdownMenu.RefreshShownValue();
        }
    }
}
