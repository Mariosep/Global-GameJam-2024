using UnityEngine;

public class WaitingToStartUI : Singleton<WaitingToStartUI>
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void ShowWaitingPanel()
    {
        _animator.SetBool("Show", true);
    }

    public void HideWaitingPanel()
    {
        _animator.SetBool("Show", false);
    }
}
