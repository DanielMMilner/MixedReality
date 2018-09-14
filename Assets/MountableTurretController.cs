using UnityEngine;

public class MountableTurretController : MonoBehaviour {

    public float timerStart;
    public GameObject turretCamera;
    public GameObject player;

    private bool timerStarted = false;
    private bool inTurret = false;
    private MountableTurretGunController mountableTurretGunController;
    private TurretFPSController turretFPSController;

    private float timeLeft;

    public void Start()
    {
        mountableTurretGunController = GetComponent<MountableTurretGunController>();
        turretFPSController = GetComponentInChildren<TurretFPSController>();
        mountableTurretGunController.enabled = false;

        timeLeft = timerStart;
        turretFPSController.enabled = false;
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

        turretFPSController.enabled = true;
        mountableTurretGunController.enabled = true;

        player.SetActive(false);
        turretCamera.SetActive(true);
    }

    private void ExitTurret()
    {
        Debug.Log("Exiting turret");
        inTurret = false;

        turretFPSController.enabled = false;
        mountableTurretGunController.enabled = false;

        player.SetActive(true);
        turretCamera.SetActive(false);
        timeLeft = timerStart;
    }
}
