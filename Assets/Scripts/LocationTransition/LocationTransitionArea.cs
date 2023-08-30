using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieFarm.Managers;

namespace ZombieFarm.LocationTransition
{
    public class LocationTransitionArea : MonoBehaviour
    {
        [SerializeField] private int sceneIndexToTranistion;
        [SerializeField] private UIManager.WindowType trasitionWindowType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Root.UIManager.OpenPanel(trasitionWindowType);
                Root.TransitionManager.SetTransition(sceneIndexToTranistion);
            }
        }
    }
}
