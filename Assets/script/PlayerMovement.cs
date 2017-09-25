using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody rigidBody;

    public float speed;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called before each frame
	void Update () {
		
	}

    // FixedUpdate should be used instead of Update when dealing with Rigidbody
    void FixedUpdate() {
        // should these be called here or in Update() ?
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // Determine player heading based on inputs
        Vector3 vel = new Vector3(horiz, 0, vert);
        rigidBody.velocity = vel * speed;
    }
}
