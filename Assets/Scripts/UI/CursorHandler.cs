using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public Texture2D defaultCursor;
    public Texture2D grabCursor;

    public AudioClip clickSound;
    public AudioClip releaseSound;

    private Vector2 _hotspot;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _hotspot = new Vector2(defaultCursor.height / 2f, 0);
        Cursor.SetCursor(defaultCursor,_hotspot, CursorMode.Auto);
        
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
        Cursor.SetCursor(defaultCursor,_hotspot, CursorMode.Auto);
    }

    private void OnImageGrabbed(Movable imageGrabbed)
    {
        Cursor.SetCursor(grabCursor, _hotspot, CursorMode.Auto);
    }
}
