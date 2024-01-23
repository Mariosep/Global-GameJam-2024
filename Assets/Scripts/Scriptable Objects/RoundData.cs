using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RoundData", fileName = "RoundData")]
public class RoundData : ScriptableObject
{
    public List<GameObject> accessoriesList = new List<GameObject>();
    public GameObject baseImage;
    public float roundTime;
}