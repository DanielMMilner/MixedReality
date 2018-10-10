using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    [Header("Particle Settings")]
    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyDrone") || other.CompareTag("Enemy") || other.CompareTag("Mounatins"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (other.CompareTag("EnemyDrone"))
            {
                other.GetComponent<DroneController>().Died(transform.position);
            }
            else if (other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<TurretController>().Destroyed();
            }
        }
    }
}
