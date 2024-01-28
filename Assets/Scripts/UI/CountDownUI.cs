using System.Collections;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    public TextMeshProUGUI countDownText;
    private AudioSource _audioSource;

    public AudioClip bipSound;
    public AudioClip bipSound2;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        RoundChannel.onBaseImageShowed += StartCountDown;
    }

    private void OnDestroy()
    {
        RoundChannel.onBaseImageShowed -= StartCountDown;
    }

    public void StartCountDown()
    {
        StartCoroutine(CO_CountDown());
    }

    private IEnumerator CO_CountDown()
    {
        countDownText.enabled = true;
        
        _audioSource.clip = bipSound;
        _audioSource.Play();
        countDownText.text = 3.ToString();
        
        yield return new WaitForSeconds(1);

        _audioSource.Play();
        countDownText.text = 2.ToString();
        
        yield return new WaitForSeconds(1);
        
        _audioSource.Play();
        countDownText.text = 1.ToString();
        
        yield return new WaitForSeconds(1);
        
        _audioSource.clip = bipSound2;
        _audioSource.Play();
        countDownText.enabled = false;
    }
}