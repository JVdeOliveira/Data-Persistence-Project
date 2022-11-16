using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_xLimit;

    private bool m_canMove = true;

    private void Start()
    {
        if (GameController.Instance)
            GameController.Instance.OnLose += Instance_OnLose;
    }

    private void Instance_OnLose()
    {
        m_canMove = false;
    }

    private void Update()
    {
        Movement();
        LimitXPosition();
    }

    private void Movement()
    {
        if (!m_canMove) return;

        var horizontal = Input.GetAxis("Horizontal");

        transform.Translate(m_speed * horizontal * Time.deltaTime * Vector2.right);
    }

    private void LimitXPosition()
    {
        var xPos = transform.position.x;

        xPos = Mathf.Clamp(xPos, -m_xLimit, m_xLimit);

        transform.position = new(xPos, transform.position.y, 0);
    }
}
