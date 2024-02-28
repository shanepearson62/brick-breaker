using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    public Vector2 direction { get; private set; }
    public int speed = 30;
    public int maxBounceAngle = 75;

    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rigidBody.AddForce(this.direction * this.speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Ball ball = other.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePos = this.transform.position;
            Vector2 contactPoint = other.GetContact(0).point;

            float offset = paddlePos.x - contactPoint.x;
            float width = other.otherCollider.bounds.size.x * 0.5f;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidBody.velocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;

            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidBody.velocity = rotation * Vector2.up * ball.rigidBody.velocity.magnitude;
        }
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidBody.velocity = Vector2.zero;
    }
}
