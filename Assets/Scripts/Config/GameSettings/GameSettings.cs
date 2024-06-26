using UnityEngine;

    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Configs" + "/" + nameof(GameSettings))]
    public class GameSettings : ScriptableObject
    {
        public PlayerConfig Player;
    }
