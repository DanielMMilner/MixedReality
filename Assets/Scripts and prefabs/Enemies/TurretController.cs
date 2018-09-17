﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController : MonoBehaviour {

    private GameObject ship;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 5.0f;
    public float bulletLifespan = 5.0f;
    public float pooledBullets = 5;
    public float range = 100;

    // Time in seconds
    public float cooldownTime = 1f;
    private Queue<GameObject> bullets;
    private float remainingCooldownTime = 0f;

    private bool isAlive = true;

    // Use this for initialization
    void Start () {
        ship = GameObject.FindWithTag("ShipTarget");
        remainingCooldownTime = cooldownTime;

        bullets = new Queue<GameObject>();
        for(int i = 0; i < pooledBullets; i++)
        {
            GameObject bullet = Instantiate (bulletPrefab) as GameObject;
            bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<TurretBulletController>().Reset(bulletLifespan);
            bullet.GetComponent<TurretBulletController>().SetParentTurret(this);
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
            transform.LookAt(ship.transform);

            RaycastHit hit;
            Vector3 rayDirection = ship.transform.position - bulletSpawn.transform.position;
            int layerMask = 1 << 9;
            int layerMask2 = 1 << 2;
            layerMask = ~(layerMask | layerMask2);

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

    public void QueueBullet(GameObject bullet)
    {
        if(isAlive)
        {
            bullets.Enqueue(bullet);
        }
        else
        {
            Destroy(bullet);
        }
    }

    void Shoot()
    {
        if (remainingCooldownTime <= 0f)
        {
            GameObject bullet = bullets.Dequeue();

            if (!bullet.activeSelf)
            {
                bullet.transform.position = bulletSpawn.transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<TurretBulletController>().Reset(bulletLifespan);
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            }
            remainingCooldownTime = cooldownTime;
        }
    }

    public void Destroyed()
    {
        isAlive = false;
        foreach (GameObject x in bullets)
        {
            Destroy(x);
        }
    }
}
