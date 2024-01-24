using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AccessoriesCollection", fileName = "AccessoriesCollection")]
public class AccessoriesCollectionData : ScriptableObject
{
    public List<GameObject> accessoriesList = new List<GameObject>();
}