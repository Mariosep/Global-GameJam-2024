using AQM.Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "RoundData", fileName = "RoundData")]
public class RoundData : ScriptableObject
{
    public AccessoriesCollectionData accessoriesCollectionData;
    public GameObject baseImage;
    public float roundTime;
    public ConversationTree conversation;
}