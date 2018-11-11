using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TurretFPSController : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;

    public GameObject cannon;
    public Transform shipParentTransform;

    public float cannonRotationSpeed;
    public float maxGunAngle;
    public float minGunAngle;

    public float cannonRotationAngle;

    private Quaternion rightCannonRotation;
    private Quaternion leftCannonRotation;

    public void Start()
    {
        rightCannonRotation = transform.localRotation;
        leftCannonRotation = transform.localRotation;
        
        rightCannonRotation *= Quaternion.Euler(0, cannonRotationAngle, 0); // this adds a 45 degrees Y rotation
        leftCannonRotation *= Quaternion.Euler(0, -cannonRotationAngle, 0); // this adds a -45 degrees Y rotation
    }

    // Update is called once per frame
    private void Update()
    {
        RotateView();
    }

    private void RotateView()
    {
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (vertical == 0) //alternative value used when playing with a mouse and keyboard
            vertical = CrossPlatformInputManager.GetAxis("TurretTurnOtherVertical");

        if (vertical > 0 && cannon.transform.right.y < minGunAngle)
        {
            cannon.transform.Rotate(0,-cannonRotationSpeed, 0);
        }else if (vertical < 0 && cannon.transform.right.y > -maxGunAngle)
        {
            cannon.transform.Rotate(0, cannonRotationSpeed, 0);
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        if (horizontal == 0) //alternative value used when playing with a mouse and keyboard
            horizontal = CrossPlatformInputManager.GetAxis("TurretTurnOtherHorizontal");

        if (horizontal > 0 && -transform.localRotation.y > -rightCannonRotation.y)
        {
            transform.Rotate(0, cannonRotationSpeed, 0);
        }else if (horizontal < 0 && -transform.localRotation.y < -leftCannonRotation.y)
        {
            transform.Rotate(0, -cannonRotationSpeed, 0);
        }        
    }
}
