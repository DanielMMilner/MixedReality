using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretBulletController : MonoBehaviour
{

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        // Create an explosion and destroy yourself.
        if (!other.tag.Equals("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }

        // In future, damage the players ship.
    }
}
