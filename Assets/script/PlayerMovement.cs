using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    private Animator animator;
    private Transform playerTransform;
    private CharacterController controller;

    private const int FWD_SPEED = 5;
    private const int BWD_SPEED = 3;
    private const int HORZ_SPEED = 2;

    private const int TURN_SPEED = 4;

    private const int GRAVITY = 30;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
    }

    void LateUpdate() {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        
        // Rotate the player based on mouse movement
        playerTransform.Rotate(0, Input.GetAxis("Mouse X") * Constants.TURN_SPEED * Time.deltaTime, 0);

        animate(horiz, vert);
    }
     
    private void animate(float horiz, float vert) {
        // Animator uses the exact same logic as below to pick animation direction

        // Vertical movement precedes horizontal
        // For now if player holds l/r then u/d it will result in weirdness - decide how to handle blending
        animator.SetFloat("vert", vert);
        if(vert != 0) {
            horiz = 0;
        }
        animator.SetFloat("horiz", horiz);

        Vector3 inputDir = Vector3.zero;
        int speed = 0;
        // forward 
        if(vert > 0.1) {
            inputDir = playerTransform.forward;
            speed = FWD_SPEED;
        }
        // backward
        else if(vert < -0.1) {
            inputDir = -playerTransform.forward;
            speed = BWD_SPEED;
        }
        // right
        else if(horiz > 0.1) {
            inputDir = playerTransform.right;
            speed = HORZ_SPEED;
        }
        // left
        else if(horiz < -0.1) {
            inputDir = -playerTransform.right;
            speed = HORZ_SPEED;
        }
        
        // Always move - This will apply gravity even if there is no input.
        move(inputDir, speed);

        /*
        float sprint = isSprinting() ? 0.2f : 0.0f;

        animator.SetFloat("sprint", sprint);

        Debug.Log("turn" + horiz + " walk" + vert + " sprint" + sprint);
        Debug.Log(animator.gameObject.name);*/
    }

    // Apply gravity and move the player in the given direction at the given speed
    private void move(Vector3 direction, int speed) {
        // "gravity" - looks really bad
        direction.y -= GRAVITY * Time.deltaTime;

        direction.x *= speed;
        direction.z *= speed;
        controller.Move(direction * Time.deltaTime);
    }

    private bool isSprinting() {
        return false;
    }
}