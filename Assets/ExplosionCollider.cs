using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour {
    public GameObject colliderExplosion;

    void OnDestroy()
    {
        Instantiate<GameObject>(colliderExplosion, transform.position, Quaternion.identity);
    }
}
