using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveScale;

    public GameObject Bullet;

    public Transform shootingLocation;

    GameController m_gc;

    public AudioManager aum;

    UIManager m_ui;

    int bulletInBag = 10;

    public float timeToMoreBullets;

    float m_timeToMoreBullets;

    private void Awake()
    {
        aum = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_gc = FindAnyObjectByType<GameController>();
        m_ui = FindAnyObjectByType<UIManager>();
        string bulletsString = string.Empty;
        for (int i = 0; i < 10; i++)
        {
            bulletsString += "|=>";
        }

        m_ui.SetBulletsText(bulletsString);
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletInBag < 10)
        {
            if (m_ui.IsGamePause() || m_gc.IsGameOver())
                return;

            m_timeToMoreBullets -= Time.deltaTime;

            if (m_timeToMoreBullets <= 0)
            {
                bulletInBag++;
                m_timeToMoreBullets = timeToMoreBullets;
            }
        }
        else
        {
            bulletInBag = 10;
        }

        string bulletsString = string.Empty;
        for (int i = 0; i < bulletInBag; i++)
        {
            bulletsString += "|=>";
        }

        m_ui.SetBulletsText(bulletsString);

        if (m_gc.IsGameOver() || m_ui.IsGamePause())
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        float Posi_x = Input.GetAxisRaw("Horizontal");
        float Posi_y = Input.GetAxisRaw("Vertical");

        float moveX = moveScale * Posi_x * Time.deltaTime;
        float moveY = moveScale * Posi_y * Time.deltaTime;

        if ((transform.position.x <= -8.25 && Posi_x < 0) || (transform.position.x >= 7.5 && Posi_x > 0) || (transform.position.y >= 3.3 && Posi_y > 0) || (transform.position.y <= -0.5 && Posi_y < 0))
        {
            return;
        }
        else
        {
            transform.position = transform.position + new Vector3(moveX, moveY, 0);
        }
    }
    public void Shoot()
    {
        if (bulletInBag == 0)
        {
            return;
        }

        if (Bullet && shootingLocation)
        {
            if (m_ui != null && m_ui.gamePausePanel.activeInHierarchy)
            {
                return;
            }

            if (aum && aum.shootingSound)
            {
                aum.playSFX(aum.shootingSound);
            }
            Instantiate(Bullet, shootingLocation.position, Quaternion.identity);
            bulletInBag--;

            string bulletsString = string.Empty;
            for (int i = 0; i < bulletInBag; i++)
            {
                bulletsString += "|=>";
            }

            m_ui.SetBulletsText(bulletsString);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (aum && aum.deathSound)
            {
                aum.playSFX(aum.deathSound);
            }
            m_gc.HealthPointDecre();
            //m_gc.SetGameOver(true);
            //Destroy(m_gc);
            //Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
