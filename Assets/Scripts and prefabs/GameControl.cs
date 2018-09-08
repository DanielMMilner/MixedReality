using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public GameObject playerParent;
    public GameObject player;
    public GameObject ship;
    public GameObject arCamera;
    public SplineController splineController;
    public GameObject enemies;

    private PlayerHealth playerHealth;
    private ShipHealth shipHealth;

    public bool SpawnEnemies;

    private bool gameStarted = false;


    // Use this for initialization
    void Start () {
        playerParent.SetActive(false);
        enemies.SetActive(false);
        arCamera.SetActive(true);

        playerHealth = player.GetComponent<PlayerHealth>();
        shipHealth = ship.GetComponent<ShipHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameStarted = true;
            Debug.Log("Game Started");
            arCamera.SetActive(false);
            playerParent.SetActive(true);

            if(SpawnEnemies)
                enemies.SetActive(true);

            splineController.StartGame();
        }
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
