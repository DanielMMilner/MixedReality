﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Bullet") && !other.tag.Equals("InvisibleWall") && !other.tag.Equals("ChangeSplineSpeed"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (other.tag.Equals("EnemyDrone"))
            {
                other.GetComponent<DroneController>().Died(transform.position);
            }
            else if (other.tag.Equals("Enemy"))
            {
                other.gameObject.GetComponent<TurretController>().Destroyed();
            }
        }
    }
}
