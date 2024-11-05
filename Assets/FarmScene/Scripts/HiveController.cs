using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Unity.Mathematics;

public class HiveController : MonoBehaviour
{
    

    public float beeAmount;
    public float honeyAmount;
    public int hiveAmount;

    public float mitesAmount;

    public bool queenExist;
    public int queenPurchaseSeason;
    public AnimationCurve queenCurve;

    public float wind;
    public float sun;
    public float flower;

    public GameObject HarvestIcon;
    public GameObject HarvestPanel;
    public GameObject FeedIcon;
    public GameObject FeedSelection;
    public SpriteRenderer FeedSr;

    public GameObject SnowCover;
  
    public GameObject QueenIcon;

    public Image BeeBar;
    public Image HoneyBar;
    private float beeBarLength;
    private float honeyBarLength;

    public List<GameObject> HiveList;
    // Start is called before the first frame update


    void Start()
    {
        beeAmount = 2;
        honeyAmount = 0;
        hiveAmount = 1;
        mitesAmount = 0;
        queenExist = true;
        queenPurchaseSeason = FarmManager.Instance.passedSeasonsCounter;

       
        wind = -2;
        sun = 2;
        flower = 2;

        beeBarLength = BeeBar.rectTransform.rect.width;
        honeyBarLength = HoneyBar.rectTransform.rect.width;
        BeeBarChange(beeAmount, hiveAmount * 10);
        HoneyBarChange(honeyAmount, hiveAmount * 100);
    }
    public void BeeProduce()
    {
        if (queenExist)
        {
            switch ((int)FarmManager.Instance.season)
            {
                case 0:
                    beeAmount -= 5; break;
                case 1:
                    beeAmount += 2; break;
                case 2:
                    beeAmount += 4; break;
                case 3:
                    beeAmount += 2; break;
            }
            if (beeAmount > hiveAmount * 10) { beeAmount /= 2; }
            QueenJudge();
            QueenProduce(queenExist);
            Debug.Log("beeAmount is " + beeAmount);
        }
        else
        {
            if((int)FarmManager.Instance.season  == 0)
            {
                beeAmount = 0;
                Debug.Log("The queen didn't survive the winter, as well as the hive...");
            }
            else
            {
                beeAmount *= 0.8f;
                Debug.Log("Because of lack of queen, the bee amount keep decreasing, " +
                    "they won't survivr the winter...");
            }           
        }
        MiteCheck();
        BeeBarChange(beeAmount, hiveAmount * 10);
        if (queenExist == false) QueenIcon.SetActive(true);
        else QueenIcon.SetActive(false);
    }

    public void HoneyProduce()
    {
        honeyAmount += (wind + sun + flower) * beeAmount ;
        if (honeyAmount > hiveAmount * 100) honeyAmount = hiveAmount * 100; 
        HoneyBarChange(honeyAmount, hiveAmount * 100);
    }



    //Harvest Part

    IEnumerator HarvestPopLogic()
    {
        HarvestIcon.SetActive(true);
        HarvestIcon.transform.SetAsLastSibling();
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        Tweener moveUP = HarvestIcon.transform.DOLocalMoveY(210, 1);
        Tweener moveDown = HarvestIcon.transform.DOLocalMoveY(188, 1);
        sequence.Append(moveUP);
        sequence.Append(moveDown);
        sequence.SetLoops(-1);
        yield return null;
        }
        
    public void HarvestDetection()
    {
        if((int)FarmManager.Instance.season == 1 && FarmManager.Instance.passedSeasonsCounter != 1)
        {
            StartCoroutine(HarvestPopLogic());
        }
        else
        {
            HarvestIcon.SetActive(false);
        }
        
    }
    public void OpenHarvestPanel()
    {
        HarvestIcon.SetActive(false);
        HarvestPanel.SetActive(true);
        HarvestPanel.transform .SetAsLastSibling();
    }

    //selection result will be (0,0.5,1) of honey
    public void HarvestOption(float selectionResult)
    {
        FarmManager.Instance.ChangeMoney(selectionResult * honeyAmount * 100);
        honeyAmount *= (1 - selectionResult);
        HarvestPanel.SetActive(false);
        HoneyBarChange(honeyAmount, hiveAmount * 100);
        HarvestIcon.SetActive(false);
    }

    public void CloseHarvestSelectionPanel()
    {
        HarvestPanel.SetActive(false);
        HarvestIcon.SetActive(true);
    }
        

    //Feed Part

    public void FeedDetection()
    {
        if ((int)FarmManager.Instance.season == 2)
        {
            StartCoroutine(NormalFeedLogic());
        }
        //else if(FarmManager.Instance.weather == "Rainy")
        //{
        //    StartCoroutine(UrgentLogic());
        //}
        else
        {
            FeedIcon.SetActive(false);
        }
    }

    IEnumerator NormalFeedLogic()
    {
        FeedIcon.SetActive(true);
        FeedIcon.transform.SetAsLastSibling();
        Tween goUp = FeedIcon.transform.DOLocalMoveY(222, 0.5f);
        Tween goDown = FeedIcon.transform.DOLocalMoveY(183, 0.5f);
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(goUp);
        sequence.Append(goDown);
        sequence.SetLoops(-1);
        yield return null;
    }

    //This is selectable method to simulate bee are very hungry so the feed icon moves fast
    //IEnumerator UrgentFeedLogic()
    //{
    //    FeedIcon.SetActive(true);
    //    Tween start = FeedSr.DOFade(0.3f, 1);
    //    Tween back = FeedSr.DOFade(1,1);
    //    Sequence sequence = DOTween.Sequence();
    //    sequence.Append(start);
    //    sequence.Append(back);
    //    sequence.SetLoops(-1);
    //    yield return null;
    //}

    public void FeedFunction()
    {
        FarmManager.Instance.ChangeMoney(-beeAmount * 10);
        FeedIcon.SetActive(false );
    }


    //This method is for drawing all the hives on the top
    public void SetHiveList()
    {
        if(hiveAmount > 1)
        {
            for(int i = 0; i < hiveAmount - 1; i++)
            {
                HiveList[i].SetActive(true);
            }
        }
    }

    //This method means if the weather is snow, the snow cover sprite will show in the enterance of the hive
    public void SnowDetection(bool snow)
    {
        SnowCover.SetActive(snow);
    }
    
    //This method is for the snow cover button, after clicking certain times, the snow will despair
    public void CleanSnow()
    {
       Slider snowSlider = SnowCover.GetComponentInChildren<Slider>();
        snowSlider.value += 0.05f;
        if(snowSlider.value == 1) SnowCover.SetActive(false);
    }

    public void QueenJudge()
    {
        if (queenExist == false)  return; 
        int currentSeasonIndex = FarmManager.Instance.passedSeasonsCounter;
        int queenLivingTime = currentSeasonIndex - queenPurchaseSeason;
        float random = UnityEngine.Random.Range(0.0f,1.0f);
        if (random < queenCurve.Evaluate(queenLivingTime)) queenExist = false;
        QueenProduce(queenExist);
    }

    public void QueenProduce(bool queenAlive)
    {
        if (queenAlive == true)  return; 
        int temp = UnityEngine.Random.Range(0, 2);
        if(temp == 0) { Debug.Log("Your queen has dead, no new queen born in the nest."); }
        else
        {
           queenExist = true;
           queenPurchaseSeason = FarmManager.Instance.passedSeasonsCounter;
           Debug.Log("The queen has dead, but fortunately a new queen has born in your nest "); 
        }
    }

    
    public void MiteCheck()
    {
        if(mitesAmount == 0)
        {
            int temp = UnityEngine.Random.Range(0, 11);
            if(temp == 10) { mitesAmount = beeAmount * 10; }
            else { return; }
        }
        else
        {
            switch ((int)FarmManager.Instance.season)
            {
                case 0:
                    mitesAmount /= 2; break;
                case 1:
                    mitesAmount *= 4;break;
                case 2:
                    mitesAmount *= 2;break;
                case 3:
                    mitesAmount *= 2;break;
            }
        }
        if(mitesAmount > beeAmount * 500 && mitesAmount < beeAmount * 2100)
        {
            beeAmount *= 0.8f;
            Debug.Log("Too much mites in the hive, your bee are suffering.");
        }
         else if (mitesAmount > beeAmount * 2100)
        {
            queenExist = false;
            Debug.Log("Too much mites in the hive, the queen falls...");
        }
         if(beeAmount == 0)  mitesAmount = 0;
    }

    public void OpenOrCloseInfo()
    {
        UIManager.instance.OpenInfoPage(gameObject.GetComponent<HiveController>());
    }

    public void BeeBarChange(float beeAmount, float beeLimitation)
    {
        BeeBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, beeAmount / beeLimitation * beeBarLength);
    }

    public void HoneyBarChange(float honeyAmount, float honeyLimitation)
    {
        HoneyBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, honeyAmount / honeyLimitation * honeyBarLength);
    }
}

