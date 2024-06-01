using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float moveScale;

    public float timeToDestroy;

    float timeToDestroyReset;

    private bool isPaused = false;

    Rigidbody2D m_rb;

    GameController m_gc;

    UIManager m_ui;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindAnyObjectByType<GameController>();
        m_ui = FindAnyObjectByType<UIManager>();

        //Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_ui != null && m_ui.gamePausePanel.activeInHierarchy)
        //{
        //    return;
        //}

        if (m_ui.IsGamePause())
        {
            isPaused = true;
        }

        else
        {
            isPaused = false;
        }

        if (!isPaused /*&& Time.time >= timeToDestroy*/)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                Destroy(gameObject);
                timeToDestroy = timeToDestroyReset;
            }
        }

        if (m_gc.IsGameOver() || m_ui.IsGamePause())
        {
            m_rb.velocity = Vector2.zero;
            return;
        }
        m_rb.velocity = Vector2.down * moveScale;
    }
}
