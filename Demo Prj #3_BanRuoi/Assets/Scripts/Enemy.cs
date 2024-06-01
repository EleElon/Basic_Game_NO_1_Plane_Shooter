using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveScale;

    Rigidbody2D m_rb;

    GameController m_gc;

    public AudioManager aum;

    UIManager m_ui;

    private void Awake()
    {
        aum = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_gc = FindAnyObjectByType<GameController>();
        m_ui = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (m_ui != null && m_ui.gamePausePanel.activeInHierarchy)
        //{
        //    return;
        //}

        if (m_gc.IsGameOver() || m_ui.IsGamePause())
        {
            m_rb.velocity = Vector2.zero;
            return;
        }
        m_rb.velocity = Vector2.down * moveScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathZone"))
        {
            if (aum && aum.loseSound)
            {
                aum.playSFX(aum.loseSound);
            }
            Destroy(gameObject);
            m_gc.HealthPointDecre();
            //m_gc.SetGameOver(true);
        }
    }
}
