using UnityEngine;

public class CamFollowTarget : MonoBehaviour
{
    public static CamFollowTarget Instance;
    public Transform target;
    public float CamFollowSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * CamFollowSpeed);

        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) {Destroy(gameObject); return; }
        Instance = this;
        target = null;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
