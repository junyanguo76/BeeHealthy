using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherList { Sunny,Windy,Rainy,Cloudy,Snow}
public enum SeasonList { Spring,Summer,Fall,Winter}

public class FarmManager : MonoBehaviour
{
    public static FarmManager Instance;


    public int passedSeasonsCounter;
    public WeatherList weather = WeatherList.Sunny;
    public SeasonList season = SeasonList.Spring;

    public HiveController[] hiveList;

    public GameObject Guidance;
    public int temp = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        passedSeasonsCounter = 0;
    }



    public void WeatherChange()
    {
        int weatherNum = Random.Range(0, 6);
        switch (weatherNum)
        {
            case 0:
            case 1:
            case 2:
                weather = WeatherList.Sunny;break;
            case 3:
                weather = WeatherList.Windy;break;
            case 4:
                weather = WeatherList.Rainy;break;
            case 5:
                weather = WeatherList.Cloudy;break;
                
        }

        if(weather == WeatherList.Rainy && season == SeasonList.Winter)
        {
            weather = WeatherList.Snow;
            foreach(HiveController temp in hiveList) temp.SnowDetection(true);
        }
        else { foreach (HiveController temp in hiveList) temp.SnowDetection(false); }
        ChangeBackGround.Instance.SeasonChange();
        Debug.Log(weather);
    }

    public void SeasonChange()
    {
        passedSeasonsCounter++;
        if ((int)season < 3) season++;
        else season = 0;
        Debug.Log(season);
    }

    public void NextSeasonButton()
    {
        hiveList = FindObjectsByType<HiveController>(FindObjectsSortMode.None);
        SeasonChange();
        WeatherChange();
        foreach (HiveController temp in hiveList)
        {
            temp.BeeProduce();
            temp.HoneyProduce();
            temp.HarvestDetection();
            temp.FeedDetection();
        }
        //GuidanceDetection();
    }

    //This method is mean to be used in other scripts to change the money account(like shopping or harvest)
    //and immediately show the new money amount in the money bar
    public void ChangeMoney(float amount)
    {
        if(amount >= 0)
        {
            GameManager.Instance.IncreaseBalance((int)amount);
        }
        else
        {
            GameManager.Instance.DecreaseBalance(-(int)amount);
        }
    }

    //Open guidance at center time
   //void GuidanceDetection()
   // {
   //     if(passedSeasonsCounter <= 3)
   //     {
   //         Guidance.SetActive(true);
   //     }
        
   // }
}
