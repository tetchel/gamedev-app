using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private Animator animator;
    private Rigidbody player;

    // There was an issue where when the player released a key, 
    // and then immediately pressed the opposite direction key (eg right -> left)
    // 'moving' would be false for a few frames. By having a grace period 
    // where the character continues moving shortly after the button is released, this is worked around
    // however, it makes the input a little less responsive

    // below are in frames
    private const int GRACE_PERIOD = 3;
    private int counter = -1;

    //public float horiz, vert;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody>();
    }

    void LateUpdate() {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        animate(horiz, vert);
    }

    // Call this function providing the vert and horiz inputs from Input.GetAxis
    // to animate the player based on the direction inputs.
    public void animate(float horiz, float vert) {
        // The player can move only in one direction at a time: up, down, left, right with no blending.
        // Note that the order of the if statements is significant; vertical movement precedes horizontal

        // Play the running animation iff this is true
        bool moving = false;
        // Up
        if (vert > 0.1) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moving = true;
        }
        // Down
        else if (vert < -0.1) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            moving = true;
        }
        // Right
        else if (horiz > 0.1) {
            transform.eulerAngles = new Vector3(0, 90, 0);
            moving = true;
        }
        // Left
        else if (horiz < -0.1) {
            transform.eulerAngles = new Vector3(0, -90, 0);
            moving = true;
        }

        if (moving) {
            // counter = 0 means that the player WAS moving
            counter = 0;
        }
        else if(counter > -1) {
            if(counter < GRACE_PERIOD) {
                counter++;
                moving = true;
            }
            else {
                // counter = -1 means the grace period has expired
                counter = -1;
            }
        }

        animator.SetBool("moving", moving);

        /*
        float sprint = isSprinting() ? 0.2f : 0.0f;

        animator.SetFloat("sprint", sprint);

        Debug.Log("turn" + horiz + " walk" + vert + " sprint" + sprint);
        Debug.Log(animator.gameObject.name);*/
    }

    private bool isSprinting() {
        return Input.GetButton("Fire1");
    }
}