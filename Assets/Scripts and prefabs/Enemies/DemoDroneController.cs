using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A pseudo state machine so the drone responds to the player */

public class DemoDroneController : MonoBehaviour {

    [Header("Drone Movement Settings")]
    private Transform player;                
    public float aggroRange = 5f;           // The minimum range for it to respond to the player
    public float stoppingRange = 2f;        // How close to get to the player
    private bool _aggro = false;            // Internally track state
    public float flightSpeed = 0.01f;       // How far to move in timesteps.

    [Header("Drone Combat Settings")]
    public GameObject bulletPrefab;         // Bullet to spawn
    public Transform bulletSpawn;           // Child gameobject on drone prefab
    public float bulletSpeed = 5.0f;        // Flight speed
    public float bulletLifespan = 5.0f;     // Maximum lifespan, if nothing hit by then.
    public float bulletCooldown = 0.5f;     // Time between attacks
    private float _bulletCooldown = 0f;     // Internal countdown of bulletCooldown

    private bool Alive = true;
    public float corpseLifespan = 45.0f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void FixedUpdate () {
        if (Alive)
        {
            Fly();
        }else if(corpseLifespan > 0)
        {
            corpseLifespan -= Time.deltaTime;
            if(corpseLifespan < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Fly()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If you aren't already aggro'd but are now in range.
        if (!_aggro && distanceToPlayer <= aggroRange)
        {
            _aggro = true;
        } else if (distanceToPlayer > aggroRange)
        {
            _aggro = false;
        }

        // If not aggrod, do nothing
        if (!_aggro)
        {
            return;
        }

        transform.LookAt(player); // TODO: Replace with a smooth rotation

        // Move to within range of the player, and start firing
        if (distanceToPlayer > stoppingRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, flightSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -flightSpeed);
        }

        Fire();
    }

    void Fire() {

        // Rate limiting the drone firing
        if (_bulletCooldown > 0) {
            _bulletCooldown -= Time.fixedDeltaTime;
            return;
        }

        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet if it timeouts, but it should hit something before then (ground/player)
        Destroy(bullet, bulletLifespan);
        _bulletCooldown = bulletCooldown;
    }

    public void Died(Vector3 explosionPosition)
    {
        Alive = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddExplosionForce(50f, explosionPosition, 10f);
        rb.useGravity = true;
    }
}
