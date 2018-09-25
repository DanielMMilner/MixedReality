using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour {
    public int health = 1000;

    public void TakeDamage(int amount)
    {
        health -= amount;
    }
}
