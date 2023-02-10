using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToPos : MonoBehaviour
{
    /*
     * For character to move to target position
     * target - target to move towards
     * lookTarget - target to rotate to
     * t - speed for move
     * s - speed for rotation
     */
    public GameObject target;
    public GameObject lookTarget;
    public LevelScroll levelRoot;
    public float t = 1;
    public float s = 1;
    public bool isControlable = true;

    // private variables for calculating move position
    private bool isCollidingObstacle;
    private float posx;
    private float posy;
    private GameObject collidingObject;
    private Vector3 targetPos;
    private float collisionPosition;
    private Rigidbody rb;
    private LevelScroll ls;
    private Vector3 collisionNormal;
    private Vector3 lookPos;

    private Vector3 addition = new(0,-2,0);

    private Vector3 subtraction = new(0, -1, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // find vector to face towards lookTarget
        if (transform.up.x > 0)
        {
            lookPos = (lookTarget.transform.position + addition) - transform.position;
        }
        else
        {
            lookPos = lookTarget.transform.position - transform.position;
        }
        /*
        if (lookPos.y < 0)
        {
            lookPos.x = -lookPos.x;
        }
        */
        // make rotation towards lookPos vector
        var rotation = Quaternion.LookRotation(lookPos.normalized, transform.up);
        // remove useless axises
        rotation.x = 0;
        rotation.y = 0;
        // limit rotation to 45 degrees
        /*
        if ((rotation.z < -0.35 && rotation.w > 0.35) || (rotation.w < -0.35 && rotation.z > 0.35))
        {
            print(rotation);
            rotation.z = Mathf.Abs(rotation.z);
            rotation.w = Mathf.Abs(rotation.w);
        }
        */
        //  check is colliding
        if (isCollidingObstacle)
        {
            // if colliding with obstacle calculate colliding side and lock movement for this side
            posx = target.transform.position.x;
            posy = target.transform.position.y;
            /*
             * if (collidingObject.transform.position.y + 3 > transform.position.y &&
                collidingObject.transform.position.y - 1 < transform.position.y &&
                (collidingObject.transform.position.x + 1.8 > transform.position.x &&
                collidingObject.transform.position.x - 1.8 < transform.position.x))
            
            if (collidingObject.transform.position.y - transform.position.y > 0 &&
                (collidingObject.transform.position.x + 1.8 > transform.position.x &&
                collidingObject.transform.position.x - 1.8 < transform.position.x))
            */
            if (collisionNormal.y < -0.2)
            {
                posy = collidingObject.transform.position.y + (transform.position.y - collidingObject.transform.position.y);//transform.position.y;
            }
            else if (collidingObject.transform.position.x + 3 > transform.position.x &&
                collidingObject.transform.position.x - 3 < transform.position.x)
            {
                posx = transform.position.x;
            }
            // create vector for movement
            targetPos = new Vector3(posx, 
                posy, 
                target.transform.position.z);
        }
        // if not colliding assign target's position to vector for movement
        else
        {
            targetPos = target.transform.position;
        }
        // lerp for smooth movement
        if (isControlable)
        {
            // lerp to get smooth rotation
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation.normalized, s));
            //transform.LookAt(target.transform.position, -transform.forward);

            rb.MovePosition(Vector3.MoveTowards(transform.position, Vector3.Lerp(transform.position, targetPos, t * Time.deltaTime), t));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if colliding with obstacle turn on isCollidingObstacle and assign colliding gameObject to variable
        if (collision.gameObject.CompareTag("obstacle"))
        {
            isCollidingObstacle = true;
            collidingObject = collision.gameObject;
            collisionNormal = collision.GetContact(0).normal;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if exit colliding with obstacle turn off isCollidingObstacle
        if (collision.gameObject.CompareTag("obstacle"))
        {
            isCollidingObstacle = false;
        }
    }
}
