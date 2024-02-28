using UnityEngine;
using ZombieFarm.Managers;

namespace ZombieFarm.LocationTransition
{
    public class LocationTransitionArea : MonoBehaviour
    {
        [SerializeField] private UIManager.WindowType trasitionWindowType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Root.UIManager.OpenPanel(trasitionWindowType);
            }
        }
    }
}
