using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class to hold the information of a ShopItem
/// </summary>
[System.Serializable]
public class ShopItem
{
    // Fields to hold general information of the ShopItem
    public string title;
    public int price;
    public Sprite sprite;
    public bool isAvailable;

    // UnityEvent to check and set the availability of the ShopItem
    // This requires the method to be in the form of:
    // public void FunctionName(ShopItem item)
    public UnityEvent<ShopItem> conditionFunction;

    // UnityEvent to perform the purchase action
    public UnityEvent onPurchaseAction;

    [EnumFlag]
    public Season season;

    [HideInInspector]
    public GameObject reference;

    // Method to check the availability of the ShopItem
    public void CheckAvailability()
    {
        conditionFunction.Invoke(this);
    }

}
