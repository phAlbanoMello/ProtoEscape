using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_FollowTarget;


    private void LateUpdate()
    {
        transform.position = new Vector3(m_FollowTarget.position.x, m_FollowTarget.position.y, transform.position.z);
    }
}
