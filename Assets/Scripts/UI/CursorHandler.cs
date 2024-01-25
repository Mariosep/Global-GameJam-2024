using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public Texture2D defaultCursor;
    public Texture2D grabCursor;

    public AudioClip clickSound;
    public AudioClip releaseSound;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        InteractionChannel.onImageGrabbed += OnImageGrabbed;
        InteractionChannel.onImageReleased += OnImageReleased;
    }

    private void OnDisable()
    {
        InteractionChannel.onImageGrabbed -= OnImageGrabbed;
        InteractionChannel.onImageReleased -= OnImageReleased;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _audioSource.clip = clickSound;
            _audioSource.Play();
        }    
        else if(Input.GetMouseButtonUp(0))
        {
            _audioSource.clip = releaseSound;
            _audioSource.Play();
        }
    }

    private void OnImageReleased(Movable imageReleased)
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnImageGrabbed(Movable imageGrabbed)
    {
        Cursor.SetCursor(grabCursor, Vector2.zero, CursorMode.Auto);
    }
}
