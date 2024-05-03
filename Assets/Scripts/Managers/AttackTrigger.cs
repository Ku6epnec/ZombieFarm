using UnityEngine;

namespace ZombieFarm.Views.Player
{
    public class AttackTrigger : MonoBehaviour
    {
        private PlayerView playerView;
        private ReceivedDamageObject receivedDamageObject;

        private GameObject interactiveObject;

        private void Start()
        {
            playerView = Root.ViewManager.GetPlayerView();
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
                receivedDamageObject.Interaction(playerView.Damage);
            }
        }
    }
}
