using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    [Header("RayPoint")]
    [SerializeField] private List<Transform> _rayPointList;
    [SerializeField] private List<Vector3> _rayPointDirection;
    [SerializeField] private LayerMask _enemyBarrieLayerMask;

    public void Start()
    {
    }


    public void MoveToTarget(Transform target, float speed, float stopDistance)
    {
        for (int i = 0; i < _rayPointList.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(_rayPointList[i].position, _rayPointDirection[i], 0.3f, _enemyBarrieLayerMask);

            


            if (hit.collider == null )
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
            }
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
