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
        //print(_agent.velocity);
        float velocityOnTarget = 0.77f;

        if (_agent.velocity.x > velocityOnTarget && _agent.velocity.y < velocityOnTarget && _agent.velocity.y > -velocityOnTarget)
        {
            _body.sprite = enemyData.BodyRightSprite;
            _head.sprite = enemyData.HeadRightSprite;

            

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_agent.velocity.x < -velocityOnTarget && _agent.velocity.y < velocityOnTarget && _agent.velocity.y > -velocityOnTarget)
        {
            _body.sprite = enemyData.BodyRightSprite;
            _head.sprite = enemyData.HeadRightSprite;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (_agent.velocity.y > velocityOnTarget && _agent.velocity.x < velocityOnTarget && _agent.velocity.x > -velocityOnTarget)
        {
            _body.sprite = enemyData.BodyTopSprite;
            _head.sprite = enemyData.HeadTopSprite;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_agent.velocity.y < -velocityOnTarget && _agent.velocity.x < velocityOnTarget && _agent.velocity.x > -velocityOnTarget)
        {
            _body.sprite = enemyData.BodyBottomSprite;
            _head.sprite = enemyData.HeadBottomSprite;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (
            _agent.velocity.y >= -velocityOnTarget &&
            _agent.velocity.y <= velocityOnTarget && 
            _agent.velocity.x >= -velocityOnTarget && 
            _agent.velocity.x <= velocityOnTarget &&
            target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (
            _agent.velocity.y >= -velocityOnTarget &&
            _agent.velocity.y <= velocityOnTarget &&
            _agent.velocity.x >= -velocityOnTarget &&
            _agent.velocity.x <= velocityOnTarget &&
            target.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
