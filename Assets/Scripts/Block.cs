using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] Color m_color;

    [SerializeField] int m_life;
    [SerializeField] int m_score;

    private void Awake()
    {
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_spriteRenderer.color = m_color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            LoseLife();
        }
    }

    private void LoseLife()
    {
        m_life--;

        if (m_life <= 0)
        {
            if (GameController.Instance) 
                GameController.Instance.IncreaseScore(m_score);

            Destroy(gameObject);
        }
    }
}
