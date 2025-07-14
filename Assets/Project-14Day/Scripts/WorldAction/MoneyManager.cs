using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("Money Settings")]
    [SerializeField] private int _moneyAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FertilizerManager.Instance.AddMoney(_moneyAmount);

            Destroy(gameObject);
        }
    }
}
