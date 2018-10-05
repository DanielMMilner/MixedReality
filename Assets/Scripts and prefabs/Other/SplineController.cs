using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

[RequireComponent(typeof(BGCcMath))]
public class SplineController : MonoBehaviour {
    private BGCcMath mathCurve;

    public GameObject objToMove;

    public float speed = 1;
    public float distance = 0;

    public float lookPointDist = 10;
    private Vector3 lookPoint;

    private bool start = false;

    // Use this for initialization
    void Start () {
        mathCurve = GetComponent<BGCcMath>();
    }
	
	// Update is called once per frame
	void Update () {
	}


    public void StartGame() {
        this.start = true;
    }

    private void FixedUpdate()
    {
        if (!start) return;
        //d = s * t
        distance += speed * Time.fixedDeltaTime;

        lookPoint = mathCurve.CalcPositionByDistance(distance + lookPointDist);
        objToMove.transform.position = mathCurve.CalcPositionByDistance(distance);

        var heading = lookPoint - objToMove.transform.position;
        heading.y = 0;
        objToMove.transform.rotation = Quaternion.LookRotation(heading);
    }
}
