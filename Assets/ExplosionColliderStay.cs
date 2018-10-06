using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionColliderStay : MonoBehaviour {

    void Start()
    {
        Destroy(this.gameObject, 1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Bullet") && !other.CompareTag("InvisibleWall") && !other.CompareTag("ChangeSplineSpeed"))
        {
            //Instantiate(explosion, transform.position, Quaternion.identity);
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
