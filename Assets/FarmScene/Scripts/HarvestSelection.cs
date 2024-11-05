using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectinResult { Zero,Half,All}
public class HarvestSelection : MonoBehaviour
{
   public SelectinResult Result;


    public void ReturnSelection()
    {
        Transform parent = transform.parent;
         ((parent.transform.parent).transform.parent).transform.parent.GetComponent<HiveController>().HarvestOption((int)Result * 0.5f);
        //If player choose back button, set icon back to true
        if ((int)Result == 3) ((parent.transform.parent).transform.parent).transform.parent.GetComponent<HiveController>().HarvestIcon.SetActive(true);
    }
   
}
