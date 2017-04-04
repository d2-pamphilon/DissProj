﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

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
    public int mode = 0; //cant pass enum into switch witout converting to int, so just have int instead
    public GameObject cutOff;
    [SerializeField]
    pass state;
    public bool Prune = false;
   // private float sphereSize;

    private string fileName;
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
        {       }
        if (state == pass.first || state == pass.main)
        {
            interSting = reWrite.getFinalString();
            centerTurtle();
            mainLoop();
            state = pass.none;
        }
        if (state == pass.save)
        {

            PrefabUtility.CreatePrefab("Assets/" + fileName +".prefab", parentObject, ReplacePrefabOptions.ReplaceNameBased);
            state = pass.none;

        }  
    }
    bool insideCheck(Vector3 curPos)
    {
        if (Prune)
        {
            if (Vector3.Distance(cutOff.transform.position, curPos) <= cutOff.GetComponent<SphereCollider>().radius)//y u no work anym??
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
        turtle.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
        cutOff.transform.position = turtle.transform.position;// Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.farClipPlane / 20));
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
                        case 0:
                            X2D();
                            break;
                        case 1:
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
                        case 0:
                            F2D();
                            break;
                        case 1:
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
                        case 0:
                            Y2D();
                            break;
                        case 1:
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
                        case 0:
                            P2D();
                            break;
                        case 1:
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
                        case 0:
                            N2D();
                            break;
                        case 1:
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
            else if (state == pass.main)
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

        if (insideCheck(turtle.transform.position))
        {
            if (state == pass.first)
            {
                parentObject = Instantiate(trunk, turtle.transform.position, turtle.transform.rotation);

                state = pass.main;
            }
            else if (state == pass.main)
            {
                Instantiate(trunk, turtle.transform.position, turtle.transform.rotation, parentObject.transform);

            }
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
        Prune = temp;

    }
    public void setAngle(float temp)
    {
        angle = temp;
    }
    public void setTrunkRot(float temp)
    {
        rotationOfTrunk = (int)temp;
    }
    public void setMode(int temp)
    {
        mode = temp;
    }
    public void setFileName(string text)
    {
        fileName = text;
    }
    public void save()
    {
        state = pass.save;
    }
    //public void setSphere(float temp)
    //{
    //    sphereSize = temp;
    //}

}




