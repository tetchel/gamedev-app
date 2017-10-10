using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThirdPersonCamera : MonoBehaviour {
    
    public GameObject target;

    private Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.transform.position;

        // Since we're only using the mouse to rotate, we don't want a cursor, 
        // and we want to confine the cursor to the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // LateUpdate is called once per frame after Update() is complete - set camera position AFTER target has moved
    void LateUpdate () {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        // Move the camera based on mouse movement, then look at the player
        // The mouseX scalar must (?) match the player's turn speed 
        offset = Quaternion.AngleAxis(mouseX * 4, Vector3.up) * offset;
        transform.position = target.transform.position + offset;

        Vector3 cameraLook = target.transform.position;
        cameraLook.y = cameraLook.y + 2;
        transform.LookAt(cameraLook);
    }

}