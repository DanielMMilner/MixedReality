using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class DemoGunController : MonoBehaviour {

    [Header("Gun Settings")]
    public float recoilTime = 0.3f;
    private float _recoilTime = 0.0f;
    public float maxRecoil = 5f;
    private float _maxRecoil_x;
    private float _maxRecoil_y;
    public float recoilSpeed = 2f;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 5.0f;
    public float bulletLifespan = 5.0f;
    public float bulletCooldown = 0.5f;
    private float _bulletCooldown = 0f;


	// Update is called once per frame
	void Update () {
        UpdateRecoil();
        Fire();
	}

    void Fire()
    {
        if (_bulletCooldown > 0f) {
            _bulletCooldown -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Mouse0) && _bulletCooldown <= 0f)
        {
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            Destroy(bullet, bulletLifespan);
            _bulletCooldown = bulletCooldown;

            StartRecoil(recoilTime, maxRecoil, 10f);
        }
    }


    void StartRecoil(float recoilTime, float maxRecoil, float recoilSpeedParam)
    {
        _recoilTime = recoilTime;
        _maxRecoil_x = maxRecoil;
        recoilSpeed = recoilSpeedParam;
        _maxRecoil_y = Random.Range(-maxRecoil, maxRecoil);
    }

    void UpdateRecoil()
    {
        if (_recoilTime > 0f)
        {
            Quaternion maxRecoil = Quaternion.Euler(-_maxRecoil_x, _maxRecoil_y, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
            _recoilTime -= Time.deltaTime;
        } else
        {
            _recoilTime = 0f;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
        }
    }
}
