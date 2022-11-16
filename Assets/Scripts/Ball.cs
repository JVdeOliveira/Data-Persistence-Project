using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    [SerializeField] private float m_startSpeed;
    [SerializeField] private float m_endSpeed;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartMove();
    }

    private void FixedUpdate()
    {
        LimitSpeed();
    }

    private void StartMove()
    {
        var direction = Vector2.down;
        m_rigidbody.AddForce(direction * m_startSpeed, ForceMode2D.Impulse);
    }

    private void LimitSpeed()
    {
        var velocity = m_rigidbody.velocity;

        velocity.x = math.clamp(velocity.x, -m_endSpeed, m_endSpeed);
        velocity.y = math.clamp(velocity.y, -m_endSpeed, m_endSpeed);

        m_rigidbody.velocity = velocity;
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        m_rigidbody.velocity = Vector2.zero;
    }
}
