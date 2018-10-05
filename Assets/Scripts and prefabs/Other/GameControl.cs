using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameControl : MonoBehaviour {
    public GameObject fpsController;
    public GameObject mixedRealityCamera;
    public GameObject mixedRealityCameraParent;
    public PlayerHealth playerHealth;
    public GameObject ship;
    public GameObject arCamera;
    public GameObject setUpCamera;
    public GameObject overviewCamera;
    public SplineController splineController;
    public GameObject enemies;
    public WallController wallController;
    public ProjectionController turretProjectionController;
    public SpaceShipsController spaceShipsController;
    public bool SpawnEnemies;
    public bool StartSpline;
    public bool useVuforia;

    private ShipHealth shipHealth;
    private bool gameStarted = false;
    private Outline[] outlines;
    private GameObject player;

    private bool overview = false;

    // Use this for initialization
    void Start () {
        mixedRealityCamera.SetActive(false);
        shipHealth = ship.GetComponent<ShipHealth>();
        outlines = enemies.GetComponentsInChildren<Outline>();

        if (useVuforia)
        {
            arCamera.SetActive(true);
            setUpCamera.SetActive(true);
        }
        else
        {
            arCamera.SetActive(false);
            setUpCamera.SetActive(false);
            StartGame();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
            return;

        var interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (var interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                StartGame();
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKey("joystick button 4") || Input.GetKey("joystick button 5"))
        {
            if (overview)
            {
                overview = false;
                ShowOverview(false);
            }
            else
            {
                overview = true;
                ShowOverview(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKey("joystick button 4") || Input.GetKey("joystick button 5"))
        {
            //ShowOverview(false);
        }
    }

    private void ShowOverview(bool show)
    {
        overviewCamera.SetActive(show);

        foreach(Outline outline in outlines)
        {
            outline.enabled = show;
        }
    }

    private void StartGame()
    {
        Debug.Log("Game Started");
        gameStarted = true;
        ShowOverview(false);


        if (UnityEngine.XR.XRDevice.isPresent)
        {
            player = mixedRealityCamera;
            Debug.Log("Using VR Device");
            mixedRealityCamera.SetActive(true);
            fpsController.SetActive(false);
        }
        else
        {
            player = fpsController;
            Debug.Log("Not Using VR Device");
            fpsController.SetActive(true);
            mixedRealityCameraParent.SetActive(false);
        }

        if (useVuforia)
        {
            arCamera.SetActive(false);
            setUpCamera.SetActive(false);
            wallController.Stop();
            turretProjectionController.stop = true;

            ProjectionController[] projectionControllers = ship.GetComponentsInChildren<ProjectionController>();
            foreach (ProjectionController p in projectionControllers)
            {
                p.enabled = false;
            }
        }

        if (SpawnEnemies)
        {
            ShowEnemies();
        }
        else
        {
            enemies.SetActive(false);
        }

        if (StartSpline)
            splineController.StartGame();

        spaceShipsController.StartGame();
    }

    private void ShowEnemies()
    {
        DroneController[] drones = enemies.GetComponentsInChildren<DroneController>();

        foreach (DroneController drone in drones)
        {
            //set the drones to target either the fpscontroller or the mixed reality camera.
            drone.SetTarget(player);
        }

        enemies.SetActive(true);
    }

    public void DamageShip(int amount)
    {
        shipHealth.TakeDamage(amount);
    }

    public void DamagePlayer(int amount)
    {
        playerHealth.TakeDamage(amount);
    }
}
