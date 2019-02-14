using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ShootScript : NetworkBehaviour
{
    // Public:
    // Amount of bullets that can be fired per second.
    public float fireRate = 1f;
    // Range that the bullet can travel
    public float range = 100f;
    // LayerMask of which layer to hit
    public LayerMask mask;

    // Private:
    // Timer for the fireRate
    private float fireFactor = 0f;
    // Reference to the camera child
    private GameObject mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command] void Cmd_PlayerShot(string id)
    {
        Debug.Log("Player" + id + "has been shot!")
    }

    [Client] void Shoot()
    {
        Ray ray = new Ray(mainCamera.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, mask))
        {

        }
    }
}
