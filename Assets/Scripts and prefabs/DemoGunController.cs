using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class DemoGunController : MonoBehaviour {

    [Header("Gun Settings")]
    public float recoilTime = 0.3f;         // How long to recoil for
    private float _recoilTime = 0.0f;       // Internal variable to track the recoil cooldown
    public float maxRecoil = 5f;            // The amount to recoil by (in random range)
    private float _maxRecoil_x;             // Internal record for x and y direction
    private float _maxRecoil_y;             //
    public float recoilSpeed = 10f;         // How fast to carry out the recoil
    private float _recoilSpeed = 0f;

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

            StartRecoil(recoilTime, maxRecoil, recoilSpeed);
        }
    }


    void StartRecoil(float recoilTime, float maxRecoil, float recoilSpeed)
    {
        _recoilTime = recoilTime;
        _maxRecoil_x = maxRecoil;
        _recoilSpeed = recoilSpeed;
        _maxRecoil_y = Random.Range(-maxRecoil, maxRecoil);
    }

    void UpdateRecoil()
    {
        // If currently recoiling
        if (_recoilTime > 0f)
        {
            Quaternion maxRecoil = Quaternion.Euler(-_maxRecoil_x, _maxRecoil_y, 0f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * _recoilSpeed);
            _recoilTime -= Time.deltaTime;
        
        // Otherwise returning to correct orientation
        } else {
            _recoilTime = 0f;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * _recoilSpeed / 2);
        }
    }
}
