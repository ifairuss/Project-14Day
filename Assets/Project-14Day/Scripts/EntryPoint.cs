using UnityEngine;

public class EntryPoint: MonoBehaviour
{
    private void Start()
    {
        PlayerControllerManager.Instance.Initialized();
        CameraFollow.Instance.Initialized();
    }
}
