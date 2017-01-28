using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class ReWriter : MonoBehaviour
{
    public string Axiom;
    public string StringA;//move to private
    public string StringB;//move to private
    public bool flip = true;  //true = A    false = B //move to private - get, set
    public bool generate = false;//move to private - get set
    public int iterations;
    public string fileName;
    public bool stochY;
    public bool stochX;
    public bool stochF;

    //  \/move to private \/ - use ui for inputs
    public string ruleX;
    public string ruleF;
    public string ruleY;
    public string ruleBO;
    public string ruleBC;
    public string ruleN;
    public string ruleP;

    InputField input;
    InputField.SubmitEvent se;

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
        stochY = true;
        stochX = true;
        stochF = true;
        input = GameObject.FindGameObjectWithTag("Input").GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.onEndEdit = se;

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
                    if (stochX)
                    {
                        if (rndBool(50))
                        {
                            writeTo = writeTo.Insert(i, ruleX);
                            i += ruleX.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "x");
                            i++;
                        }
                    }
                    else
                    {
                        writeTo = writeTo.Insert(i, ruleX);
                        i += ruleX.Length;
                    }



                    break;

                case 'f':
                case 'F':
                    if (stochF)
                    {
                        if (rndBool(50))
                        {
                            writeTo = writeTo.Insert(i, ruleF);
                            i += ruleF.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "f");
                            i++;
                        }
                    }
                    else
                    {
                        writeTo = writeTo.Insert(i, ruleF);
                        i += ruleF.Length;
                    }

                    break;
                case 'y':
                case 'Y':
                    if (stochY)
                    {
                        if (rndBool(50))
                        {
                            writeTo = writeTo.Insert(i, ruleY);
                            i += ruleY.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "y");
                            i++;
                        }
                    }
                    else
                    {
                        writeTo = writeTo.Insert(i, ruleY);
                        i += ruleY.Length;
                    }
                    break;
                case '[':

                    writeTo = writeTo.Insert(i, ruleBO);
                    i += ruleBO.Length;

                    break;

                case ']':

                    writeTo = writeTo.Insert(i, ruleBC);
                    i += ruleBC.Length;

                    break;
                case '+':


                    writeTo = writeTo.Insert(i, ruleP);
                    i += ruleP.Length;

                    break;

                case '-':
                case '−':


                    writeTo = writeTo.Insert(i, ruleN);
                    i += ruleN.Length;

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

    private void SubmitInput(string arg0)
    {
        //  string currentText = output.text; //maybe add ToString()?
        //string newText = currentText + "\n" + arg0;
        //output.text = newText;
        //input.text = "";
        //input.ActivateInputField();
        print(arg0);
        fileName = arg0;
    }

    public void save()
    {
        //12 lines
        StreamWriter sw = new StreamWriter(fileName + ".txt");
        sw.WriteLine(Axiom);//axiom
        sw.WriteLine(iterations);//iterations
        sw.WriteLine(stochY);
        sw.WriteLine(stochX);
        sw.WriteLine(stochF);
        sw.WriteLine(ruleX);
        sw.WriteLine(ruleF);
        sw.WriteLine(ruleY);
        sw.WriteLine(ruleBO);
        sw.WriteLine(ruleBC);
        sw.WriteLine(ruleN);
        sw.WriteLine(ruleP);
        sw.WriteLine(getAngle());
        sw.Close();
    }

    public void load()
    {
        StreamReader sr = new StreamReader(fileName + ".txt");
        Axiom = sr.ReadLine();
        iterations = int.Parse(sr.ReadLine());
        stochY = bool.Parse(sr.ReadLine());
        stochX = bool.Parse(sr.ReadLine());
        stochF = bool.Parse(sr.ReadLine());
        ruleX = sr.ReadLine();
        ruleF = sr.ReadLine();
        ruleY = sr.ReadLine();
        ruleBO = sr.ReadLine();
        ruleBC = sr.ReadLine();
        ruleN = sr.ReadLine();
        ruleP = sr.ReadLine();
        forceSetAngle(float.Parse(sr.ReadLine()));
        sr.Close();

    }
    private bool rndBool(float value)
    {
        if (Random.value >= value / 100)//between 0-1
        { return true; }
        else
        { return false; }
    }

    private float getAngle()
    {
        Interpreter inter = GetComponent<Interpreter>();
        return inter.angle;
    }

    private void forceSetAngle(float m_angle)
    {
        Interpreter inter = GetComponent<Interpreter>();
        inter.angle = m_angle;
    }

}








