using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPlatformController : PlatformController
{
    [SerializeField] private float _minDistanceToReact;
    [SerializeField] private Transform _ball;

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(_ball.position, transform.position);

        if (distance < _minDistanceToReact)
        {
            if (_ball.position.y > transform.position.y && Mathf.Abs(_ball.position.y - transform.position.y) > 0.1f)
            {
                MoveUp();
            }
            else if (_ball.position.y < transform.position.y && Mathf.Abs(_ball.position.y - transform.position.y) > 0.1f)
            {
                MoveDown();
            }
        }
        else
        {
            if (transform.position.y > 0.1f)
            {
                MoveDown();
            }
            else if (transform.position.y < -0.1f)
            {
                MoveUp();
            }
        }
    }
}
