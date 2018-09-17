﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretBulletController : MonoBehaviour
{
    public ParticleSystem explosion;
    public int damage = 30;
    private float currentLifeSpan;
    private TurretController parentTurret;

    public void SetParentTurret(TurretController parent)
    {
        parentTurret = parent;
    }

    public void Reset(float lifeSpan)
    {
        currentLifeSpan = lifeSpan;
    }

    void FixedUpdate()
    {
        currentLifeSpan -= Time.deltaTime;

        if (currentLifeSpan < 0)
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        parentTurret.QueueBullet(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Enemy") && !other.tag.Equals("Bullet") && !other.tag.Equals("InvisibleWall") && !other.tag.Equals("ChangeSplineSpeed"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            DisableBullet();

            if (other.tag.Equals("MainCamera"))
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamagePlayer(damage);
            }
            else if (other.tag.Equals("PlayerShip"))
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamageShip(damage);
            }
        }
    }
}
