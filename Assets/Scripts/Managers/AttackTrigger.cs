using UnityEngine;

namespace ZombieFarm.Views.Player
{
    public class AttackTrigger : MonoBehaviour
    {
        private GameObject InteractiveObject;
        private PlayerView playerView;

        private ReceivedDamageObject ReceivedDamageObject;

        private void Start()
        {
            playerView = Root.ViewManager.GetPlayerView();
        }

        private void OnTriggerEnter(Collider other)
        {
            InteractiveObject = other.gameObject;
            if (InteractiveObject.TryGetComponent<ReceivedDamageObject>(out ReceivedDamageObject receivedDamageObject))
            {
                ReceivedDamageObject = receivedDamageObject;
            }
            ApplyDamage();
        }

        private void ApplyDamage()
        {
            if (ReceivedDamageObject != null)
            {
                ReceivedDamageObject.Interaction(playerView.Damage);
            }
        }
    }
}
