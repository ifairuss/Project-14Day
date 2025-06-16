using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    private Vector3 _offSet;
    private Transform _target;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialized()
    {
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        _offSet = _target.position - transform.position;
    }

    public void CameraFollowAtPlayer(float cameraSpeed)
    {
       transform.position = Vector3.Lerp(transform.position, _target.position - _offSet, cameraSpeed * Time.deltaTime);
    }
}
