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
    private Rigidbody rb;

    public float pooledBullets = 10;
    private Queue<GameObject> bullets;
    private bool isAlive = true;
    public float corpseLifespan = 45.0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rb = GetComponent<Rigidbody>();
        rb.Sleep();

        bullets = new Queue<GameObject>();
        for (int i = 0; i < pooledBullets; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<DroneBulletController>().Reset(bulletLifespan);
            bullet.GetComponent<DroneBulletController>().SetParentDrone(this);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }

    void FixedUpdate () {
        if (isAlive)
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

        GameObject bullet = bullets.Dequeue();

        if (!bullet.activeSelf)
        {
            bullet.transform.position = bulletSpawn.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<DroneBulletController>().Reset(bulletLifespan);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        }

        _bulletCooldown = bulletCooldown;
    }

    public void QueueBullet(GameObject bullet)
    {
        if (isAlive)
        {
            bullets.Enqueue(bullet);
        }
        else
        {
            Destroy(bullet);
        }
    }

    public void Died(Vector3 explosionPosition)
    {
        isAlive = false;
        rb.useGravity = true;
        rb.AddExplosionForce(50f, explosionPosition, 10f);

        foreach (GameObject x in bullets)
        {
            Destroy(x);
        }
    }
}
