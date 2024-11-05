using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GuideProcess : MonoBehaviour
{
    public TextMeshProUGUI text;
    private List<string> textList;
    private int textIndex = 0;
    public GameObject GuidingPoint;
    // Start is called before the first frame update
    void Start()
    {
        textList = new List<string>()
        {
            "Ready for your own bee farm? Let's go!",
            "First step, let's set up your bee hive. Click the shop and purchase a hive.",
            "This is a shop for bee lovers, items are nice and cheap. Buy anything you want! As long as you have enough money...",
            "Now, let's buy a hive.",
            "See? The hive has been settled, it contains a new queen and small amount of bee, they are ready for their new life!",
            "Yes, now we need a honey supper, then we can wait for harvest in next year.",
            "How about we plant some flower for the bee, plenty of pollen will help them produce more honey.",
            "Well done! As you can see these two bars, one for the bee amount and one for the honey.",
            " Don't forget to buy empty box to expand room when bee amount reach the limition, or half of the bee will leave the hive with a new queen~",
            "And this, you can click the hive to check its status, check it often~",
            "Now, you are in charge. Click next season button when you think everything has been settled.",//11
            "As you can see, weather is changing every season, good weather help bee produce and collect honey",
            "When its rainy, bee will stay at hive. So, no honey for the rainy season~ And they may need extra feed",
            "Preparation for the winter is really important, having enough food will effectively decrease winter loss",
            "Make sure you have fed them~",
            "Looks like your bee farm meets its first winter, winter can be tough, bee's amount will decrease and of course no honey produce.",
            "Beekeepers should remove excess hives, leaving the bees with less space to keep them warm.",
            "And there's snow in the winter, make sure you clear the snow on the enterance of the hive, or bee may suffer from lack of oxygen",
            ""
        };
        text.text = textList[textIndex];
    }

    // Update is called once per frame
    void Update()
    {
        NextSentence();
    }

    void NextSentence()
    {
        if(textIndex < textList.Count - 1)
        {
            if (GameObject.Find("GuidingPoint(Clone)") == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    NextAction();
                    textIndex++;
                };
                text.text = textList[textIndex];
            }
        }
        
    }

    void NextAction()
    {
        switch(textIndex + 1)
        {
            case 0: break;
            case 1: Instantiate(GuidingPoint, new Vector3(0,0,0),Quaternion.identity); break;//create the guide point at shop icon
            case 2: break;
            case 3: Instantiate(GuidingPoint, new Vector3(0, 0, 0), Quaternion.identity);break;//at hiveitem
            case 4: break;//1.set up hive 2.close shop scene 
            case 5: Instantiate(GuidingPoint, new Vector3(0, 0, 0), Quaternion.identity); break;//1.open the shop scene 2. create at honey supper
            case 6: Instantiate(GuidingPoint, new Vector3(0, 0, 0), Quaternion.identity); break;//1. create at flower
            case 11: gameObject.SetActive(false); break;
            case 13: gameObject.SetActive(false); break;
            case 15: gameObject.SetActive(false); break;
            case 17: gameObject.SetActive(false); break;
        }
    }
}
