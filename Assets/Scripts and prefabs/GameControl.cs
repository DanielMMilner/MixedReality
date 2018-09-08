using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
    public GameObject player;
    public GameObject arCamera;
    public SplineController splineController;
    public GameObject enemies;

    public bool arTesting;

    private bool gameStarted = false;


    // Use this for initialization
    void Start () {
        player.SetActive(false);
        enemies.SetActive(false);
        arCamera.SetActive(true);      
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameStarted && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameStarted = true;
            Debug.Log("Game Started");
            arCamera.SetActive(false);
            player.SetActive(true);

            if(!arTesting)
                enemies.SetActive(true);

            splineController.StartGame();
        }
    }
}
