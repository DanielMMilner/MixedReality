using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* */

public class DemoBulletController : MonoBehaviour {

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {

        if (!other.tag.Equals("Bullet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (other.tag.Equals("Enemy"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
