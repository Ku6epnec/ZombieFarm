using System;
using UnityEngine;
using ZombieFarm.Views.Player;

[Serializable]
public class PlayerData : MonoBehaviour, IData
{
    public PlayerView playerView;
    public PlayerMover playerMover;
    public Transform playerTransform;
    public Vector3 position;
    public string objectName;

    public Transform ObjectTransform => playerTransform;
    public string ObjectName => objectName;
}
