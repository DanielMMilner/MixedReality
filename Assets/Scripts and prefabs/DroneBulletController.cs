using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBulletController : MonoBehaviour
{

    public ParticleSystem explosion;
    public int damage = 5;

    private void OnTriggerEnter(Collider other)
    {
        // Create an explosion and destroy yourself.
        if (!other.tag.Equals("Enemy") && !other.tag.Equals("DroneBullet") && !other.tag.Equals("PlayerBullet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            if (other.tag.Equals("MainCamera"))
            {
                Debug.Log("Drone hit player");
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamagePlayer(damage);
            }
            else if (other.tag.Equals("PlayerShip"))
            {
                Debug.Log("Drone hit Ship");

                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamageShip(damage);
            }
        }

        // In future, damage the players ship.
    }
}
