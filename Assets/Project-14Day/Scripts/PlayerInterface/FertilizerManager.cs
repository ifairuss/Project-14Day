using TMPro;
using UnityEngine;

public class FertilizerManager : MonoBehaviour
{

    public static FertilizerManager Instance { get; private set; }

    [Header("Fertilizer settings")]
    [SerializeField] private TextMeshProUGUI _fertilizerCountText;

    private int _fertilizerCount;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialized()
    {
        _fertilizerCountText.text = _fertilizerCount.ToString();
    }

    public void AddMoney(int amount)
    {
        _fertilizerCount += amount;
        _fertilizerCountText.text = _fertilizerCount.ToString();
        Debug.Log($"Money added: {amount}");
    }

    public void RemoveMoney(int amount)
    {
        if (_fertilizerCount != 0)
        {
            _fertilizerCount -= amount;

            if (_fertilizerCount < 0)
            {
                _fertilizerCount = 0;
            }

            _fertilizerCountText.text = _fertilizerCount.ToString();
            Debug.Log($"Money removed: {amount}");
        }
    }
}
