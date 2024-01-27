using UnityEngine;

public class ShelfController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShowShelf()
    {
        _animator.SetBool("ShowShelf", true);
    }
    
    public void HideShelf()
    {
        _animator.SetBool("ShowShelf", false);
    }
}