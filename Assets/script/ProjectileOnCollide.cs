using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileOnCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("A projectile hit a " + collision.collider.name);
        Destroy(gameObject);
    }
}
