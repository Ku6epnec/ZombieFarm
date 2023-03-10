using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieFarm.Views.Player
{ 
    public class PlayerView : MonoBehaviour
    {
        public event Action<PlayerState> OnChangeState = (newState) => { };

        private PlayerState currentPlayerState;

        private void Awake()
        {
            currentPlayerState = PlayerState.Idle;
        }


    }
}
