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
    private int baseTurnSpeed = 150;                                                               //This is the base turn speed of the ant
    private float dir;
    private float cert;                                                                              //This is to represent the ants certanty on where its going
    private float tempCert;                                                                         //This is to save the previous states certenty in the case of a change in state mid way
    private int bobs = 0;
    private Stack commandStack;                                                                     //This will be an enum storing the current event state
    private int timer;
    private Dictionary<string, ArrayList> methods;
    private Stack whileStack;
    enum Command
    {
        Neutral,
        ForageFind,
        ForageNeutral,
        FoundFood,
        CircleStart,
        Circle,
        GoTo,
        TurnAroundStart,
        TurnAround,
        FaceX
    }
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        commandStack = new Stack();
        whileStack = new Stack();
        moveSpeed = baseMoveSpeed;
        turnSpeed = baseTurnSpeed;
        left = transform.Find("leftSensor").gameObject.GetComponent<sensor>();
        right = transform.Find("rightSensor").gameObject.GetComponent<sensor>();
        //currentState.Push(State.ForageFind);
        //currentState.Push(State.CircleStart);
        commandStack.Push(Command.Neutral);

        commandStack.Push(Command.FaceX);
        
}


    private void FixedUpdate()
    {
        
        switch (commandStack.Peek())
        {
            case Command.ForageFind:
                forage();
                break;
            case Command.ForageNeutral:
                forageNeutral();
                break;
            case Command.CircleStart:
                circleStart();
                break;
            case Command.Circle:
                circle();
                break;
            case Command.TurnAroundStart:
                turnAroundStart();
                break;
            case Command.TurnAround:
                turnAround();
                break;
            case Command.FaceX:
                faceX();
                break;
            default:
                //methods.GetEnumerator(commendStack.peek());
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
      ToDo::: make the ants go in more straight lines this will be easyer for path finding
         */

    private void forage()
    {
        int l = left.getInside();
        int r = right.getInside();
        dir = forageLeftOrRight(l,r);
        cert = l + r;
        if (cert == 0)
        {
            dir = (forageLeftOrRight(left.getBobs(), right.getBobs()) * -1);
            timer = 1;
            commandStack.Push(Command.ForageNeutral);
        }
        dir = dir * 5;
        moveSpeed = baseMoveSpeed / (cert * 2 + 1);
        turnSpeed = baseTurnSpeed / (cert + 1);
        turnAnt();
        moveAnt();
        
        layPheromone();
    }

    private void forageNeutral()
    {
        
        if (timer == 0)
        {
            commandStack.Pop();
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
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddRelativeForce(transform.forward + Vector3.up * moveSpeed * Time.deltaTime*2000);
    }

    /*
     ############################################################### T u r n  A n t ###############################################################
     this function will turn the ant clock wise if dir is positive and counter clockwise if dir is negative
         */
    private void turnAnt()
    {
        m_Rigidbody.angularVelocity = 0;
        //Debug.Log(turnSpeed * dir * 0.05f);
        m_Rigidbody.AddTorque(turnSpeed * dir * 0.05f);
    }

    private void stopAnt()
    {
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.angularVelocity = 0;
    }
    /*
     ############################################################### F o r a g e L e f t  O r  R i g h t ###############################################################
     this function will return the a positive number if the ant is to turn left or a negative number if the ant is to turn right. 
     This is based on what is inside there sensors
         */
    private float forageLeftOrRight(int l, int r)
    {
        float x;

        if (l < r)
        {
            //Debug.Log("right");
            x = -1;
        }
        else
        {
            //Debug.Log("left");
            x = 1;
        }
        if (l == r)
            //Debug.Log("eh");
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
        commandStack.Pop();
        commandStack.Push(Command.Circle);
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
            commandStack.Pop();
        }
        
    }


    /*
     ############################################################### T u r n  A r o u n d  S t a r t ############################################################### 
     */
     private void turnAroundStart()
    {
        
        if (cert == -1)
        {
            cert = 0;
            stopAnt();
            commandStack.Pop();
        }
        else
        {
            turnSpeed = baseTurnSpeed;
            dir = 4;
            cert = 16;
            commandStack.Push(Command.TurnAround);
        }
        
    }

    /*
    ############################################################### T u r n  A r o u n d ###############################################################
     */

    private void turnAround()
    {
        turnAnt();
        cert--;
        Debug.Log("turn");
        if (cert == 0) {
            cert--;
            commandStack.Pop();
        }
    }

    /*
     ############################################################### G o  T o  X ############################################################### TO DO
     this will send an ant to a set of coordinates
     */



    /*
    ############################################################### F a c e  X ############################################################### 
    void faceX()
    {
        Vector3 targetPosition =  Vector3.zero;
        targetPosition.x = targetPosition.x - transform.position.x;
        targetPosition.y = targetPosition.y - transform.position.y;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;
        float anglefin = angle - (transform.eulerAngles.z);
        Debug.Log(anglefin);
        if (anglefin < 0)
        {
            anglefin = 360 + anglefin;
        }
        dir = -1;
        if (anglefin > 90 && anglefin < 270)
        {
            dir = 1;
        }
        turnAnt();
    }
    */

    /*
         *  ############################################################### G e t  A n g l e  T o  P o s ###############################################################
         *  this will return the angle between the ant and the target
    */
    private float getAngleToPos()
    {
        
        Vector3 targetPos = Vector3.zero;
        Vector3 direction = targetPos - transform.position;
        float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float lookerAngle = transform.eulerAngles.z;
        float checkAngle = 0f; 
        if (ang >= 0f)
            checkAngle = ang - lookerAngle - 90f;
        else if (ang < 0f)
            checkAngle = ang - lookerAngle + 270f;
        return checkAngle;
    }

/*
    /*
     ############################################################### F a c e  X ############################################################### TO DO
     this will send an ant to a set of coordinates
    */
    private void faceX()//https://answers.unity.com/questions/503934/chow-to-check-if-an-object-is-facing-another.html
    {
        bool facing = false;
        float FOVAngle = 10;
        float checkAngle = getAngleToPos();
        if (checkAngle < -180f)
            checkAngle = checkAngle + 360f;

        if (checkAngle <= FOVAngle * .4f)
        {
            dir = -2;
        }
        else if (checkAngle >= -FOVAngle * .4f)
        {
            dir = 2;
        }
        else if (checkAngle >= FOVAngle* 10 && checkAngle <= -FOVAngle * 10)
        {
            facing = true;
            commandStack.Pop();
        }

            turnAnt();

    }

    /*
     *  ############################################################### W h i l e  S t a r t ###############################################################
     * /

    /*
     *  ############################################################### H a n d l e  B o b I n ###############################################################
     *  this will handle if a Bob enters the fov
     */






}
