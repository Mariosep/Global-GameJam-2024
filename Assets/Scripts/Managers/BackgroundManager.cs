using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BackgroundManager : MonoBehaviour
{
    public TMP_Dropdown dropdownMenu;
    private RoundData _currentRound;
    private RectTransform _rectTransform;
    private Image _image;

    public void Awake()
    {
        RoundChannel.onRoundStarted += OnRoundStarted;
        GameObject imageGo = GameObject.FindGameObjectWithTag("View Image BG");
        _rectTransform = imageGo.GetComponent<RectTransform>();
        _image = imageGo.GetComponent<Image>();
    }

    private void Update()
    {
        if (!MoveTool.Instance.canMove)
        {
            dropdownMenu.interactable = false;
        }
        else
        {
            dropdownMenu.interactable = true;
        }
    }

    public void ActionToCall(int selectedIndex)
    {
        SetBgImage(selectedIndex);
    }
    
    private static void SetRect(RectTransform trs, float left, float top, float right, float bottom)
    {
        trs.offsetMin = new Vector2(left, bottom);
        trs.offsetMax = new Vector2(-right, -top);
    }

    private void SetBgImage(int selectedIndex)
    {
        BackgroundData bgData = _currentRound.backgrounds[selectedIndex];
        _image.sprite = bgData.bg;
        _rectTransform.transform.localScale = bgData.scale;
        SetRect(_rectTransform,bgData.left, bgData.top, bgData.right, bgData.bottom );
    }

    private void OnRoundStarted(RoundController roundController)
    {
        _currentRound = roundController.roundData;
        PopulateList();
    }

    private void PopulateList()
    {
        dropdownMenu.ClearOptions();
        if (_currentRound.backgrounds.Count > 0)
        {
            SetBgImage(0);
            foreach (var background in _currentRound.backgrounds)
            {
                dropdownMenu.options.Add(new TMP_Dropdown.OptionData(background.title, background.bg));
            }
            dropdownMenu.RefreshShownValue();
        }
    }

    private void OnDestroy()
    {
        RoundChannel.onRoundStarted -= OnRoundStarted;
    }
}
