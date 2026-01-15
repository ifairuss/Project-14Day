using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _canDebug = false;

    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    private NavMeshAgent _agent;
    private Animator _enemyAnimator;
    private Transform _enemyTargetPointContainer;

    [Header("Enemy components data")]
    [SerializeField] private Transform _enemyTargetPoint;
    [SerializeField] private Transform _enemyWeaponPoint;

    [Header("Flip sprite")]
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _leftHand;
    [SerializeField] private SpriteRenderer _rightHand;


    [Header("Debug properties")]
    [SerializeField] private bool _visibleEnemyDeltaPosition = false;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
        _enemyTargetPointContainer = GameObject.FindGameObjectWithTag("EnemyPointContainer").GetComponent<Transform>();


    }

    public void MoveToTarget(Transform target, EnemyPreferences enemyData, float speed)
    {
        if (target == null) return;

        _agent.speed = speed;
        EnemyPointData();

        if (_canMove)
        {
            _agent.SetDestination(target.position);
        }

        if (Vector3.Distance(target.position, transform.position) <= (_agent.stoppingDistance + 0.3f))
        {
            IsMoving = false;
            _enemyAnimator.SetBool("Attack", true);
        }
        else
        {
            _enemyAnimator.SetBool("Attack", false);
            IsMoving = true;
        }

        _enemyAnimator.SetFloat("directionY", _agent.velocity.y);
        _enemyAnimator.SetFloat("directionX", _agent.velocity.x);

        Flip(target, enemyData);
    }

    private void EnemyPointData()
    {
        _enemyTargetPoint.position = transform.position;
        _enemyTargetPoint.rotation = Quaternion.Euler(0, 0, 0);
        _enemyTargetPoint.SetParent(_enemyTargetPointContainer);
    }

    private void PrintDebug(float deltaX, float deltaY)
    {
        if (_canDebug)
        {
            string DelayMore = Mathf.Abs(deltaX) > Mathf.Abs(deltaY) ? "true" : "false";
            string DelayColor = Mathf.Abs(deltaX) > Mathf.Abs(deltaY) ? "red" : "blue";

            if (_visibleEnemyDeltaPosition)
            {
                var DeltaXDebug = Mathf.Abs(deltaX);
                var DeltaYDebug = Mathf.Abs(deltaY);
                print($"" +
                       $" Delaye debug " +
                       $"( <color=red> DelayX {DeltaXDebug} </color> - <color=blue> DelayY {DeltaYDebug} </color>) - " +
                       $"( DelayX > DelayY <color={DelayColor}> {DelayMore} </color> )");
            }
            else
            {
                var DeltaXDebug = "null";
                var DeltaYDebug = "null";

                print($"" +
                       $" Delaye debug " +
                       $"( <color=red> DelayX {DeltaXDebug} </color> - <color=blue> DelayY {DeltaYDebug} </color>) - " +
                       $"( DelayX > DelayY <color={DelayColor}> {DelayMore} </color> )");
            }
        }
    }

    private void Flip(Transform target, EnemyPreferences enemyData)
    {
        float deltaX = _enemyTargetPoint.position.x - target.position.x;
        float deltaY = _enemyTargetPoint.position.y - target.position.y;

        PrintDebug(deltaX, deltaY);

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                _body.sprite = enemyData.BodyRightSprite;
                _head.sprite = enemyData.HeadRightSprite;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _leftHand.sortingOrder = -20;
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
        else if (Mathf.Abs(deltaX) < Mathf.Abs(deltaY))
        {
            if (deltaY > 0)
            {
                _body.sprite = enemyData.BodyBottomSprite;
                _head.sprite = enemyData.HeadBottomSprite;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _leftHand.sortingOrder = 20;
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
    }
}
