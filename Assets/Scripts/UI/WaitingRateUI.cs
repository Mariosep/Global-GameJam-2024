using UnityEngine;

public class WaitingRateUI : Singleton<WaitingRateUI>
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void ShowWaitingRatePanel()
    {
        _animator.SetBool("ShowPanel", true);
    }

    public void HideRatePanel()
    {
        _animator.SetBool("ShowPanel", false);
    }
}