using System.Collections.Generic;
using UnityEngine;

public abstract class BossAbstract : MonoBehaviour
{
    [Header("Boss help")]
    [SerializeField] private List<GameObject> _zombieVariable;

    [Header("Preferences")]
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private float _timeToAttack;

    [Header("Dmage Preferences")]
    [SerializeField] private int _defaulthDamage;
    [SerializeField] private int _rangeDamage;
    [SerializeField] private int _splashDamage;

    [Header("System status")]
    [SerializeField] private bool _isMoving;
    
    public virtual void BossMoving()
    {

    }

    public virtual void BossAttack()
    {

    }
}
