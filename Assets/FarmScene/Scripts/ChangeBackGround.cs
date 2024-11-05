using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackGround : MonoBehaviour
{
    
   // public Sprite[] background;
   // private int backGroundIndex = 0;
   // public SpriteRenderer sr;

    public GameObject Weather;
    public SpriteRenderer weatherSr;
    public Sprite[] weather;
    private int weatherIndex = 0;


    public static ChangeBackGround Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //sr = GetComponent<SpriteRenderer>();
       // sr.sprite = background[backGroundIndex];
        
        weatherSr = Weather.GetComponent<SpriteRenderer>();
        weatherSr.sprite = weather[weatherIndex];

    }

    // Update is called once per frame
    public void SeasonChange()
    {
        //backGroundIndex = (int)FarmManager.Instance.season;
       // sr.sprite = background[backGroundIndex];
        weatherSr.sprite = weather[(int)FarmManager.Instance.weather];
    }
}
