using UnityEngine;

public class AccessoriesManager : Singleton<AccessoriesManager>
{
    public Transform accessoriesContainer;

    private GameObject _shelf;
    private ShelfController _shelfController;
    
    private void Awake()
    {
        RoundChannel.onDecorPhaseCompleted += OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;

        _shelfController = GetComponent<ShelfController>();
    }
    
    private void OnDestroy()
    {
        RoundChannel.onDecorPhaseCompleted -= OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }
    
    public void ShowShelf(GameObject shelfPrefab)
    {
        if(_shelf != null)
            Destroy(_shelf);

        _shelf = Instantiate(shelfPrefab, transform).gameObject;
        _shelfController.ShowShelf();
    }
    
    private void OnDecorPhaseCompleted()
    {
        _shelfController.HideShelf();
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
}