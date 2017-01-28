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

    private int mode = 1;//cant pass enum into switch witout converting to int, so just have int instead
    private static Stack<Vector3> thePosStack = new Stack<Vector3>();
    private static Stack<Quaternion> theRotStack = new Stack<Quaternion>();
    private ReWriter reWrite;


    void Start()
    { //get ReWriting script
        reWrite = GetComponent<ReWriter>();
    }

    // Update is called once per frame
    void Update()
    {
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
                            X2D();
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
                            Forward();
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
                            Y2D();
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
                            OnStack();
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
                            OffStack();
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
                            P2D();
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
                            N2D();
                            break;
                        case 2:
                            //extra mode
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

    //2d functions
    private void X2D()
    {/*do nothing*/}


    private void Y2D()
    { /*nothing*/    }


    private void P2D()
    {
        //angle right
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, -angle));
    }

    private void N2D()
    {
        //angle left
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }

    //3d tree specific functions

    private void XTree()
    { }

    private void YTree()
    { }

    //varience in angle
    private void PTree()
    { }

    private void NTree()
    { }

    //comon functions
    private void Forward()
    {
        //create trunk at turtles location and rotation
        Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);
        //move turtle forward
        turtle.transform.Translate(Vector3.up * 2);
    }

    private void OnStack()
    { //place turtles position into the position stack
        Vector3 tempPos = turtle.transform.position;
        thePosStack.Push(tempPos);
        //place turtles rotation into the rotation stack
        Quaternion tempRot = turtle.transform.rotation;
        theRotStack.Push(tempRot);
    }

    private void OffStack()
    {
        // move turtle to position on top of stack
        turtle.transform.position = thePosStack.Pop();
        //rotate turtle to rotation on stack
        turtle.transform.rotation = theRotStack.Pop();
    }

    
}




