﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public GameObject playerParent;
    public GameObject player;
    public GameObject ship;
    public GameObject arCamera;
    public GameObject setUpCamera;
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
        setUpCamera.SetActive(true);

        playerHealth = player.GetComponent<PlayerHealth>();
        shipHealth = ship.GetComponent<ShipHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            Debug.Log("Game Started");
            arCamera.SetActive(false);
            setUpCamera.SetActive(false);
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
