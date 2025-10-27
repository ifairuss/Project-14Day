using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    private NavMeshAgent _agent;

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    public void MoveToTarget(Transform target, float speed)
    {
        if (target == null) return;

        _agent.SetDestination(target.position);

        if (Vector3.Distance(target.position, transform.position) <= (_agent.stoppingDistance + 0.2f))
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
        }

        Flip(target);
    }

    private void Flip(Transform target)
    {
        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
