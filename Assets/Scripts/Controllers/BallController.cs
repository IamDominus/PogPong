using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector2 _direction;
    private Vector2 _currentSpeed;

    public GameObject LeftPlayer;
    public GameObject RightPlayer;
    public bool LeftPlayerLastTouchedTheBall { get; private set; }
    public bool _isFirstFixedUpdate = true;

    public event System.Action<bool> PlayerScored = delegate (bool leftPlayerScored) { };
    public event System.Action BallHitPlayer = delegate () { };
    private void Start()
    {
        RsetBall();
        StartMovement();
    }

    private void Update()
    {
        if (transform.position.x < LeftPlayer.transform.position.x)
        {
            PlayerScored(false);
            RsetBall();
        }
        else if (transform.position.x > RightPlayer.transform.position.x)
        {
            PlayerScored(true);
            RsetBall();
        }
    }
    private void RsetBall()
    {
        StopAllCoroutines();
        _currentSpeed = Constants.DEFAULT_BALL_SPEED;
        transform.position = Vector3.zero;
        RandomizeDirection();
    }
    public void RandomizeDirection()
    {
        var leftPlayerStartTheGame = Random.Range(0, 2) == 1;
        float yDirection = Random.Range(-0.5f, 0.5f);

        if (leftPlayerStartTheGame)
        {
            _direction = new Vector2(-1f, yDirection).normalized;
        }
        else
        {
            _direction = new Vector2(1f, yDirection).normalized;
        }
    }
    private void FixedUpdate()
    {
        if (_isFirstFixedUpdate)
        {
            _isFirstFixedUpdate = false;
            return;
        }
        //transform.gameObject.GetComponent<Rigidbody>().MovePosition(LeftPlayer.transform.position);
        //transform.gameObject.GetComponent<Rigidbody>().AddForce();
        //transform.gameObject.GetComponent<Rigidbody>().velocity;
        transform.position += Vector3.Scale(_direction, _currentSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        var contactNormal = collision.contacts[0].normal;

        Debug.DrawLine(collision.contacts[0].point, contactNormal * 100, Color.red, 120f);
        if (collision.gameObject.CompareTag("Player"))
        {
            LeftPlayerLastTouchedTheBall = GameObject.ReferenceEquals(collision.gameObject, LeftPlayer) ? true : false;

            if (collision.contacts[0].point.y > collision.gameObject.transform.position.y)
            {
                _direction.y = Mathf.Abs(_direction.y);
            }
            else
            {
                _direction.y = Mathf.Abs(_direction.y) * -1;

            }
            if (contactNormal.x != 0 && Mathf.Sign(contactNormal.x) != Mathf.Sign(_direction.x))
            {
                _direction.x *= -1;
            }

            _currentSpeed *= 1.06f;
            BallHitPlayer();
        }
        else
        {
            if (Mathf.Round(contactNormal.x * 100f) / 100f != 0 && Mathf.Sign(contactNormal.x) != Mathf.Sign(_direction.x))
            {
                _direction.x *= -1;
            }
            if (Mathf.Round(contactNormal.y * 100f) / 100f != 0 && Mathf.Sign(contactNormal.y) != Mathf.Sign(_direction.y))
            {
                _direction.y *= -1;
            }
        }

    }
    public void StartMovement()
    {
        _currentSpeed = Constants.DEFAULT_BALL_SPEED;
    }
    public void StopMovement()
    {
        _currentSpeed = Vector2.zero;
    }
    public void ChangeSpeed(float speedMultiplier, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speedMultiplier, duration));
    }
    private IEnumerator ChangeSpeedCoroutine(float speedMultiplier, float duration)
    {

        _currentSpeed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        _currentSpeed /= speedMultiplier;
    }
}
