using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* */

public class DemoBulletController : MonoBehaviour {

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        // Create an explosion and destroy yourself.
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);

        // In future, damage the drone or just destroy it.
    }
}
