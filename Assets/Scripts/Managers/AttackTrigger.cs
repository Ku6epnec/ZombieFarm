using UnityEngine;

namespace ZombieFarm.Views.Player
{
    public class AttackTrigger : MonoBehaviour
    {
        private ReceivedDamageObject receivedDamageObject;

        private GameObject interactiveObject;
        private PlayerConfig playerConfig;

        private void Start()
        {
            playerConfig = Root.ConfigManager.GameSettings.Player;
        }

        private void OnTriggerEnter(Collider other)
        {
            interactiveObject = other.gameObject;
            receivedDamageObject = null;
            if (interactiveObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject _receivedDamageObject))
            {
                receivedDamageObject = _receivedDamageObject;
            }
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            if (receivedDamageObject != null)
            {
                receivedDamageObject.Interaction(playerConfig.damage);
            }
        }
    }
}
