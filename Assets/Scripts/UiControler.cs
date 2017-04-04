using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiControler : MonoBehaviour
{
    private bool UIOn = true;
    private GameObject[] UIObjects; //list of all ui to be toggled, have to be list so can be toggled back on

    void Start()
    {
        UIObjects = GameObject.FindGameObjectsWithTag("UI");
    }

    public void ToggleUI()
    {
        //when button clicked toggles bool
        UIOn = !UIOn;

        if (UIOn)//if on active all ui
        {
            foreach (GameObject T in UIObjects)
            {
                T.SetActive(true);

            }
        }
        else if (!UIOn)//if off deactivate all
        {
            foreach (GameObject T in UIObjects)
            {
                T.SetActive(false);
            }
        }
    }

   //changes text of given object 
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

    //changes interger of given object
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

    //changes float of given object
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

    //sets checkbox of given object
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

    //sets text to be same as slider
    public void setAngleText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Angle %")
            {
                T.GetComponent<Text>().text = temp.ToString() + "°";
            }
        }
    }

    //sets text to be same as slider
    public void setIterationText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "It %")
            {
                T.GetComponent<Text>().text = temp.ToString();
            }
        }
    }
    
    //sets text to be same as slider
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
  
    //sets text to be same as slider
    public void setRotText(float temp)
    {
        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Rot %")
            {
                T.GetComponent<Text>().text = temp.ToString() + " °";
            }
        }
    }

    //sets text to be same as slider
    public void setSphere(float temp)
    {
       GameObject tempref= GameObject.FindGameObjectWithTag("Sphere");
        tempref.transform.localScale = new Vector3(temp, temp, temp);

        foreach (GameObject T in UIObjects)
        {
            if (T.name == "Sphere %")
            {
                T.GetComponent<Text>().text = temp.ToString();
            }
        }
    }
}
