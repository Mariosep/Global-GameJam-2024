using UnityEngine;

public class RatingObtainedUI : Singleton<RatingObtainedUI>
{
    public GameObject pukeRating;
    public GameObject fineRating;
    public GameObject funnyRating;

    public AudioClip pukeRatingSound;
    public AudioClip fineRatingSound;
    public AudioClip funnyRatingSound;
    
    private GameObject content;
    private Animator _animator;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        
        content = transform.GetChild(0).gameObject;
        HideRatingObtainedPanel();
    }

    public void ShowRatingObtainedPanel()
    {
        //content.SetActive(true);
        HideResult();
        _animator.SetBool("ShowPanel", true);
    }
    
    public void HideRatingObtainedPanel()
    {
        _animator.SetBool("ShowPanel", false);
        /*content.SetActive(false);
        HideResult();*/
    }
    
    public void ShowResult(RatingType ratingObtained)
    {
        switch (ratingObtained)
        {
            case RatingType.Puke:
                pukeRating.SetActive(true);
                fineRating.SetActive(false);
                funnyRating.SetActive(false);
                _audioSource.clip = pukeRatingSound;
                break;
            
            case RatingType.Fine:
                pukeRating.SetActive(false);
                fineRating.SetActive(true);
                funnyRating.SetActive(false);
                _audioSource.clip = fineRatingSound;
                break;
            
            case RatingType.Funny:
                pukeRating.SetActive(false);
                fineRating.SetActive(false);
                funnyRating.SetActive(true);
                _audioSource.clip = funnyRatingSound;
                break;
        }
        
        _audioSource.Play();
    }

    public void HideResult()
    {
        pukeRating.SetActive(false);
        fineRating.SetActive(false);
        funnyRating.SetActive(false);
    }
}
