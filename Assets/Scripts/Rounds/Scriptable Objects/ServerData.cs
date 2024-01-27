using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ServerData", fileName = "ServerData")]
public class ServerData : ScriptableObject
{
    public bool isPrivate;
    public string title;
    public string ip;
    public string region;
    public string playersOnline;
    public int code;
}
