using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretController : MonoBehaviour {

    private GameObject player;

    public float range = 100;

    // Time in seconds
    public float cooldownTime = 5;

    public GameObject bullet;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        // Raycast to object
        // Determine distance between objects
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < range)
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
            if(cooldownTime <= 0)
            {
                // Fire

            }
        }
        else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        cooldownTime -= Time.fixedDeltaTime;
	}
}
