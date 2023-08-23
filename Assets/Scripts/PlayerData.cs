using System;
using UnityEngine;

[Serializable]
public class PlayerData : IPlayerObject
{
    public ZombieFarm.Views.Player.PlayerView playerView;
    public ZombieFarm.Views.Player.PlayerMover playerMover;
    public Transform playerTransform;
    public Vector3 posit;
}
