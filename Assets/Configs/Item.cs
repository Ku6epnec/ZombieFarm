
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Configs/Item", order = 1)]
public class Item : ScriptableObject
{
    public string NameId;
    public float PriceInSoftCurrency;
    public bool CanBeSold;
}

