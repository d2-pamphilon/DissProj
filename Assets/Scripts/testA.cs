using UnityEngine;
using System.Collections;

public class testA : MonoBehaviour
{

    public int iterations;
    public string axom;
    public string newAxom ;
    public int stringLength;
    public bool once = true;


    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {  codex();
            once = false;
        }
    }

    private void codex()
    {
        stringLength = axom.Length;
        for (int i = 0; stringLength < i; i++)
        {
            if (axom[i] == 'a')
            {

                newAxom = newAxom.Insert(i, "ab");
            }
            else
            {
                newAxom = newAxom.Insert(i, "b");
            }

           
            print(newAxom);
        }

    }
}
