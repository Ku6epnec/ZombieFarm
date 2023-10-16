using System;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour, IData
{
    public ZombieFarm.Views.Player.PlayerView playerView;
    public ZombieFarm.Views.Player.PlayerMover playerMover;
    public Transform playerTransform;
    public Vector3 posit;
    public string objectName;

    public Transform ObjectTransform => playerTransform;
    public string ObjectName => objectName;
}
