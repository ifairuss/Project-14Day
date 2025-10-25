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

        transform.position = new Vector3(_target.position.x, _target.position.y + 0.7f, -10);

        _offSet = _target.position - transform.position;
    }

    public void CameraFollowAtPlayer(float cameraSpeed)
    {
       transform.position = Vector3.Lerp(transform.position, _target.position - _offSet, cameraSpeed * Time.deltaTime);
    }
}
