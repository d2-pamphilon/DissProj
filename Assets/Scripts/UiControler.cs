using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    private bool UIOn = true;
    private GameObject[] UIObjects;

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

   

    public void setText(string name, string t_text)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == name)
            {
                Text t_t = T.GetComponentInChildren<Text>();
                t_t.text = t_text;
            }
        }

    }

    public void setSliderInt(string name, int value)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == name)
            {
                T.GetComponent<Slider>().value = value;
            }
        }
    }

    public void setSliderFloat(string name, float value)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == name)
            {
                T.GetComponent<Slider>().value = value;
            }
        }
    }

    public void setCheckbox(string name, bool on_off)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == name)
            {
                T.GetComponent<Toggle>().isOn = on_off;
            }
        }
    }

    public void setAngleText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Angle %")
            {
                T.GetComponent<Text>().text = temp.ToString() + " %";
            }
        }
    }

    public void setIterationText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "It %")
            {
                T.GetComponent<Text>().text = temp.ToString() + " %";
            }
        }
    }
    public void setStochText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Stoch %")
            {
                T.GetComponent<Text>().text = temp.ToString() + " %";
            }
        }
    }
    public void setRotText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Rot %")
            {
                T.GetComponent<Text>().text = temp.ToString() + " %";
            }
        }
    }



}
