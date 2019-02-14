using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class LookScript : NetworkBehaviour {

    public RotationalAxis axis = RotationalAxis.MouseX;

    // Public:
    // Sensitivity of the mouse
    public float mouseSensitivity = 2.0f;
    // Minimum & Maximum Y axis (degrees)
    public float minimumY = -90f;
    public float maximumY = 90f;

    // Private:
    // Yaw of the camera (Rotation on Y)
    private float yaw = 0f;
    // Pitch of the camera (Rotation on X)
    private float pitch = 0f;

    // Main camera reference
    private GameObject mainCamera;

	// Use this for initialization
	void Start () {
        // Lock the mouse
        Cursor.lockState = CursorLockMode.Locked;
        // Make cursor invisible
        Cursor.visible = false;
        // Gets reference to the camera inside of this gameobject
        Camera cam = GetComponentInChildren<Camera>();
        if (cam != null)
        {
            mainCamera = cam.gameObject;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isLocalPlayer)
        {
            // Update input for local player only
            HandleInput();
        }
	}

    // Remember to use LateUpdate for when you move the camera
    void LateUpdate()
    {
        // Check if this is the local player
        if(isLocalPlayer)
        {
            // Rotate the camera up or down using pitch
            mainCamera.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }

    // Gets called upon GameObject being destroyed
    void OnDestroy()
    {
        // Release the cursor
        Cursor.lockState = CursorLockMode.None;
        // Make cursor visible
        Cursor.visible = true;
    }

    void HandleInput()
    {
        if (axis == RotationalAxis.MouseXandY)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-pitch, yaw, 0);
        }
        else if (axis == RotationalAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), mouseSensitivity);
        }

        else
        {
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }
    }
}

public enum RotationalAxis
{
    MouseXandY,
    MouseX,
    MouseY
}