using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public static EnemyMoving Instance { get; private set; }

    public bool IsMoving;

    public GameObject rayTest;

    public Transform RayUp;
    public Transform RayDown;

    public float up;
    public float down;

    public bool search;

    Vector3 direction = Vector3.right.normalized;

    public void Start()
    {
    }


    public void MoveToTarget(Transform target, float speed, float stopDistance)
    {
        if (target == null) return;

        RaycastHit2D hit = Physics2D.Raycast(rayTest.transform.position, rayTest.transform.right, 0.5f);
        RaycastHit2D hitUp = Physics2D.Raycast(RayUp.position, RayUp.right, 5f);
        RaycastHit2D hitDown = Physics2D.Raycast(RayDown.position, RayDown.right, 5f);

        Debug.DrawRay(rayTest.transform.position, rayTest.transform.right, Color.yellow , 0.5f);

        if (hit.collider != null && search == false)
        {
            Debug.DrawRay(RayUp.position, RayUp.right, Color.red, 5);
            Debug.DrawRay(RayDown.position, RayDown.right, Color.red, 5);

            if (search == false)
            {
                if (hitUp.collider != null)
                {
                    RayUp.rotation = Quaternion.Euler(0, 0, up);

                    up += 50f * Time.deltaTime;
                }
                else
                {
                    search = true;
                    direction = Vector3.up.normalized;
                }
            }

            if (search == false)
            {
                if (hitDown.collider != null)
                {
                    RayDown.rotation = Quaternion.Euler(0, 0, down);

                    down -= 50f * Time.deltaTime;
                }
                else
                {
                    search = true;
                    direction = Vector3.down.normalized;
                }
            }
        }
        else
        {
            transform.position += direction * speed * Time.deltaTime; ;
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
