using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosionScript : MonoBehaviour {
    //private ParticleSystem ps;
    public float length; 

    // Use this for initialization
    void Start () {
        //ps = GetComponent<ParticleSystem>();
        Destroy(gameObject, length);
    }

    // Update is called once per frame
    //void Update () {
    //    if(ps && !ps.IsAlive())
    //    {
    //        Destroy(gameObject);
    //    }		
	//}
}
