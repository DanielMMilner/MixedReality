using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectionController : MonoBehaviour {

    public GameObject imageTarget;
    public GameObject shipCentre;
    private MeshRenderer[] mesh;

    // Replace this with the playarea gameobject
    private float SHIP_WIDTH = 1.5f;
    private float SHIP_LENGTH = 2f;

    public float rotation_offset = 0;

    void Start()
    {
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

        Vector3 temp = imageTarget.transform.forward;
        temp.y = 0;

        float y_val;
        if (temp.x > 0)
        {
            y_val = Vector3.Angle(Vector3.forward, temp);
        } else
        {
            y_val = Vector3.Angle(Vector3.forward, -1 * temp) + 180;
        }

        y_val += rotation_offset;

        transform.localEulerAngles = new Vector3(
            shipCentre.transform.rotation.x,
            y_val,
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
