using TMPro;
using UnityEngine;

public class EnemyCounterDead : MonoBehaviour
{
    public static EnemyCounterDead Instance;

    public TextMeshProUGUI EnemyCounter;

    public int EnemyDead;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCountedDead()
    {
        EnemyDead += 1;

        EnemyCounter.text = EnemyDead.ToString();
    }

    public void RemoveCountedDead()
    {
        EnemyDead = 0;

        EnemyCounter.text = EnemyDead.ToString();
    }
}
