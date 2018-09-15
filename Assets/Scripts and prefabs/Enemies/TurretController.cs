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
        ship = GameObject.FindWithTag("ShipTarget");
        remainingCooldownTime = cooldownTime;
    }
	
	// Update is called once per frame
	void Update () {
        // Raycast to object
        // Determine distance between objects

        float distance = Vector3.Distance(transform.position, ship.transform.position);
        if(distance < range)
        {
            transform.LookAt(ship.transform);

            RaycastHit hit;
            Vector3 rayDirection = ship.transform.position - bulletSpawn.transform.position;
            int layerMask = 1 << 9;
            layerMask = ~layerMask;

            if (Physics.Raycast(bulletSpawn.transform.position, rayDirection, out hit, range, layerMask))
            {
                Debug.DrawRay(bulletSpawn.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Shoot();
                }
            }
        }
        remainingCooldownTime -= Time.fixedDeltaTime;
	}

    void Shoot()
    {
        if (remainingCooldownTime <= 0f)
        {
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, bulletLifespan);
            remainingCooldownTime = cooldownTime;
        }
    }
}
