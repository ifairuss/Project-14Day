using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;
    

    public void MoveToTarget(Transform target, float speed, float stopDistance)
    {
        if (target == null) return;

        if (Vector2.Distance(transform.position, target.position) > stopDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

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
