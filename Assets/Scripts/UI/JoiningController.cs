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
    public GameObject joiningPanel;
    public GameObject codeVerificationPanel;
    public TMP_InputField codeInputFile;

    private Coroutine _joiningCo;

    private RectTransform _joiningRecTransForm;
    private Vector2 _offsetDefaultMin;
    private Vector2 _offsetDefaultMax;
    private ServerData _currentServerData;

    private void Awake()
    {
        _joiningRecTransForm = joiningModal.GetComponent<RectTransform>();
        _offsetDefaultMin = _joiningRecTransForm.offsetMin;
        _offsetDefaultMax = _joiningRecTransForm.offsetMax;
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
        
        CloseModal();
    }

    public void OnVerifyPassword()
    {
        int code = int.Parse(codeInputFile.text);
        if (code == _currentServerData.code)
        {
            codeVerificationPanel.SetActive(false);
            _joiningCo = StartCoroutine(OpenServer());
        }
        else
        {
            Debug.Log("INCORRECT");
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
        _joiningRecTransForm.offsetMin = _offsetDefaultMin;
        _joiningRecTransForm.offsetMax = _offsetDefaultMax;
    }

    private void OnDestroy()
    {
        if(_joiningCo != null) StopCoroutine(_joiningCo);
    }
}
