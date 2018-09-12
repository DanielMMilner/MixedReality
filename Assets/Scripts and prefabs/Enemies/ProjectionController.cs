using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionController : MonoBehaviour {

    public GameObject imageTarget;
    public GameObject shipCentre;
    private CustomTrackableEventHandler imageScript;
    private MeshRenderer[] mesh;

    // Replace this with the playarea gameobject
    private float SHIP_WIDTH = 1.5f;
    private float SHIP_LENGTH = 2f;

    void Start()
    {
        imageScript = imageTarget.GetComponent<CustomTrackableEventHandler>();
        mesh = this.GetComponentsInChildren<MeshRenderer>();
    }
	
	void FixedUpdate () {
        // This checks if tracking has been lost.
        /*if (imageScript.someSortOfBooleanMethod()){
            return;
        }*/

        if (imageTarget.transform.position.x > shipCentre.transform.position.x + SHIP_WIDTH ||
            imageTarget.transform.position.x < shipCentre.transform.position.x - SHIP_WIDTH || 
            imageTarget.transform.position.z > shipCentre.transform.position.z + SHIP_LENGTH ||
            imageTarget.transform.position.z < shipCentre.transform.position.z - SHIP_LENGTH)
        {
            Disable();
            return;
        }

        transform.position = new Vector3(
            imageTarget.transform.position.x,
            shipCentre.transform.position.y,
            imageTarget.transform.position.z
        );

        transform.localEulerAngles = new Vector3(
            shipCentre.transform.rotation.x,
            imageTarget.transform.rotation.y,
            shipCentre.transform.rotation.z
        );

        Enable();
	}

    void Disable()
    {
        foreach (MeshRenderer m in mesh) {
            m.enabled = false;
        }
    }

    void Enable()
    {
        foreach (MeshRenderer m in mesh)
        {
            m.enabled = true;
        }
    }
}
