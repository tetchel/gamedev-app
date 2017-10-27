using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonShooterCam : MonoBehaviour {
    
    public Transform player;

    // Should be removed once can rotate in Y
    public float aimHeight = 2f;
    // How high the rays leave the player from
    public float gunHeight = 0.8f;

    private Camera cam;

    // Difference between player location and camera location
    public Vector3 camToPlayerOffset;

    // Where player will shoot at
    private Vector3 playerAim;
    
    // In degrees, this determines how far to the right of the player the crosshair appears
    public float crosshairOffsetH = 2f;
    // How far above the player the crosshair appears
    public float crosshairOffsetV = 2f;

    public Texture2D crosshairTexture;
    private Vector2 crosshairSize;

    public Rigidbody projectile;
    // should add a Texture for the projectile

    // in millis
    public float reloadTime = 500;

    private float reloadCounter = 0;

    public int projectileSpeed = 20;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
    
        crosshairSize = new Vector2(crosshairTexture.width, crosshairTexture.height);

        // Since we're only using the mouse to rotate, we don't want a cursor, 
        // and we want to confine the cursor to the window
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate () {
        cameraLookAtPlayer();
        aimAndFire();
    }

    // Move the camera based on mouse movement, then look at the player
    void cameraLookAtPlayer() {
        // The mouseX scalar must (?) match the player's turn speed 
        camToPlayerOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Constants.TURN_SPEED * Time.deltaTime, Vector3.up) * camToPlayerOffset;
        //camToPlayerOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Constants.TURN_SPEED * Time.deltaTime, Vector3.right) * camToPlayerOffset;

        transform.position = player.transform.position + camToPlayerOffset;

        Vector3 cameraLook = player.transform.position;
        cameraLook.y += aimHeight;
        transform.LookAt(cameraLook);
    }

    void aimAndFire() {
        playerAim = cam.transform.position + (cam.transform.forward * 100);
        playerAim = Quaternion.AngleAxis(crosshairOffsetH, Vector3.up) * playerAim;

        //Debug.Log("After: " + playerAim);
        //playerAim = Quaternion.AngleAxis(crosshairOffsetV, Vector3.right) * playerAim;

        if (reloadCounter < reloadTime) {
            reloadCounter += Time.deltaTime * 1000;
        }

        //Debug.DrawRay(Vector3.zero, playerAim);
        if(Input.GetButton("Fire1") && reloadCounter > reloadTime) {
            reloadCounter = 0;

            Vector3 gunPos = player.transform.position;
            gunPos.y += gunHeight;
            Debug.DrawRay(gunPos, playerAim, Color.black, 1, true);
            Debug.Log(gunPos + " is gunpos");
            Debug.Log(playerAim + " is aim");

            //Debug.Log("Player at " + player.transform.position + " aiming at " + playerAim);

            // The projectile has to spawn outside the player's hitbox so it doesn't get stuck
            Rigidbody projectileInstance = Instantiate(projectile, gunPos + (0.5f * playerAim.normalized), Quaternion.identity);
            projectileInstance.AddForce(playerAim.normalized * projectileSpeed);
        }
    }

    void OnGUI() {
        // Draw crosshair
        if (Time.timeScale != 0) {
            Vector2 crosshairLoc = cam.WorldToScreenPoint(playerAim);
            crosshairLoc.x -= crosshairSize.x/2;
            crosshairLoc.y -= crosshairSize.y/2;

            GUI.DrawTexture(new Rect(crosshairLoc, crosshairSize), crosshairTexture);
        }
    }
}