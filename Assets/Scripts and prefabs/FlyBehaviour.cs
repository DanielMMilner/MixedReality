using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour {
    public float flightSpeed;
    private float currentSpeed = 0;
    public void Fly()
    {
        currentSpeed = flightSpeed;
    }

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * currentSpeed);
    }
}
