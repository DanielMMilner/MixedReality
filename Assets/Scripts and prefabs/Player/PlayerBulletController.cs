using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    [Header("Particle Settings")]
    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet") && !other.CompareTag("InvisibleWall") && !other.CompareTag("ChangeSplineSpeed"))
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
