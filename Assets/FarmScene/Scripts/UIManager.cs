using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Text MoneyBar;
    public GameObject InformationPage;
    public TextMeshProUGUI Information;

    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        //(ShowMoneyBar).GetComponent<TMP_Text>().text = (FarmManager.Instance.money.ToString());
        

    }
  

   

    

    public void OpenInfoPage(HiveController hive)
    {
        if(InformationPage.activeInHierarchy == false)
        {
            InformationPage.SetActive(true);
            InfoUpdate(hive);
            InformationPage.transform.SetAsLastSibling();
        }
        else
        {
            InformationPage.SetActive(false);
        }
    }

    public void CloseInfoPage()
    {
        InformationPage.SetActive(false);
    }
    public void InfoUpdate(HiveController hive)
    {
        Information.text =
            "Bee Amount:  " +hive.beeAmount + "\n" +
            "Honey Amount:  " +hive.honeyAmount + "\n" + "\n" + "\n" +
            "Flower:  " +hive.flower + "\n" +
            "Wind:  " +hive.wind + "\n" +
            "Sunlight:  " +hive.sun;
    }
}
