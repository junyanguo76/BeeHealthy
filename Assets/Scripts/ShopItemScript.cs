using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class to attach to a ShopItem GameObject
/// </summary>
public class ShopItemScript : MonoBehaviour
{
    public TMP_Text TextBanner;
    public TMP_Text PriceBanner;

    public GameObject Icon;
    int price;

    public void SetItem(ShopItem item)
    {
        price = item.price;
        TextBanner.text = item.title;
        PriceBanner.text = item.price.ToString();

        Icon.GetComponent<Image>().sprite = item.sprite;
        Icon.GetComponent<Button>().onClick.AddListener(() => 
        {   
            // Method to check if the player can afford the item and perform the purchase action
            int playerMoney = GameManager.Instance.balance;
            if (GameManager.Instance.DecreaseBalance(price))
            {
                item.onPurchaseAction.Invoke();
                ShopManager.Instance.RefreshItems();
            }
         
        });

        // Check if item is within budget, if not grey out entire object
        if (GameManager.Instance.balance < price)
        {
            Icon.GetComponent<Button>().interactable = false;
            
        }

        gameObject.SetActive(false);
    }
}
