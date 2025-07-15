using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner preferences")]
    [SerializeField] private EnemySpawnerPreferences _enemySpawnerPreferences;
    [SerializeField] private Button _startAltarButton;

    private void Start()
    {
        _startAltarButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _startAltarButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _startAltarButton.gameObject.SetActive(false);
        }
    }
}
