using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSplineSpeed : MonoBehaviour {
    public SplineController splineController;
    public float newSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerShip"))
        {
            splineController.speed = newSpeed;
        }
    }
}
