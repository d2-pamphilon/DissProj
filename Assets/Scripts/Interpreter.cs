using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

// X and Y are used for shaping the sting and have no functionality
// in this system, commented functions have been put in place for 
// future use  
public class Interpreter : MonoBehaviour
{
    private enum pass
    {
        none,
        first,
        main,
        save
    }


    public GameObject m_turtle;
    public GameObject m_trunk;
    public Material m_green;
    public Material m_brown;
    public float m_angle;

    private string m_interSting;
    private float m_rotationOfTrunk;
    private int m_mode = 0; 
    public GameObject m_cutOff;
    [SerializeField]
    pass state;
    private bool m_prune = false;

    private string m_fileName;
    private static Stack<Vector3> thePosStack = new Stack<Vector3>();
    private static Stack<Quaternion> theRotStack = new Stack<Quaternion>();
    private ReWriter reWrite;
    private GameObject m_parentObject;


    void Start()
    {
        //get ReWriting script
        reWrite = GetComponent<ReWriter>();
        state = pass.none;
    }

    void Update()
    {
        
        //if (state == pass.none)
        //{/*Do Nothing*/ }
        if (state == pass.first || state == pass.main)
        {
            m_interSting = reWrite.getFinalString();
            centerTurtle();
            mainLoop();
            state = pass.none;
        }
        if (state == pass.save)
        {
#if UNITY_EDITOR
            //creates prefab and puts the base trunk the rest are parented to in it
            PrefabUtility.CreatePrefab("Assets/" + m_fileName + ".prefab", m_parentObject, ReplacePrefabOptions.ReplaceNameBased);
            state = pass.none;
#endif
        }
    }

    bool insideCheck(Vector3 _curPos)
    {
        if (m_prune)//if prune ticked 
        {
            //if distance between center of sphere and where trunk will spawn
            //is less that the size of the sphere collider then spawn the trunk
            if (Vector3.Distance(m_cutOff.transform.position, _curPos) <= m_cutOff.GetComponent<SphereCollider>().radius)//y u no work anymore??
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return true;

        }
    }

    private void centerTurtle()
    {
        //places turtle in the middle of the screen, 20X closer than the far plain
        m_turtle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
        //makes prune sphere center the start point 
        m_cutOff.transform.position = m_turtle.transform.position;
        //turtle looks up so trees should depending on string go up
        m_turtle.transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    private void mainLoop()
    {
        //look at each character in the final string
        foreach (char c in m_interSting)
        {
            switch (c)
            {
                //1 and 2 used for colour changing 
                //switches out materials 
                case '1':
                    m_trunk.GetComponentInChildren<Renderer>().material = m_green;
                    break;

                case '2':
                    m_trunk.GetComponentInChildren<Renderer>().material = m_brown;
                    break;
                //both caps and lowercase will work
                case 'x':
                case 'X':
                    //included for future                 
                    switch (m_mode)
                    {
                        case 0:
                            //X2D();
                            break;
                        case 1:
                           // XTree();                      
                            break;
                        default:
                            break;
                    }
                    break;

                case 'f':
                case 'F':
                    switch (m_mode)
                    {
                        case 0:
                            F2D();
                            break;
                        case 1:
                            FTree();
                           
                            break;
                        default:
                            break;
                    }
                    break;

                case 'y':
                case 'Y':
                    switch (m_mode)
                    {
                        case 0:
                            //Y2D();
                            break;
                        case 1:
                            //YTree();                         
                            break;
                        default:
                            break;
                    }

                    break;
                case '[':
                    OnStack();
                    break;
                case ']':
                    OffStack();
                    break;

                case '+':
                    switch (m_mode)
                    {
                        case 0:
                            P2D();
                            break;
                        case 1:
                            PTree();
                          
                            break;
                        default:
                            break;
                    }
                    break;

                case '-':
                case '−':
                    switch (m_mode)
                    {
                        case 0:
                            N2D();
                            break;
                        case 1:
                            NTree();
                           
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    print("default");
                    break;
            }
        }
    }
  
    private void F2D()
    {
        //check if trunks allowed to spawn 
        if (insideCheck(m_turtle.transform.position))
        {
            //if its the first pass make the trunk the parrent object for the rest
            if (state == pass.first)
            {
                m_parentObject = Instantiate(m_trunk, m_turtle.transform.position, m_turtle.transform.rotation);
                state = pass.main;
            }
            else if (state == pass.main)
            {
                //created childed to parent
                Instantiate(m_trunk, m_turtle.transform.position, m_turtle.transform.rotation, m_parentObject.transform);

            }
        }
        //moves forward even if trunk dosnt spawn
        m_turtle.transform.Translate(Vector3.up * 2);
    }
  
    private void P2D()
    {
        //angle right
        m_turtle.transform.Rotate(new Vector3(0.0f, 0.0f, -m_angle));
    }

    private void N2D()
    {
        //angle left
        m_turtle.transform.Rotate(new Vector3(0.0f, 0.0f, m_angle));
    }


    private void FTree()
    {

        if (insideCheck(m_turtle.transform.position))
        {
            if (state == pass.first)
            {
                m_parentObject = Instantiate(m_trunk, m_turtle.transform.position, m_turtle.transform.rotation);

                state = pass.main;
            }
            else if (state == pass.main)
            {
                Instantiate(m_trunk, m_turtle.transform.position, m_turtle.transform.rotation, m_parentObject.transform);

            }
        }


        m_turtle.transform.Translate(Vector3.up * 2);
        //add rotation for simple 3d 
        m_turtle.transform.Rotate(new Vector3(0f, m_rotationOfTrunk, 0f));

    }

    //For exta functionality if wanted

    //private void XTree()
    //{/*nothing atm*/ }

    //private void YTree()
    //{ /*nothing atm*/}

    //private void Y2D()
    //{ /*nothing*/  }

    //private void X2D()
    //{/*do nothing*/}


    private void PTree()
    {
        //angle right with slight varience 
        float tempAngle = Random.Range(m_angle - 5, m_angle + 5);
        m_turtle.transform.Rotate(new Vector3(0.0f, 0.0f, -tempAngle));
    }

    private void NTree()
    {
        //angle left with slight varience
        float tempAngle = Random.Range(m_angle - 5, m_angle + 5);
        m_turtle.transform.Rotate(new Vector3(0.0f, 0.0f, tempAngle));
    }

    private void OnStack()
    {
        //place turtles position into the position stack
        Vector3 tempPos = m_turtle.transform.position;
        thePosStack.Push(tempPos);
        //place turtles rotation into the rotation stack
        Quaternion tempRot = m_turtle.transform.rotation;
        theRotStack.Push(tempRot);
    }

    private void OffStack()
    {
        // move turtle to position on top of stack
        m_turtle.transform.position = thePosStack.Pop();
        //rotate turtle to rotation on stack
        m_turtle.transform.rotation = theRotStack.Pop();
    }

    //setters
    public void setState(string g_state)
    {
        switch (g_state)
        {
            case "none":
                state = pass.none;
                break;
            case "first":
                state = pass.first;
                break;
            case "main":
                state = pass.main;
                break;
            case "save":
                state = pass.save;
                break;

        }

    }

    public void setPrune(bool temp)
    {
        m_prune = temp;

    }
    public void setAngle(float temp)
    {
        m_angle = temp;
    }
    public void setTrunkRot(float temp)
    {
        m_rotationOfTrunk = (int)temp;
    }
    public void setMode(int temp)
    {
        m_mode = temp;
    }
    public void setFileName(string text)
    {
        m_fileName = text;
    }
    public void save()
    {
        state = pass.save;
    }
 
}




