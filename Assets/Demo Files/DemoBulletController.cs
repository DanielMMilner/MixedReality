using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* */

public class DemoBulletController : MonoBehaviour {

    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
