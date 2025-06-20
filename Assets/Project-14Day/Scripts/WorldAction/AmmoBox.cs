using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [Header("Box preferences")]
    [SerializeField] private AmmoBoxPreferences _boxPreferences;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Guns gun = collision.GetComponentInChildren<Guns>();

            gun.AddAmmo(_boxPreferences.AmmoCount);

            Destroy(gameObject);
        }
    }
}
