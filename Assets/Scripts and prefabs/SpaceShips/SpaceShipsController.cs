using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipsController : MonoBehaviour {
    private FlyBehaviour[] flyBehaviours;
	// Use this for initialization
	void Start () {
        flyBehaviours = GetComponentsInChildren<FlyBehaviour>();
	}
	
    public void StartGame()
    {
        foreach(FlyBehaviour fb in flyBehaviours)
        {
            fb.Fly();
        }
    }
}
