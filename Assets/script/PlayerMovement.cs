using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;
    private Animator animator;

    private const int FWD_SPEED = 5;
    private const int BWD_SPEED = 3;
    private const int HORZ_SPEED = 3;

    private const int TURN_SPEED = 4;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        
        // Rotate the player based on mouse movement
        rb.transform.Rotate(0, Input.GetAxis("Mouse X") * Constants.TURN_SPEED * Time.deltaTime, 0);

        animate(horiz, vert);
    }
     
    private void animate(float horiz, float vert) {
        // Animator uses the exact same logic as below to pick animation direction
        animator.SetFloat("vert", vert);
        animator.SetFloat("horiz", horiz);

        // Vertical movement precedes horizontal - will add blending later?
        // forward 
        if(vert > 0.1) {
            move(rb.transform.forward, FWD_SPEED);
        }
        // backward
        else if(vert < -0.1) {
            move(-rb.transform.forward, BWD_SPEED);
        }
        // right
        else if(horiz > 0.1) {
            move(rb.transform.right, HORZ_SPEED);
        }
        // left
        else if(horiz < -0.1) {
            move(-rb.transform.right, HORZ_SPEED);
        }

        /*
        float sprint = isSprinting() ? 0.2f : 0.0f;

        animator.SetFloat("sprint", sprint);

        Debug.Log("turn" + horiz + " walk" + vert + " sprint" + sprint);
        Debug.Log(animator.gameObject.name);*/
    }

    private void move(Vector3 direction, int speed) {
        rb.MovePosition(rb.transform.position + (direction * Time.deltaTime * speed));
        //rb.velocity = direction * Time.deltaTime * speed;
        //rb.AddForce(direction * speed);
    }

    private bool isSprinting() {
        return Input.GetButton("Fire1");
    }
}