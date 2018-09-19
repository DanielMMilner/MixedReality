using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityStandardAssets.Characters.FirstPerson;

public class GameControl : MonoBehaviour {
    public GameObject playerParent;

    public FirstPersonController fpsController;
    public GameObject fpsCharacter;

    public GameObject mixedRealityCameraParent;

    public PlayerHealth playerHealth;
    public GameObject ship;
    public GameObject arCamera;
    public GameObject setUpCamera;
    public SplineController splineController;
    public GameObject enemies;
    public WallController wallController;
    public SpaceShipsController spaceShipsController;
    public bool SpawnEnemies;
    public bool StartSpline;
    public bool useVuforia;

    private ShipHealth shipHealth;
    private bool gameStarted = false;


    // Use this for initialization
    void Start () {
        playerParent.SetActive(false);
        shipHealth = ship.GetComponent<ShipHealth>();

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
    }

    private void StartGame()
    {
        Debug.Log("Game Started");
        gameStarted = true;

        playerParent.SetActive(true);

        if (UnityEngine.XR.XRDevice.isPresent)
        {
            Debug.Log("Using VR Device");
            mixedRealityCameraParent.SetActive(true);
            fpsController.enabled = false;
            fpsCharacter.SetActive(false);
        }
        else
        {
            Debug.Log("Not Using VR Device");
            mixedRealityCameraParent.SetActive(false);
            fpsController.enabled = true;
            fpsCharacter.SetActive(true);
        }

        if (useVuforia)
        {
            arCamera.SetActive(false);
            setUpCamera.SetActive(false);
            wallController.stop = true;

            ProjectionController[] projectionControllers = ship.GetComponentsInChildren<ProjectionController>();
            foreach (ProjectionController p in projectionControllers)
            {
                p.enabled = false;
            }
        }

        enemies.SetActive(SpawnEnemies);

        if (StartSpline)
            splineController.StartGame();

        spaceShipsController.StartGame();
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
