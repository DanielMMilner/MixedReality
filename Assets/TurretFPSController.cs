using UnityEngine;

public class TurretFPSController : MonoBehaviour
{
    [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;

    private Camera m_Camera;

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
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
