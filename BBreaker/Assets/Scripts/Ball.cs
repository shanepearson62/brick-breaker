using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
    public int speed = 10;

    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }


    private void Update()
    {
        //Debug.Log(rigidBody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        if (rigidBody.velocity.sqrMagnitude != speed * speed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speed;
        }
    }

    private void SetTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidBody.AddForce(force.normalized * this.speed);
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidBody.velocity = Vector2.zero;
        Invoke(nameof(SetTrajectory), 1f);
    }
}
