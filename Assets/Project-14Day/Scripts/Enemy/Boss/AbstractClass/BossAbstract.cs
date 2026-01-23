using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class BossAbstract : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private float _speed;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private string _bossName;

    public float Health;

    [Space]
    [Header("Boss Phase HP")]
    public float HealthInPhaseThree;
    public float HealthInPhaseTwo;
    public float HealthInPhaseOne;

    [Header("System status")]
    [SerializeField] private bool _isMoving = true;

    [Space]
    public PlayerControllerManager PlayerControllerManager;

    private BossHPData _bossUITransform;

    private GameObject _bossHealthBar;
    private Image _imageHealthMiddleground;
    private Image _imageHealthForwardground;
    private TextMeshProUGUI _bossNameUIText;

    [HideInInspector] public NavMeshAgent Agent;

    public bool BossSpawnedPhaseThree;
    public bool BossSpawnedPhaseTwo;
    public bool BossSpawnedPhaseOne;

    private void BossUIHealthGetComponents()
    {
        _bossHealthBar = _bossUITransform.BossHeatBar;
        _imageHealthMiddleground = _bossUITransform.ImageHealthMiddleground;
        _imageHealthForwardground = _bossUITransform.ImageHealthForwardground;
        _bossNameUIText = _bossUITransform.BossNameUIText;

        _bossNameUIText.text = _bossName;
        _bossHealthBar.SetActive(true);
        _imageHealthForwardground.fillAmount = Health / 1000;
    }

    public void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        PlayerControllerManager = GameObject.FindWithTag("Player").GetComponent<PlayerControllerManager>();
        _bossUITransform = GameObject.FindWithTag("WorldUI").GetComponent<BossHPData>();

        Agent.speed = _speed;

        BossUIHealthGetComponents();

        BossSpawnedPhaseThree = true;
    }

    public virtual void Update()
    {
        if (Health <= 0)
        {
            BossDead();
            _bossHealthBar?.SetActive(false);
        }

        BossMoving();
        BossAttack();
        HealthUpdate();
    }

    public void HealthUpdate()
    {
        _imageHealthForwardground.fillAmount = Health / 1000;

        if (_imageHealthMiddleground.fillAmount > _imageHealthForwardground.fillAmount)
        {
            _imageHealthMiddleground.fillAmount -= 0.05f * Time.fixedDeltaTime;
        }
        else if (_imageHealthMiddleground.fillAmount < _imageHealthForwardground.fillAmount)
        {
            _imageHealthMiddleground.fillAmount = _imageHealthForwardground.fillAmount;
        }
    }

    private void Flip(Transform PlayerControllerManager)
    {
        if (PlayerControllerManager.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public virtual void BossMoving()
    {
        if (PlayerControllerManager == null && Health > 0 && _isMoving) return;

        Agent.SetDestination(PlayerControllerManager.transform.position);

        Flip(PlayerControllerManager.transform);
    }

    public virtual void BossAttack()
    {
        //Не готового убрав но не зробив - потім доробити
       //if (Vector3.Distance(transform.position, PlayerControllerManager.transform.position) <= (Agent.stoppingDistance + 0.3f))
       //{
       //   
       //}
    }

    public void PlayerTakeDamage(int damage)
    {
        PlayerControllerManager.TakeDamage(damage);
    }

    public virtual void BossDead()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
