using System;
using UnityEngine;
using TMPro;

/// <summary>
/// Enum to represent the seasons, which can be combined using bitwise operations
/// </summary>
[System.Flags]
public enum Season
{
    Spring = 1 << 0,
    Summer = 1 << 1,
    Fall =   1 << 2,
    Winter = 1 << 3,
}



public static class Extensions
{

    static Season[] SeasonValues = (Season[])Enum.GetValues(typeof(Season));
    /// <summary>
    /// Get the next season from the current one
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static Season Next(this Season src)
    {
        int new_index = (Index(src) + 1) % SeasonValues.Length;
        return FromIndex(new_index);
    }

    /// <summary>
    /// Get the index of the current season
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static int Index(this Season src)
    {
        return Array.IndexOf(SeasonValues, src);
    }

    /// <summary>
    /// Get the season from the index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static Season FromIndex(int index)
    {
        return SeasonValues[index];
    }

    /// <summary>
    /// Check if a multi-season variable has a specific season
    /// </summary>
    /// <param name="seasons"></param>
    /// <param name="seasonToCheck"></param>
    /// <returns></returns>
    public static bool HasSeason(this Season multiSeason, Season season)
    {
        return (multiSeason & season) != 0;
    }
}


public class GameManager : MonoBehaviour
{
    public SpriteRenderer Background;

    // The order of the backgrounds should be Spring, Summer, Fall, Winter
    public Sprite[] Backgrounds;

    public Season season;

    public int balance;
    public TMP_Text BalanceIndicator;

    public static GameManager Instance { get; private set; }

    void Start()
    {
        if (Backgrounds.Length == 0) Debug.LogError("No backgrounds set in GameManager");

        NextSeason();
        UpdateBalance(balance);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextSeason()
    {
        season = season.Next();
        SetBackground();

        FarmManager.Instance.temp ++;
        if(FarmManager.Instance.temp >= 2) FarmManager.Instance.NextSeasonButton();
    }

    private void SetBackground()
    {
        Background.sprite = Backgrounds[season.Index()];
    }

    public void UpdateBalance(int amount)
    {
        balance = amount;
        BalanceIndicator.text = "$ " + balance;
    }

    public void IncreaseBalance(int amount)
    {
        UpdateBalance(balance + amount);
    }

    public bool DecreaseBalance(int amount)
    {
        if (balance < amount) 
            return false;


        UpdateBalance(balance - amount);
        return true;
    }
}
