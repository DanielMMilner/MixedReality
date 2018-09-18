using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class MountableTurretGunController : MonoBehaviour {
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 5.0f;
    public float bulletLifespan = 5.0f;
    public float bulletCooldown = 0.5f;
    private float _bulletCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (_bulletCooldown > 0f)
        {
            _bulletCooldown -= Time.deltaTime;
        }
        var interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (var interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed && _bulletCooldown <= 0f)
            {
                ShootBullet();
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && _bulletCooldown <= 0f)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        Destroy(bullet, bulletLifespan);
        _bulletCooldown = bulletCooldown;
    }
}
