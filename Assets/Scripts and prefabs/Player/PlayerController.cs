using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* A simple movement script for the player while we don't have access to the VR headset */

public class PlayerController : MonoBehaviour {

    [Header("Player Movement Settings")]
    public float turnSpeed = 200;
    public float movementSpeed = 2;

	void Update () {

        // Rotation using WASD Keys
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * -1, Space.World);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed, Space.World);
        } else if (Input.GetKey(KeyCode.W)) {
            transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * -1);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed);
        }

        // Movement using Up and Down
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * -1);
        }
        
    }
}
