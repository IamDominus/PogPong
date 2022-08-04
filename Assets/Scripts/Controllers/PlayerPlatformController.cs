using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPlatformController : PlatformController
{
    private BoxCollider2D _playerCollider;
    private int _fingerId;

    protected override void Awake()
    {
        Input.simulateMouseWithTouches = true;
        base.Awake();
        _playerCollider = transform.gameObject.GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        //if (_isLeftPlayer)
        //{
        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        MoveUp();
        //    }
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        MoveDown();
        //    }
        //}
        //else
        //{
        //    if (Input.GetKey(KeyCode.UpArrow))
        //    {
        //        MoveUp();
        //    }
        //    if (Input.GetKey(KeyCode.DownArrow))
        //    {
        //        MoveDown();
        //    }
        //}

        Vector3 touchPosition = Vector3.zero;

        if (Input.touches.Where(t => t.fingerId == _fingerId).Count() > 0)
        {
            var touch = Input.touches.Where(t => t.fingerId == _fingerId).First();
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
        else
        {
            int tempFingerId = -1;

            foreach (var touch in Input.touches)
            {
                var tempTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                tempTouchPosition.z = transform.position.z;

                if (_playerCollider.bounds.Contains(tempTouchPosition))
                {
                    tempFingerId = touch.fingerId;
                    touchPosition = tempTouchPosition;
                    break;
                }
            }

            _fingerId = tempFingerId;
        }

        if (_fingerId == -1)
        {
            return;
        }

        if (touchPosition.y > transform.position.y && Mathf.Abs(touchPosition.y - transform.position.y) > 0.1f)
        {
            MoveUp();
        }
        else if (touchPosition.y < transform.position.y && Mathf.Abs(touchPosition.y - transform.position.y) > 0.1f)
        {
            MoveDown();
        }
    }
}
