using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosionScript : MonoBehaviour {
    public float length; 

    void Start () {
        Destroy(gameObject, length);
    }
}
