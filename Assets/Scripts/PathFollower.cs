using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform[] path;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Mode mode = Mode.BY_ORDER;

    private int targetIndex = 0;

    private void Update()
    {
        CheckTarget();
        Move();
    }

    private void CheckTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, path[targetIndex].position);
        // check if we arrived at the target
        if (distanceToTarget < 0.001f)
        {
            // set new target
            switch (mode)
            {
                case Mode.BY_ORDER:
                    targetIndex++;
                    targetIndex %= path.Length; // make sure the target index never goes out of bounds
                    break;
                case Mode.RANDOM:
                    targetIndex = Random.Range(0, path.Length);
                    break;
            }
        }
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        // move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, path[targetIndex].position, step);
    }

    private enum Mode
    {
        RANDOM, BY_ORDER
    }
}
