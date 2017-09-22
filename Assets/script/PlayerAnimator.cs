using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerAnimator
{

    public class PlayerAnimator : MonoBehaviour
    {

        private Animator animator;

        float horiz, vert;

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            horiz = Input.GetAxis("Horizontal");
            vert = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            animate(horiz, vert);
        }

        // Call this function providing the vert and horiz inputs from Input.GetAxis
        // to animate the player based on the direction it is running.
        public void animate(float horiz, float vert)
        {
            animator.SetFloat("turn", horiz);
            animator.SetFloat("walk", vert);

            float sprint = isSprinting() ? 0.2f : 0.0f;

            animator.SetFloat("sprint", sprint);

            Debug.Log("turn" + horiz + " walk" + vert + " sprint" + sprint);
            Debug.Log(animator.gameObject.name);
        }

        private bool isSprinting()
        {
            return Input.GetButton("Fire1");
        }
    }

}