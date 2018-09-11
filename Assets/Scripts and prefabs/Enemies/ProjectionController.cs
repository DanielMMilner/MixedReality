using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionController : MonoBehaviour {

    public GameObject imageTarget;
    public GameObject ship;
    private CustomTrackableEventHandler imageScript;
    private MeshRenderer mesh;

    // Replace this with the playarea gameobject
    private float SHIP_WIDTH = 10f;
    private float SHIP_LENGTH = 20f;

    void Start()
    {
        imageScript = imageTarget.GetComponent<CustomTrackableEventHandler>();
        mesh = GetComponent<MeshRenderer>();
    }
	
	void FixedUpdate () {
        // This checks if tracking has been lost.
        /*if (imageScript.someSortOfBooleanMethod()){
            return;
        }*/

        if (imageTarget.transform.position.x > ship.transform.position.x + SHIP_WIDTH ||
            imageTarget.transform.position.x < ship.transform.position.x - SHIP_WIDTH || 
            imageTarget.transform.position.z > ship.transform.position.z + SHIP_LENGTH ||
            imageTarget.transform.position.z < ship.transform.position.z - SHIP_LENGTH)
        {
            Disable();
            return;
        }

        transform.position = new Vector3(
            imageTarget.transform.position.x,
            ship.transform.position.y,
            imageTarget.transform.position.z
        );

        transform.localEulerAngles = new Vector3(
            ship.transform.rotation.x,
            imageTarget.transform.rotation.y,
            ship.transform.rotation.z
        );

        Enable();
	}

    void Disable()
    {
        mesh.enabled = false;
    }

    void Enable()
    {
        mesh.enabled = true;
    }
}
