using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThirdPersonCamera : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame after Update() is complete - set camera position AFTER player has moved
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        //Debug.Log("Player " + player.transform.position + " Transform " + )
	}
}
