using UnityEngine;
using UnityEngine.AI;

public class EnemyMoving : MonoBehaviour
{
    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    public void Start()
    {
    }


    public void MoveToTarget(Transform target, float speed, float stopDistance)
    {
        if (target == null) return;

        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {

            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }

        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
