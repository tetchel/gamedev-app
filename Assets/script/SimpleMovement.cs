using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        // Vertical movement precedes horizontal - will add blending later?
        // forward 
        if(vert > 0.1) {
            move(rb.transform.forward);
        }
        // backward
        else if(vert < -0.1) {
            move(-rb.transform.forward);
        }
        // right
        else if(horiz > 0.1) {
            move(rb.transform.right);
        }
        // left
        else if(horiz < -0.1) {
            move(-rb.transform.right);
        }

        // "gravity"
        if(rb.transform.position.y > 0) {
            rb.MovePosition(rb.transform.position + new Vector3(0, -1 * Time.deltaTime));
        }
    }

    private void move(Vector3 direction) {
        rb.MovePosition(rb.transform.position + (direction * Time.deltaTime * speed));
    }
}