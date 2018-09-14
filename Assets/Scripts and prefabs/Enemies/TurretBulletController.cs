using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurretBulletController : MonoBehaviour
{

    public ParticleSystem explosion;
    public int damage = 30;

    private void OnTriggerEnter(Collider other)
    {
        // Create an explosion and destroy yourself.
        if (!other.tag.Equals("Enemy") && !other.tag.Equals("Bullet") && !other.tag.Equals("InvisibleWall"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
            if (other.tag.Equals("MainCamera"))
            {
                //Debug.Log("Turret hit player");

                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamagePlayer(damage);
            }
            else if (other.tag.Equals("PlayerShip"))
            {
                //Debug.Log("Turret hit Ship");

                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamageShip(damage);
            }
        }

        // In future, damage the players ship.
    }
}
