using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interpreter : MonoBehaviour
{

    public string interSting;//move to private
    public GameObject turtle;
    public GameObject trunk;
    public Material green;
    public Material red;
    public float angle;

    private int mode = 1;
    private static Stack<Vector3> thePosStack = new Stack<Vector3>();
    private static Stack<Quaternion> theRotStack = new Stack<Quaternion>();

    void Start()
    {/*do nothing*/ }

    // Update is called once per frame
    void Update()
    {
        //get ReWriting script
        ReWriter reWrite = GetComponent<ReWriter>();
        //if generate bool is true
        if (reWrite.generate)
        {
            //get the final string for interptiation
            interSting = reWrite.getFinalString();
            //place turtle in center of screen
            centerTurtle();
            //cinterpritaion
            mainLoop();
            //turn gen back off
            reWrite.generate = false;
        }

        //if reset key pressed 
        if (Input.GetKeyDown("r"))
        {
            //delet all trunks 
            foreach (GameObject T in GameObject.FindGameObjectsWithTag("Player"))
            { Destroy(T); }

        }
    }
    private void centerTurtle()
    {
        //places turtle in the middle of the screen, 20X closer than the far plain
        turtle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
    }

    private void mainLoop()
    {
        //look at each character in the final string
        foreach (char c in interSting)
        {
            switch (c)
            {
                //1 and 2 used for colour changing 
                //switches out materials 
                case '1':
                    trunk.GetComponentInChildren<Renderer>().material = green;
                    break;

                case '2':
                    trunk.GetComponentInChildren<Renderer>().material = red;
                    break;
                //both caps and lowercase will work
                case 'x':
                case 'X':
                    //included for futer
                    //can have multiple "modes" form same code
                    switch (mode)
                    {
                        case 1:
                            XTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }

                    break;

                case 'f':
                case 'F':
                    switch (mode)
                    {
                        case 1:
                            FTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }

                    break;

                case 'y':
                case 'Y':
                    switch (mode)
                    {
                        case 1:
                            YTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }

                    break;
                case '[':
                    switch (mode)
                    {
                        case 1:
                            BOTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }
                    break;

                case ']':
                    switch (mode)
                    {
                        case 1:
                            BCTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }
                    break;
                case '+':
                    switch (mode)
                    {
                        case 1:
                            PTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }

                    break;

                case '-':
                case '−':
                    switch (mode)
                    {
                        case 1:
                            NTree();
                            break;
                        case 2:
                            //extra mode
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }
    }


    private void XTree()
    {/*do nothing*/}


    private void FTree()
    {
        //create trunk at turtles location and rotation
        Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);
        //move turtle forward
        turtle.transform.Translate(Vector3.up * 2);
    }

    private void YTree()
    { /*nothing*/ }

    private void BOTree()
    {
        //place turtles position into the position stack
        Vector3 tempPos = turtle.transform.position;
        thePosStack.Push(tempPos);
        //place turtles rotation into the rotation stack
        Quaternion tempRot = turtle.transform.rotation;
        theRotStack.Push(tempRot);
    }


    private void BCTree()
    {
        // move turtle to position on top of stack
        turtle.transform.position = thePosStack.Pop();
        //rotate turtle to rotation on stack
        turtle.transform.rotation = theRotStack.Pop();
    }

    private void PTree()
    {
        //angle right
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }

    private void NTree()
    {
        //angle left
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, -angle));
    }

}
