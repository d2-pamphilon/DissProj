using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    private bool UIOn = true;
    private GameObject[] UIObjects;
   // private ReWriter RewriterRef;

    //public InputField fileName;
    void Start()
    {
       // RewriterRef = GameObject.FindGameObjectWithTag("GameController").GetComponent<ReWriter>();
        UIObjects = GameObject.FindGameObjectsWithTag("UI");

    }

    public void ToggleUI()
    {
        UIOn = !UIOn;


        if (UIOn)
        {

            foreach (GameObject T in UIObjects)
            {
                T.SetActive(true);

            }
        }
        else if (!UIOn)
        {
            foreach (GameObject T in UIObjects)
            {
                T.SetActive(false);

            }

        }


    }

  


}
