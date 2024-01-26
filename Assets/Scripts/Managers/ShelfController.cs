using UnityEngine;

public class ShelfController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Disappear()
    {
        _animator.SetTrigger("Disappear");
    }
}