using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : MonoBehaviour
{
    public GameObject bob;
    private Rigidbody2D m_Rigidbody;
    private sensor left;
    private sensor right;
    private int baseMoveSpeed = 10;
    private float moveSpeed;                                                                        //This is a fluid variable that will contain the movement speed of the ant based on enviromental factors
    private float turnSpeed;                                                                          //This is a fluid variable that will contain the turning speed of the ant based on enviromental factors
    private int baseTurnSpeed = 100;                                                               //This is the base turn speed of the ant
    private float dir;
    private float cert;                                                                               //This is to represent the ants certanty on where its going
    private Stack currentState;                                                                     //This will be an enum storing the current event state
    private int timer;
    enum State
    {
        Neutral,
        ForageFind,
        ForageNeutral,
        FoundFood,
        CircleStart,
        Circle,
        GoTo,
        TurnAround
    }
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        currentState = new Stack();
        left = transform.Find("leftSensor").gameObject.GetComponent<sensor>();
        right = transform.Find("rightSensor").gameObject.GetComponent<sensor>();
        currentState.Push(State.ForageFind);

}


    private void FixedUpdate()
    {
        switch (currentState.Peek())
        {
            case State.ForageFind:
                forage();
                break;
            case State.ForageNeutral:
                forageNeutral();
                break;
            case State.CircleStart:
                circleStart();
                break;
            case State.Circle:
                circle();
                break;
            default:
                break;
        }
        
    }

    private void layPheromone()
    {
        Instantiate(bob, transform.position, Quaternion.identity);
    }
    /*
     ############################################################### F o r a g e ###############################################################
     if the ant is in a foraging state then they will enact this algorithm
         */

    private void forage()
    {
        int l = left.getInside();
        int r = right.getInside();
        dir = leftOrRight(l,r);
        cert = l + r;
        if (cert == 0)
        {
            dir = (leftOrRight(left.getBobs(), right.getBobs()) * -1);
        }
        dir = dir * 5;
        Debug.Log(dir);
        moveSpeed = baseMoveSpeed / (cert * 2 + 1);
        turnSpeed = baseTurnSpeed / (cert + 1);
        turnAnt();
        moveAnt();
        timer = 3;
        currentState.Push(State.ForageNeutral);
        layPheromone();
    }

    private void forageNeutral()
    {
        
        if (timer == 0)
        {
            currentState.Pop();
        }
        
        moveSpeed = baseMoveSpeed / (cert * 2 + 1);
        turnSpeed = baseTurnSpeed / (cert + 1);
        turnAnt();
        moveAnt();
        timer--;
    }

    /*
     ############################################################### M o v e  A n t ###############################################################
     this function will move the ant forward
         */
    private void moveAnt()
    {
        m_Rigidbody.AddRelativeForce(transform.forward + Vector3.up * moveSpeed * Time.deltaTime*230);
    }

    /*
     ############################################################### T u r n  A n t ###############################################################
     this function will turn the ant clock wise if dir is positive and counter clockwise if dir is negative
         */
    private void turnAnt()
    {
        transform.Rotate(Vector3.forward * turnSpeed * dir * Time.deltaTime);
    }

    /*
     ############################################################### L e f t  O r  R i g h t ###############################################################
     this function will return the a positive number if the ant is to turn left or a negative number if the ant is to turn right. 
     This is based on what is inside there sensors
         */
    private float leftOrRight(int l, int r)
    {
        float x;

        if (l < r)
        {
            Debug.Log("right");
            x = -1;
        }
        else
        {
            Debug.Log("left");
            x = 1;
        }
        if (l == r)
            Debug.Log("eh");
        {
            x = Random.Range(-1f, 1f);
        }
            return x;
    }

    /*
    ############################################################### C i r c l e  S t a r t ###############################################################
    this will start the circle state
    */
    private void circleStart()
    {
        cert = 10;
        currentState.Pop();
        currentState.Push(State.Circle);
        dir = 0.5F;
    }

    /*
     ############################################################### C i r c l e ###############################################################
     this function will make an ant create a spiral around a point
     */
    private void circle()
    {
        if (cert > 1)
        {
            moveAnt();
            turnAnt();
            cert = cert * (float)0.99;
            moveSpeed = baseMoveSpeed;
            turnSpeed = baseTurnSpeed * ((cert + 1));
            layPheromone();
        }else
        {
            currentState.Pop();
        }
        
    }

    /*
     ############################################################### G o  T o  X ############################################################### TO DO
     this will send an ant to a set of coordinates
     */
}
