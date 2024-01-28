using UnityEngine;

public class AccessoriesManager : Singleton<AccessoriesManager>
{
    public GameObject shelf;
    private ShelfController _shelfController;
    
    private void Awake()
    {
        //RoundChannel.onDecorPhaseCompleted += OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;

        _shelfController = GetComponent<ShelfController>();
    }
    
    private void OnDestroy()
    {
        //RoundChannel.onDecorPhaseCompleted -= OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }
    
    public void ShowShelf(GameObject shelfPrefab)
    {
        if(shelf != null)
            Destroy(shelf);

        shelf = Instantiate(shelfPrefab, transform).gameObject;
        _shelfController.ShowShelf();
    }
    
    public void HideShelf()
    {
        _shelfController.HideShelf();
    }
    
    private void OnDecorPhaseCompleted()
    {
        _shelfController.HideShelf();
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
}