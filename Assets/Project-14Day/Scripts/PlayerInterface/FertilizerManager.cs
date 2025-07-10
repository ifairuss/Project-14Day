using TMPro;
using UnityEngine;

public class FertilizerManager : MonoBehaviour
{
    [Header("Fertilizer settings")]
    [SerializeField] private TextMeshProUGUI _fertilizerCountText;

    private int _fertilizerCount;

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        _fertilizerCountText.text = _fertilizerCount.ToString();
    }

    private void Update()
    {
        
    }
}
