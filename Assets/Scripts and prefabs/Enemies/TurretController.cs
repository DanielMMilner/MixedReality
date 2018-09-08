using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController : MonoBehaviour {

    private GameObject ship;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 5.0f;
    public float bulletLifespan = 5.0f;

    public float range = 100;

    // Time in seconds
    public float cooldownTime = 1f;
    private float remainingCooldownTime = 0f;

    // Use this for initialization
    void Start () {
        ship = GameObject.FindWithTag("PlayerShip");
        remainingCooldownTime = cooldownTime;
    }
	
	// Update is called once per frame
	void Update () {
        // Raycast to object
        // Determine distance between objects
        float distance = Vector3.Distance(transform.position, ship.transform.position);
        if(distance < range)
        {
            Debug.DrawLine(transform.position, ship.transform.position, Color.red);
            transform.LookAt(ship.transform);

            if (remainingCooldownTime <= 0f)
            {
                var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
                
                Destroy(bullet, bulletLifespan);
                remainingCooldownTime = cooldownTime;
            }
        }
        else
        {
            Debug.DrawLine(transform.position, ship.transform.position, Color.green);
        }
        remainingCooldownTime -= Time.fixedDeltaTime;
	}
}
