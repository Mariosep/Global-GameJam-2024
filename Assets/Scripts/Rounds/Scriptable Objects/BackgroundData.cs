using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BackgroundData", fileName = "BackgroundData")]
public class BackgroundData : ScriptableObject
{
    public string title;
    public Sprite bg;
    public float left;
    public float top;
    public float right;
    public float bottom;
    public Vector3 scale;
}
