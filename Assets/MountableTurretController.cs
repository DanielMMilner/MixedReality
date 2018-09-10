using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountableTurretController : MonoBehaviour {

    public float timerStart;
    public GameObject turretCamera;
    public GameObject fpsCamera;

    private bool timerStarted = false;
    private bool inTurret = false;

    private float timeLeft;

    public void Start()
    {
        timeLeft = timerStart;
    }

    public void Update()
    {
        if (timerStarted && !inTurret)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft < 0)
            {
                EnterTurret();
            }
        }else if (inTurret && Input.GetKeyDown(KeyCode.Space))
        {
            ExitTurret();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            Debug.Log("Starting mountable turret timer");
            timerStarted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("MainCamera"))
        {
            Debug.Log("Stopping mountable turret timer");
            timerStarted = false;
            timeLeft = timerStart;
        }
    }

    private void EnterTurret()
    {
        Debug.Log("Entering turret");
        inTurret = true;
        fpsCamera.SetActive(false);
        turretCamera.SetActive(true);
    }

    private void ExitTurret()
    {
        Debug.Log("Exiting turret");
        inTurret = false;
        fpsCamera.SetActive(true);
        turretCamera.SetActive(false);
        timeLeft = timerStart;
    }
}
