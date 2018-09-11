using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TurretFPSController : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;

    private Camera m_Camera;

    public GameObject cannon;
    public float cannonRotationSpeed;
    public float maxCannonAngle;
    public float minCannonAngle;


    private void Start()
    {
        m_Camera = Camera.main;
        m_MouseLook.Init(transform, m_Camera.transform);
    }

    // Update is called once per frame
    private void Update()
    {
        RotateView();
    }

    private void RotateView()
    {
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        Debug.Log(cannon.transform.right.y);
        
        if (vertical > 0 && cannon.transform.right.y < minCannonAngle)
        {
            cannon.transform.Rotate(0,-cannonRotationSpeed, 0);
        }else if (vertical < 0 && cannon.transform.right.y > -maxCannonAngle)
        {
            Debug.Log("Up");
            cannon.transform.Rotate(0, cannonRotationSpeed, 0);
        }

        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
