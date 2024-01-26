using UnityEngine;

public class AccessoriesManager : Singleton<AccessoriesManager>
{
    public Transform accessoriesContainer;
    
    private ShelfController _shelfController;
    
    private void Awake()
    {
        RoundChannel.onDecorPhaseCompleted += OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;
    }
    
    private void OnDestroy()
    {
        RoundChannel.onDecorPhaseCompleted -= OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }
    
    public void ShowShelf(GameObject shelfPrefab)
    {
        if(_shelfController != null)
            Destroy(_shelfController.gameObject);

        _shelfController = Instantiate(shelfPrefab, transform).GetComponent<ShelfController>();
    }
    
    private void OnDecorPhaseCompleted()
    {
        _shelfController.Disappear();
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
}