using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private bool _canMove = true;

    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    private NavMeshAgent _agent;
    private Animator _enemyAnimator;

    [Header("Flip sprite")]
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _leftHand;
    [SerializeField] private SpriteRenderer _rightHand;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
        
    }

    public void MoveToTarget(Transform target, EnemyPreferences enemyData, float speed)
    {
        if (target == null) return;

        _agent.speed = speed;

        if (_canMove)
        {
            _agent.SetDestination(target.position);
        }

        if (Vector3.Distance(target.position, transform.position) <= (_agent.stoppingDistance + 0.2f))
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }

        _enemyAnimator.SetFloat("directionY", _agent.velocity.y);
        _enemyAnimator.SetFloat("directionX", _agent.velocity.x);

        Flip(target, enemyData);
    }

    private void Flip(Transform target, EnemyPreferences enemyData)
    {
        float deltaX = transform.position.x - target.position.x;
        float deltaY = transform.position.y - target.position.y;

        if(deltaX > deltaY)
        {
            if (deltaX > 0 && _agent.velocity.x < -0.60f)
            {
                _body.sprite = enemyData.BodyRightSprite;
                _head.sprite = enemyData.HeadRightSprite;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _leftHand.sortingOrder = -20;
                _rightHand.sortingOrder = 20;
            }
            else
            {
                _body.sprite = enemyData.BodyTopSprite;
                _head.sprite = enemyData.HeadTopSprite;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _leftHand.sortingOrder = -20;
                _rightHand.sortingOrder = -20;
            }
        }
        else
        {
            if (deltaY > 0 && _agent.velocity.y < -0.60f)
            {
                _body.sprite = enemyData.BodyBottomSprite;
                _head.sprite = enemyData.HeadBottomSprite;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _leftHand.sortingOrder = 20;
                _rightHand.sortingOrder = 20;
            }
            else
            {
                _body.sprite = enemyData.BodyRightSprite;
                _head.sprite = enemyData.HeadRightSprite;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _leftHand.sortingOrder = -20;
                _rightHand.sortingOrder = 20;
            }
        }
    }
}
