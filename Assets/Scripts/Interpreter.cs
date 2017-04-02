﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interpreter : MonoBehaviour
{
    private enum pass
    {
        none,
        first,
        main,
        save
    }

    public string interSting;//move to private
    public GameObject turtle;
    public GameObject trunk;
    public Material green;
    public Material red;
    public float angle;
    public float rotationOfTrunk;
    public int mode; //cant pass enum into switch witout converting to int, so just have int instead
    public GameObject cutOff;
    [SerializeField]
    pass state;
    public bool Prune;

    private static Stack<Vector3> thePosStack = new Stack<Vector3>();
    private static Stack<Quaternion> theRotStack = new Stack<Quaternion>();
    private ReWriter reWrite;
    private GameObject parentObject;


    void Start()
    {
        //get ReWriting script
        reWrite = GetComponent<ReWriter>();
        state = pass.none;
    }

    void Update()
    {
        if (state == pass.none)
        {


        }
        if (state == pass.first || state == pass.main)
        {
            interSting = reWrite.getFinalString();
            centerTurtle();
            mainLoop();
            state = pass.none;
        }
        if (state == pass.save)
        {
            //create prefab
            //child all objects 
            //save to
        }

        //if generate bool is true
        //if (reWrite.getGen())
        //{
        //    //get the final string for interptiation

        //    //turn gen back off
        //    reWrite.setGen(false);
        //}

        //if reset key pressed 
        if (Input.GetKeyDown("r"))
        {
            //delet all trunks 
            foreach (GameObject T in GameObject.FindGameObjectsWithTag("Player"))
            { Destroy(T); }

        }
    }
    bool insideCheck(Vector3 curPos)
    {
        //if (cutOff.GetComponent<Collider>().bounds.Contains(curPos))
        //{ return true; }
        //else
        //{ return false; }

        if (Vector3.Distance(cutOff.transform.position, curPos) < cutOff.GetComponent<SphereCollider>().radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void centerTurtle()
    {
        //places turtle in the middle of the screen, 20X closer than the far plain
        turtle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
        cutOff.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
        turtle.transform.rotation = Quaternion.LookRotation(Vector3.forward);
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
                    //included for future
                    //can have multiple "modes" form same code
                    switch (mode)
                    {
                        case 1:
                            X2D();
                            break;
                        case 2:
                            XTree();
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
                            F2D();
                            break;
                        case 2:
                            FTree();
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
                            YTree();
                            //extra mode
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
                    switch (mode)
                    {
                        case 1:
                            P2D();
                            break;
                        case 2:
                            PTree();
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
                            NTree();
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

    private void F2D()
    {

        if (insideCheck(turtle.transform.position))
        {
            if (state == pass.first)
            {
                parentObject = Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);

                state = pass.main;
            }
            if (state == pass.main)
            {
                Instantiate(trunk, turtle.transform.position, turtle.transform.rotation, parentObject.transform);

            }
        }
        turtle.transform.Translate(Vector3.up * 2);



    }

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

    //comon functions
    private void FTree()
    {
        if (Prune)
        {
            if (insideCheck(turtle.transform.position))
            {
                //create trunk at turtles location and rotation
                Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);
                //move turtle forward

            }
        }
        else
        {
            Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);
        }

        turtle.transform.Translate(Vector3.up * 2);
        //add rotation for simple 3d 
        turtle.transform.Rotate(new Vector3(0f, rotationOfTrunk, 0f));

    }
    private void XTree()
    {/*nothing atm*/ }

    private void YTree()
    { /*nothing atm*/}

    //varience in angle
    private void PTree()
    {
        //angle right with slight varience 
        float tempAngle = Random.Range(angle - 5, angle + 5);
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, -tempAngle));
    }

    private void NTree()
    {
        //angle left with slight varience
        float tempAngle = Random.Range(angle - 5, angle + 5);
        turtle.transform.Rotate(new Vector3(0.0f, 0.0f, tempAngle));
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
        Prune = temp;

    }
    public void setAngle(float temp)
    {
        angle = (int)temp;
    }
    public void setTrunkRot(float temp)
    {
        rotationOfTrunk = (int)temp;
    }

}




