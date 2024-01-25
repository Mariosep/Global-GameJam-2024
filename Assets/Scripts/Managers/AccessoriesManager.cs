using System.Collections.Generic;
using UnityEngine;

public class AccessoriesManager : MonoBehaviour
{
    public Transform accessoriesContainer;
    
    private AccessoriesCollectionData _accessoriesCollectionData;
    private List<GameObject> _accessoryList;
    
    private void Awake()
    {
        RoundChannel.onRoundStarted += OnRoundStarted;
        RoundChannel.onDecorPhaseCompleted += OnDecorPhaseCompleted;
        RoundChannel.onRoundCompleted += OnRoundCompleted;

        _accessoryList = new List<GameObject>();
    }
    
    private void OnDisable()
    {
        RoundChannel.onRoundStarted -= OnRoundStarted;
        RoundChannel.onRoundCompleted -= OnRoundCompleted;
    }
    
    private void OnRoundStarted(RoundController roundControllerStarted)
    {
        _accessoriesCollectionData = roundControllerStarted.roundData.accessoriesCollectionData;

        _accessoryList.ForEach(Destroy);

        foreach (GameObject accessoryPrefab in _accessoriesCollectionData.accessoriesList)
        {
            GameObject accessory = Instantiate(accessoryPrefab, accessoriesContainer);
            _accessoryList.Add(accessory);
        }
    }
    
    private void OnDecorPhaseCompleted()
    {
        
    }
    
    private void OnRoundCompleted(RoundController roundControllerCompleted)
    {
        
    }
}
