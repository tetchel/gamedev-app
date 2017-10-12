using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim  : MonoBehaviour {

    public Camera playerCam;

    // In degrees, this determines how far to the right of the player the crosshair appears
    public float crosshairOffsetH = 7f;
    // How far above the player the crosshair appears
    //public float crosshairOffsetV = 7f;

    private Transform player;

    public Texture2D crosshairTexture;
    private Vector2 crosshairSize;

    private Vector3 playerAim;

	// Use this for initialization
	void Start () {
        player = GetComponent<Transform>();
        crosshairSize = new Vector2(crosshairTexture.width, crosshairTexture.height);
	}
	
	void LateUpdate () {
        playerAim = playerCam.transform.position + (playerCam.transform.forward * 100);
        playerAim = Quaternion.AngleAxis(crosshairOffsetH, Vector3.up) * playerAim;
        //playerAim = Quaternion.AngleAxis(crosshairOffsetV, Vector3.right) * playerAim;

        //Debug.DrawRay(Vector3.zero, playerAim);
        if(Input.GetButton("Fire1")) {
            Debug.DrawRay(player.transform.position, playerAim, Color.black, 1, true);

            RaycastHit hit;
            if(Physics.Raycast(player.transform.position, playerAim, out hit)) {
                Debug.Log("You shot " + hit.collider.name);
            }
            else {
                Debug.Log("Somehow, you managed to hit nothing");
            }
        }
	}
    
    void OnGUI() {
        // Draw crosshair
        if (Time.timeScale != 0) {
            Vector2 crosshairLoc = playerCam.WorldToScreenPoint(playerAim);
            crosshairLoc.x -= crosshairSize.x/2;
            crosshairLoc.y -= crosshairSize.y/2;

            GUI.DrawTexture(new Rect(crosshairLoc, crosshairSize), crosshairTexture);
        }
    }
}
