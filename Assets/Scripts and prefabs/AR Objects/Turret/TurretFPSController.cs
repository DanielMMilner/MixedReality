using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TurretFPSController : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;

    public GameObject cannon;
    public float cannonRotationSpeed;
    public float maxGunAngle;
    public float minGunAngle;

    public float maxCannonAngle;
    public float minCannonAngle;

    // Update is called once per frame
    private void Update()
    {
        RotateView();
    }

    private void RotateView()
    {
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (vertical == 0)
            vertical = CrossPlatformInputManager.GetAxis("TurretTurnOtherVertical"); ;

        if (vertical > 0 && cannon.transform.right.y < minGunAngle)
        {
            cannon.transform.Rotate(0,-cannonRotationSpeed, 0);
        }else if (vertical < 0 && cannon.transform.right.y > -maxGunAngle)
        {
            cannon.transform.Rotate(0, cannonRotationSpeed, 0);
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

        if (horizontal == 0)
            horizontal = CrossPlatformInputManager.GetAxis("TurretTurnOtherHorizontal"); ;

        if (horizontal > 0 && transform.forward.x < minCannonAngle)
        {
            transform.Rotate(0, cannonRotationSpeed, 0);
        }
        else if (horizontal < 0 && transform.forward.x > -maxCannonAngle)
        {
            transform.Rotate(0, -cannonRotationSpeed, 0);
        }
    }
}
