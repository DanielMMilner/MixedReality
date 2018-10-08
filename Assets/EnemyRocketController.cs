using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocketController : MonoBehaviour
{
    public ParticleSystem explosion;
    public int damage = 30;
    private float currentLifeSpan;

    public void Reset(float lifeSpan)
    {
        currentLifeSpan = lifeSpan;
    }

    void FixedUpdate()
    {
        currentLifeSpan -= Time.deltaTime;

        if (currentLifeSpan < 0)
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Enemy") && !other.tag.Equals("EnemyDrone") && !other.tag.Equals("InvisibleWall") && !other.tag.Equals("ChangeSplineSpeed"))
        {
            if (explosion != null)
                Instantiate(explosion, transform.position, transform.rotation);

            DisableBullet();

            if (other.tag.Equals("MainCamera"))
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamagePlayer(damage);
            }
            else if (other.tag.Equals("PlayerShip"))
            {
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                GameControl gameControl = gameController.GetComponent<GameControl>();
                gameControl.DamageShip(damage);
            }
        }
    }
}
