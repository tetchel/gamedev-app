using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public Camera playerCam;

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
        playerAim = player.transform.position + (player.transform.forward * 100);
        Debug.DrawLine(Vector3.zero, playerAim);
        if(Input.GetButton("Fire1")) {
            Debug.DrawRay(player.transform.position, player.transform.forward * 100, Color.black, 1, true);
        }
	}
    
    // GUI update is performed AFTER Update and LateUpdate
    void OnGUI() {
        //if not paused
        if (Time.timeScale != 0) {
            Vector2 crosshairLoc = playerCam.WorldToScreenPoint(playerAim);
            crosshairLoc.x -= crosshairSize.x/2;
            crosshairLoc.y -= crosshairSize.y/2;

            GUI.DrawTexture(new Rect(crosshairLoc, crosshairSize), crosshairTexture);
        }
    }
}
