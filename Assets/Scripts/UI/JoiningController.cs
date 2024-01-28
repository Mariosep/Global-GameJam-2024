using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class JoiningController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public GameObject joiningModal;
    public RectTransform topBarRect;
    public GameObject joiningPanel;
    public GameObject codeVerificationPanel;
    public TMP_InputField codeInputFile;
    public AudioClip errorAudio;

    private Coroutine _joiningCo;

    private RectTransform _joiningRecTransForm;
    private Vector2 _offsetDefaultMin;
    private Vector2 _offsetDefaultMax;
    private ServerData _currentServerData;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _offsetDefaultMin = topBarRect.offsetMin;
        _offsetDefaultMax = topBarRect.offsetMax;
    }

    public void OnOpenModal(ServerData server)
    {
        ResetData();
        _currentServerData = server;
        titleText.text = server.title+" / "+server.ip;
        
        if (server.isPrivate)
        {
            codeVerificationPanel.SetActive(true);
        }
        else
        {
            _joiningCo = StartCoroutine(OpenServer());
        }
        joiningModal.SetActive(true);
    }

    private IEnumerator OpenServer()
    {
        joiningPanel.SetActive(true);
        Random rnd = new Random();
        yield return new WaitForSeconds(rnd.Next(5,10));
        
        //TODO adding invoke event to start the server
        ServerChannel.onServerJoined?.Invoke();
        
        CloseModal();
    }

    public void OnVerifyPassword()
    {
        if(codeInputFile.text == "") return;
        int code = int.Parse(codeInputFile.text);
        if (code == _currentServerData.code)
        {
            codeVerificationPanel.SetActive(false);
            _joiningCo = StartCoroutine(OpenServer());
        }
        else
        {
            _audioSource.PlayOneShot(errorAudio);
        }
        codeInputFile.text = "";
    }

    public void CloseModal()
    {
        joiningModal.SetActive(false);
        ResetData();
    }

    private void ResetData()
    {
        if(_joiningCo != null) StopCoroutine(_joiningCo);
        joiningPanel.SetActive(false);
        codeVerificationPanel.SetActive(false);
        codeInputFile.text = "";
        topBarRect.offsetMin = _offsetDefaultMin;
        topBarRect.offsetMax = _offsetDefaultMax;
    }

    private void OnDestroy()
    {
        if(_joiningCo != null) StopCoroutine(_joiningCo);
    }
}
