using UnityEngine;

public class EntryPoint: MonoBehaviour
{
    private void Start()
    {
        CameraFollow.Instance.Initialized();

        PlayerControllerManager.Instance.Initialized();

        FertilizerManager.Instance.Initialized();

        Guns.Instance.Initialized();
    }
}
