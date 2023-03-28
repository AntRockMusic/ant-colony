using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * TODO::::::::                             We are trying to make the ant react to food and bring it home,
 *                                          First: get the Ant to react when they collid with food stuffs
 *                                       
 * 
 */





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
    private Stack currentState;                                                                     //This will be an enum storing the current event state
    private int timer;
    private float prevDir;
    private float facing = 0;
    private Vector3 targetPos;
    private int food = 0;
    private bool atHome;
    private Vector2 homeLocation;
    private float estimate = 0;
    private Vector3 targetPosition;
    enum State
    {
        None,
        Neutral,
        ForageFind,
        ForageNeutral,
        FoundFood,
        CircleStart,
        Circle,
        GoTo,
        TurnAroundStart,
        TurnAround,
        FaceX,
        MoveToX,
    }
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        currentState = new Stack();
        moveSpeed = baseMoveSpeed;
        turnSpeed = baseTurnSpeed;
        left = transform.Find("leftSensor").gameObject.GetComponent<sensor>();
        right = transform.Find("rightSensor").gameObject.GetComponent<sensor>();
        //currentState.Push(State.CircleStart);
        currentState.Push(State.Neutral);
        currentState.Push(State.ForageFind);
        //currentState.Push(State.GoTo);

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
            case State.TurnAroundStart:
                turnAroundStart();
                break;
            case State.TurnAround:
                turnAround();
                break;
            case State.GoTo:
                GoToX();
                break;
            case State.FaceX:
                faceX();
                break;
            case State.MoveToX:
                moveToX();
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
        dir = leftOrRight(l, r);
        cert = l + r;
        if (cert == 0)
        {
            dir = (leftOrRight(left.getBobs(), right.getBobs()) * -1);
            timer = 1;
            currentState.Push(State.ForageNeutral);
        }
        dir = dir * 5;
        moveSpeed = baseMoveSpeed / (cert * 2 + 1);
        turnSpeed = baseTurnSpeed / (cert + 1);
        turnAnt();
        moveAnt();

        layPheromone();
    }


    /*
    ############################################################### F o r a g e  N e u t r a l ###############################################################
    this is the neutral foraging algorithm
    */

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

    private void setDir(float newDir)
    {
        prevDir = dir;
        dir = newDir;

    }


    /*
     ############################################################### M o v e  A n t ###############################################################
     this function will move the ant forward
    */

    private void moveAnt()
    {
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.AddRelativeForce(transform.forward + Vector3.up * moveSpeed * Time.deltaTime * 2000);
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

    /*
    ############################################################### S t o p  A n t ###############################################################
    this will stop the ant from turning and moving
    mostly this is here so i don't have to deal with velocity from unity
     */
    private void stopAnt()
    {
        m_Rigidbody.velocity = Vector2.zero;
        m_Rigidbody.angularVelocity = 0;
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
        }
        else
        {
            currentState.Pop();
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
            currentState.Pop();
        }
        else
        {
            turnSpeed = baseTurnSpeed;
            dir = 4;
            cert = 16;
            currentState.Push(State.TurnAround);
        }

    }

    /*
    ############################################################### T u r n  A r o u n d ###############################################################
     */

    private void turnAround()
    {
        turnAnt();
        cert--;
        //Debug.Log("turn");
        if (cert == 0)
        {
            cert--;
            currentState.Pop();
        }
    }

    /*
     ############################################################### G o  T o  X ############################################################### TO DO
     this will send an ant to a set of coordinates
     */
    private void GoToX()
    {
        //getDistance
        atHome = false;
        moveSpeed = baseMoveSpeed;
        turnSpeed = baseTurnSpeed;
        currentState.Pop();
        currentState.Push(State.MoveToX);//Push Move X
        currentState.Push(State.FaceX);//Push Face X
    }

    /*
     ############################################################### M o v e  T o  X ############################################################### TO DO
     this will send an ant to a set of coordinates
     */
    private void moveToX()
    {
        //Vector2 tempPos = transform.position;
        if ((Mathf.Abs(transform.position.x - targetPos.x) > 2) || (Mathf.Abs(transform.position.y - targetPos.y) > 2))
        {
            moveAnt();
          //  tempPos = tempPos - transform.position;

        }
        else
        {
            currentState.Pop();
        }
    }


    /*
    private float getDistance(Vector2 triangle)
    {
        return sqrt(triangle.x * *2 + triangle.y * *2);
    }
    */

    /*
     ############################################################### F a c e  X ###############################################################
     this will send an ant to a set of coordinates
    */
    private void faceX()//https://answers.unity.com/questions/503934/chow-to-check-if-an-object-is-facing-another.html
    {
        float FOVAngle = 10;
        Vector3 direction = targetPos - transform.position;
        float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float lookerAngle = transform.eulerAngles.z;
        float checkAngle = 0f;

        if (facing == 0)
        {
            facing = -2;
        }
        //============================================Checking if the position is on the left or the right =============================================
        if (ang >= 0f)
        {
            checkAngle = ang - lookerAngle - 90f;
        }
        else if (ang < 0f)
        {
            checkAngle = ang - lookerAngle + 270f;
        }

        if (checkAngle < -180f)
        {
            checkAngle = checkAngle + 360f;
        }

        if (checkAngle <= FOVAngle * .4f)
        {
            setDir(-2);

        }
        else if (checkAngle >= -FOVAngle * .4f)
        {
            setDir(2);
        }
        //=========================================================================================


        //============================================ Checking to see if the ant is going back and fourth =============================================
        if (dir != prevDir)
        {
            facing++;              //The direction has to change twice for the algorithm to terminate
            if (facing == 0)
            {
                stopAnt();
                currentState.Pop();
            }
            Debug.Log(facing);
        }
        else
        {
            //This covors for the start when they don't equil;
            facing = -2;
        }
        if (facing != 0)
        {
            turnAnt();
        }
        //=========================================================================================

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (currentState.Peek().Equals(State.ForageFind) || currentState.Peek().Equals(State.ForageNeutral))
        {
            if (collision.gameObject.tag == "bit")
            {
                foodScript bitObj = collision.gameObject.GetComponent<foodScript>();
                Debug.Log("oooooh yummers");
                bitObj.eat();
                //currentState.Pop();
                currentState.Push(State.GoTo);
            }
        }
       

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision!!!");
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "home")
        {
            atHome = true;
            Debug.Log("Honey Im Hooooome");
            homeLocation = collision.gameObject.transform.position;
            targetPos = homeLocation;
        }
    }

        void OnCollisionExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "home")
            {
                atHome = false;
            }
        }
    }




