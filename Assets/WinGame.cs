using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour {
    public GameObject canvas; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerShip"))
        {
            canvas.SetActive(true);
        }
    }
}
