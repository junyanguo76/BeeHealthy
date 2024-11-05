using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public Button CloseButton;
    public Button OpenButton;

    public GameObject ShopItem;

    public Transform GridTransform;

    public List<ShopItem> ShopItems = new List<ShopItem>();

    public static ShopManager Instance { get; private set; }

    public Season currentSeason { 
        get { 
            return GameManager.Instance.season; 
        } 
    }
    void Start()
    {
        SpawnItems();
        CloseButton.onClick.AddListener(CloseMenu);
        OpenButton.onClick.AddListener(OpenMenu);

        gameObject.SetActive(false);
        ShopItem.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SpawnItems()
    {
        foreach (ShopItem item in ShopItems)
        {
            GameObject item_obj = Instantiate(ShopItem, GridTransform);
            item.reference = item_obj;
            item_obj.GetComponent<ShopItemScript>().SetItem(item);
        }
    }

    public void RefreshItems()
    {
        foreach (ShopItem item in ShopItems)
        {
            item.reference.GetComponent<ShopItemScript>().SetItem(item);
            item.CheckAvailability();
            if (item.season.HasSeason(currentSeason) && item.isAvailable)
            {
                item.reference.SetActive(true);
            }
        }
    }

    private void OpenMenu()
    {
        OpenButton.gameObject.SetActive(false);
        CloseButton.gameObject.SetActive(true);
        gameObject.SetActive(true);

        foreach (ShopItem item in ShopItems)
        {
            item.CheckAvailability();
            if (item.season.HasSeason(currentSeason) && item.isAvailable)
            {
                item.reference.SetActive(true);
            }
        }
    }

    private void CloseMenu()
    {
        OpenButton.gameObject.SetActive(true);
        CloseButton.gameObject.SetActive(false);

        ShopItemScript[] all_menu_items = FindObjectsOfType<ShopItemScript>();
        foreach (ShopItemScript obj in all_menu_items)
        {
            obj.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void BuyBeeBox()
    {
        Debug.Log("Buying beebox");
    }

    public void BuyBeeBox2()
    {
        Debug.Log("Buying beebox 2");
    }

    public void ConditionCheckFalse(ShopItem item)
    {
        item.isAvailable = false;
    }
    public void ConditionCheckTrue(ShopItem item) 
    { 
        item.isAvailable = true;
    }

}
