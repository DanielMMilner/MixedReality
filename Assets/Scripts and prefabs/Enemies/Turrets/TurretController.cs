using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController : MonoBehaviour {

    private GameObject ship;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform gunTransform;
    public float bulletSpeed = 5.0f;
    public float bulletLifespan = 5.0f;
    public float pooledBullets = 5;
    public float range = 100;

    // Time in seconds
    public float cooldownTime = 1f;
    private Queue<GameObject> bullets;
    private float remainingCooldownTime = 0f;
    private Animator animator;

    private bool isFiring = false;


    // Use this for initialization
    void Start () {
        ship = GameObject.FindWithTag("ShipTarget");
        remainingCooldownTime = cooldownTime;
        animator = GetComponentInParent<Animator>();

        bullets = new Queue<GameObject>();
        for(int i = 0; i < pooledBullets; i++)
        {
            GameObject bullet = Instantiate (bulletPrefab) as GameObject;
            bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<EnemyBulletController>().Reset(bulletLifespan);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Raycast to object
        // Determine distance between objects

        float distance = Vector3.Distance(transform.position, ship.transform.position);
        if(distance < range)
        {
            gunTransform.LookAt(ship.transform);

            RaycastHit hit;
            Vector3 rayDirection = ship.transform.position - bulletSpawn.transform.position;
            int layerMask = 1 << 9;
            int layerMask2 = 1 << 2;
            layerMask = ~(layerMask | layerMask2);

            if (Physics.Raycast(bulletSpawn.transform.position, rayDirection, out hit, range, layerMask))
            {
                Debug.DrawRay(bulletSpawn.transform.position, gunTransform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    if(!isFiring)
                        animator.SetTrigger("Activate");

                    Shoot();
                }
                else
                {
                    isFiring = false;
                    animator.SetTrigger("Deactivate");
                }
            }
        }
        remainingCooldownTime -= Time.fixedDeltaTime;
	}

    void Shoot()
    {
        if (remainingCooldownTime <= 0f)
        {
            GameObject bullet = bullets.Dequeue();

            if (!bullet.activeSelf)
            {
                bullet.transform.position = bulletSpawn.transform.position;
                bullet.transform.rotation = gunTransform.rotation;
                bullet.GetComponent<EnemyBulletController>().Reset(bulletLifespan);
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            }

            bullets.Enqueue(bullet);
            remainingCooldownTime = cooldownTime;
        }
    }

    public void Destroyed()
    {
        foreach (GameObject bullet in bullets)
        {
            if (bullet.activeSelf)
            {
                bullet.transform.parent = null;
                Destroy(bullet, bulletLifespan);
            }
        }
        Destroy(gameObject);
    }
}
