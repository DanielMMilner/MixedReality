using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GameControl : MonoBehaviour {
    public GameObject playerParent;
    public GameObject player;
    public GameObject ship;
    public GameObject arCamera;
    public GameObject setUpCamera;
    public SplineController splineController;
    public GameObject enemies;
    public WallController wallController;
    public bool SpawnEnemies;
    public bool StartSpline;

    private PlayerHealth playerHealth;
    private ShipHealth shipHealth;
    private bool gameStarted = false;


    // Use this for initialization
    void Start () {
        playerParent.SetActive(false);
        enemies.SetActive(false);
        arCamera.SetActive(true);
        setUpCamera.SetActive(true);

        playerHealth = player.GetComponent<PlayerHealth>();
        shipHealth = ship.GetComponent<ShipHealth>();
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
        gameStarted = true;
        Debug.Log("Game Started");
        arCamera.SetActive(false);
        setUpCamera.SetActive(false);
        playerParent.SetActive(true);
        wallController.stop = true;

        if (SpawnEnemies)
            enemies.SetActive(true);

        if(StartSpline)
            splineController.StartGame();
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
