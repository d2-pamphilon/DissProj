using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class ReWriter : MonoBehaviour
{
    //breaks if private
    public string StringA;
    public string StringB;


    private string m_axiom;    
    private int m_iterations;
    private bool m_stochY;
    private bool m_stochX;
    private bool m_stochF;
    private int m_stochChance;

    private string m_ruleX;
    private string m_ruleF;
    private string m_ruleY;
    private string m_ruleBO;
    private string m_ruleBC;
    private string m_ruleN;
    private string m_ruleP;

    private bool m_flip = true;  //true = A    false = B
    private bool m_generate = false;
    private string m_fileName;
    private UiControler m_UiC;

    void Start()
    {
        //defualt everything
        m_axiom = "";
        m_iterations = 1;
        m_ruleX = "x";
        m_ruleF = "f";
        m_ruleY = "y";
        m_ruleBO = "[";
        m_ruleBC = "]";
        m_ruleP = "+";
        m_ruleN = "-";
        m_stochY = false;
        m_stochX = false;
        m_stochF = false;

        //get refrence of ui controller
        m_UiC = GameObject.FindGameObjectWithTag("GameController").GetComponent<UiControler>();
    }

  
    public void GO()
    {
        //if both strings empty take axiom as stingA
        //shouldnt need this 
        //for security incase user breaks it somehow
        if (StringA == "" && StringB == "")
        {
            StringA = m_axiom;
            //rewrite string for n iterations
            for (int i = 0; i < m_iterations; i++)
            {
                rewrite();
            }
        }
        else
        {
            for (int i = 0; i < m_iterations; i++)
            {
                rewrite();
            }
        }
        //sets interpriter to start generating
        GetComponent<Interpreter>().setState("first");

    }

    public void reset()
    {
        //clear strings
        deleteA();
        deleteB();

        foreach (GameObject T in GameObject.FindGameObjectsWithTag("Player"))
        { Destroy(T); }
        //set flip to default 
        m_flip = true;
    }

    private void rewrite()
    {
        //write to stringB from A
        if (m_flip)
        {
            deleteB();
            StringB = stringReWrite(StringA);
            m_flip = false;
        }
        //write to stringA from B
        else
        {
            deleteA();
            StringA = stringReWrite(StringB);
            m_flip = true;
        }
    }

    private string stringReWrite(string readFrom)
    {
        //i keeps track of where in the new string to add to
        int i = 0;
        //temp string to write to
        string writeTo = "";

        //look at every character in the string
        foreach (char c in readFrom)
        {
            switch (c)
            {
                //1 and 2 just stay as themselves
                //1 and 2 are used for colour changing
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
                    //if using stochtiatic grammer chance rule wont apply
                    if (m_stochX)
                    {
                        if (rndBool(m_stochChance))
                        {
                            writeTo = writeTo.Insert(i, m_ruleX);
                            i += m_ruleX.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "x");
                            i++;
                        }
                    }
                    else
                    {
                        //add rule to string
                        writeTo = writeTo.Insert(i, m_ruleX);
                        //incress i to new string length
                        i += m_ruleX.Length;
                    }

                    break;
                case 'f':
                case 'F':
                    if (m_stochF)
                    {
                        if (rndBool(m_stochChance))
                        {
                            writeTo = writeTo.Insert(i, m_ruleF);
                            i += m_ruleF.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "f");
                            i++;
                        }
                    }
                    else
                    {
                        writeTo = writeTo.Insert(i, m_ruleF);
                        i += m_ruleF.Length;
                    }

                    break;
                case 'y':
                case 'Y':
                    if (m_stochY)
                    {
                        if (rndBool(m_stochChance))
                        {
                            writeTo = writeTo.Insert(i, m_ruleY);
                            i += m_ruleY.Length;
                        }
                        else
                        {
                            writeTo = writeTo.Insert(i, "y");
                            i++;
                        }
                    }
                    else
                    {
                        writeTo = writeTo.Insert(i, m_ruleY);
                        i += m_ruleY.Length;
                    }
                    break;
                case '[':

                    writeTo = writeTo.Insert(i, m_ruleBO);
                    i += m_ruleBO.Length;

                    break;

                case ']':

                    writeTo = writeTo.Insert(i, m_ruleBC);
                    i += m_ruleBC.Length;

                    break;
                case '+':

                    writeTo = writeTo.Insert(i, m_ruleP);
                    i += m_ruleP.Length;
                    break;
                case '-':
                case '−':
                    writeTo = writeTo.Insert(i, m_ruleN);
                    i += m_ruleN.Length;
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
        if (m_flip)
        { return StringA; }
        else
        { return StringB; }

    }

    //private void SubmitInput(string arg0)
    //{
    //    //print(arg0);
    //    m_fileName = arg0;
    //}

    public void save()
    {
        //open file of name given
        //write a line for any info to be stored
        StreamWriter sw = new StreamWriter(m_fileName + ".txt");
        sw.WriteLine(m_axiom);
        sw.WriteLine(m_iterations);
        sw.WriteLine(m_stochY);
        sw.WriteLine(m_stochX);
        sw.WriteLine(m_stochF);
        sw.WriteLine(m_ruleX);
        sw.WriteLine(m_ruleF);
        sw.WriteLine(m_ruleY);
        sw.WriteLine(m_ruleBO);
        sw.WriteLine(m_ruleBC);
        sw.WriteLine(m_ruleN);
        sw.WriteLine(m_ruleP);
        sw.WriteLine(getAngle());
        sw.Close();
    }

    public void load()//make set ui
    {
        float t_angle;
        //open file and read in in same order as saved
        StreamReader sr = new StreamReader(m_fileName + ".txt");
        m_axiom = sr.ReadLine();
        m_iterations = int.Parse(sr.ReadLine());
        m_stochY = bool.Parse(sr.ReadLine());
        m_stochX = bool.Parse(sr.ReadLine());
        m_stochF = bool.Parse(sr.ReadLine());
        m_ruleX = sr.ReadLine();
        m_ruleF = sr.ReadLine();
        m_ruleY = sr.ReadLine();
        m_ruleBO = sr.ReadLine();
        m_ruleBC = sr.ReadLine();
        m_ruleN = sr.ReadLine();
        m_ruleP = sr.ReadLine();
        t_angle = float.Parse(sr.ReadLine());
        sr.Close();

        forceSetAngle(t_angle);
        m_UiC.setText("Axiom", m_axiom);
        m_UiC.setText("Rule X", m_ruleX);
        m_UiC.setText("Rule F", m_ruleF);
        m_UiC.setText("Rule Y", m_ruleY);
        m_UiC.setText("Rule Open Bracket", m_ruleBO);
        m_UiC.setText("Rule Closed Bracket", m_ruleBC);
        m_UiC.setText("Rule Plus", m_ruleP);
        m_UiC.setText("Rule Minus", m_ruleN);

        m_UiC.setCheckbox("Y", m_stochY);
        m_UiC.setCheckbox("F", m_stochF);
        m_UiC.setCheckbox("X", m_stochX);

        m_UiC.setSliderInt("Iterations", m_iterations);
        m_UiC.setSliderFloat("Angle", t_angle);


    }

    private bool rndBool(float value)//given value 0-100 returns a bool based on that % chance
    {
        if (Random.value >= value / 100)//between 0-1
        { return true; }
        else
        { return false; }
    }

    private float getAngle()//gets angle from interpriter
    {
        Interpreter inter = GetComponent<Interpreter>();
        return inter.m_angle;
    }

    private void forceSetAngle(float m_angle)//sets interpriter angle
    {
        Interpreter inter = GetComponent<Interpreter>();
        inter.setAngle(m_angle);
    }

    //public bool getGen()//returns if generate is true
    //{ return m_generate; }
    //public void setGen(bool setter)//sets generate
    //{ m_generate = setter; }

    public void setFileName(string _text)
    {
        m_fileName = _text;
    }

    public void setAxiom(string _text)
    {
        m_axiom = _text;
    }

    public void setRuleX(string _text)
    {
        m_ruleX = _text;
    }

    public void setRuleY(string _text)
    {
        m_ruleY = _text;
    }
    public void setRuleF(string _text)
    {
        m_ruleF = _text;
    }

    public void setRuleBO(string _text)
    {
        m_ruleBO = _text;
    }
    public void setRuleBC(string _text)
    {
        m_ruleBC = _text;
    }

    public void setRuleN(string _text)
    {
        m_ruleN = _text;
    }
    public void setRuleP(string _text)
    {
        m_ruleP = _text;
    }
    public void setStochY(bool _temp)
    {

        m_stochY = _temp;
    }
    public void setStochX(bool _temp)
    {

        m_stochX = _temp;
    }
    public void setStochF(bool _temp)
    {

        m_stochF = _temp;
    }

    public void setIterations(float _temp)
    {
        m_iterations = (int)_temp;
    }

    public void setStochChance(float temp)
    {
        m_stochChance = (int)temp;
    }
}








