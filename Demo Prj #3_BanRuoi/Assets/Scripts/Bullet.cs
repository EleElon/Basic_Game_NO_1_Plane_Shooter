using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveScale;

    public float timeToDestroy;

    private bool isPaused = false;

    float timeToDestroyReset;

    Rigidbody2D m_rb;

    GameController m_gc;

    public GameObject hitVFX;

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

        //if (timeToDestroy > 0f)
        //{
        //    timeToDestroy -= Time.deltaTime;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (m_gc.IsGameOver() || m_ui.IsGamePause())
        {
            m_rb.velocity = Vector2.zero;
            return;
        }
        m_rb.velocity = Vector2.up * moveScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (hitVFX)
            {
                GameObject vfxInstantiate = Instantiate(hitVFX, collision.transform.position, Quaternion.identity);
                Destroy(vfxInstantiate, timeToDestroy);
            }
            m_gc.ScoreIncrement();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
