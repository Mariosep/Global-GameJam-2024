using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RoundData", fileName = "RoundData")]
public class RoundData : ScriptableObject
{
    public GameObject shelfPrefab;
    public GameObject baseImagePrefab;
    public List<GameObject> npcResults;
    public float roundTime;
    public DialogData dialogData;
    public List<BackgroundData> backgrounds;
}