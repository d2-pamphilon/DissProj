using UnityEngine;
using System.Collections;

public class ReWriter : MonoBehaviour
{
    public string Axiom;
    public string StringA;//move to private
    public string StringB;//move to private
    public bool flip = true;  //true = A    false = B //move to private - get, set
    public bool generate = false;//move to private - get set
    public int iterations;

    //  \/move to private \/ - use ui for inputs
    public string ruleX;
    public string ruleF;
    public string ruleY;
    public string ruleBO;
    public string ruleBC;
    public string ruleN;
    public string ruleP;


    void Start()
    {
        //set up everything
        Axiom = "f";
        //StringA = "F";
        iterations = 1;
        ruleX = "x";
        ruleF = "f";//1FF-[2-F+F+F]+[1+F-F-F]
        ruleY = "y";
        ruleBO = "[";
        ruleBC = "]";
        ruleP = "+";
        ruleN = "-";
    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed, run for x amount of iterations
        if (Input.GetKeyDown("space"))
        {
          //  if strings are empty, use axiom
            if (StringA == "" && StringB == "")
            {
                StringA = Axiom;
            }
            else
            {
                for (int i = 0; i < iterations; i++)
                {
                    rewrite();
                }
           }

        }

        if (Input.GetKeyDown("r"))
        { reset(); }
        if (Input.GetKeyDown("p"))
        { generate = true; }
    }

    private void reset()
    {
        //clear strings
         deleteA();
       /// StringA = "F";
        deleteB();
        //set flip to default 
        flip = true;
    }

    private void rewrite()
    {
        //write to stringB from A
        if (flip)
        {
            deleteB();
            StringB = stringReWrite(StringA);
            flip = false;
        }
        //write to stringA from B
        else
        {
            deleteA();
            StringA = stringReWrite(StringB);
            flip = true;
        }
    }

    private string stringReWrite(string readFrom)
    {
        //i keeps track of where in the new string to add to
        int i = 0;
        //temp string to write to
        string writeTo = "";
        foreach (char c in readFrom)
        {
            switch (c)
            {
                //1 and 2 just stay as themselves
                case '1':
                    writeTo = writeTo.Insert(i, "1");
                    i += 1;
                    break;
                case '2':
                    writeTo = writeTo.Insert(i, "2");
                    i += 1;
                    break;
                    //will take caps or lowercase
                case 'x':
                case 'X':
                    //if there is no rule, stays as itself
                    //if (ruleX == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "x");
                    //    i += 1;
                    //}
                    ////if there is a rule use that
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleX);
                        i += ruleX.Length;
                   // }

                    break;

                case 'f':
                case 'F':
                    //if (ruleF == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "f");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleF);
                        i += ruleF.Length;
                   // }
                    break;
                case 'y':
                case 'Y':
                    //if (ruleY == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "y");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleY);
                        i += ruleY.Length;
                   // }
                    break;
                case '[':
                    //if (ruleBO == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "[");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleBO);
                        i += ruleBO.Length;
                  //  }
                    break;

                case ']':
                    //if (ruleBC == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "]");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleBC);
                        i += ruleBC.Length;
                   // }
                    break;
                case '+':
                
                    //if (ruleP == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "+");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleP);
                        i += ruleP.Length;
                    //}
                    break;

                case '-':
                case '−':
             
                    //if (ruleN == "")
                    //{
                    //    writeTo = writeTo.Insert(i, "-");
                    //    i += 1;
                    //}
                    //else
                    //{
                        writeTo = writeTo.Insert(i, ruleN);
                        i += ruleN.Length;
                   // }
                    break;

                default:
                    //debug
                    break;
            }

        }
        //sends back new string based on old string
        return writeTo;
    }


    private void deleteA()//Clears string A
    {
        int i = StringA.Length;
        StringA = StringA.Remove(0, i);
    }

    private void deleteB()//Clears string B
    {
        int i = StringB.Length;
        StringB = StringB.Remove(0, i);
    }

    public string getFinalString()//Getter for strings
    {
        if (flip)
        { return StringA; }
        else
        { return StringB; }

    }

}
