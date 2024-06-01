using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public GameObject enemy, background;

    public float spawnTime;

    public float spawnBGTime;

    float m_spawnTime;

    float M_spawnBGTime;

    int m_score;

    int n, m;

    bool m_isGameOver;

    UIManager m_ui;

    int HP = 3;

    Player user;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnTime = 0;
        m_ui = FindAnyObjectByType<UIManager>();
        user = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        string HPString = string.Empty;
        for (int i = 0; i < HP; i++)
        {
            HPString += "❤";
        }

        m_ui.SetHealthText(HPString);

        if (HP == 0)
        {
            m_isGameOver = true;

            if (user)
            {
                Destroy(user.gameObject);
            }
            else
            {
                return;
            }
        }

        if (m_ui.IsGamePause())
        {
            n++;
            return;
        }

        if (m_isGameOver)
        {
            m_spawnTime = 0;
            m_ui.ShowGameOverPanel(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_ui.ShowGamePausePanel(true);
        }

        m_spawnTime -= Time.deltaTime;
        M_spawnBGTime -= Time.deltaTime;
        //if (n % 10 == 0)
        //{
        //    spawnTime -= 1;
        //    n = m;
        //}
        if (m_spawnTime <= 0)
        {
            Spawn();
            m_spawnTime = spawnTime;

        }
        if (M_spawnBGTime <= 0)
        {
            if (n==0)
            {
                SpawnBackGround();
                M_spawnBGTime = spawnBGTime;
            }
            else
            {
                SpawnBackGroundAfterPause();
                M_spawnBGTime = spawnBGTime;
                n--;
            }
        }

    }

    public void Continue()
    {

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Spawn()
    {
        if (m_ui.IsGamePause() || m_isGameOver)
            return;

        float randPosiX = Random.Range(-7f, 7f);
        Vector2 spawnPosi = new Vector2(randPosiX, 6);
        if (enemy)
        {
            Instantiate(enemy, spawnPosi, Quaternion.identity);
            //n++;
        }
    }

    public void SpawnBackGround()
    {
        if (m_ui.IsGamePause() || m_isGameOver)
            return;

        Vector2 SpawnBG = new Vector2(0, 27);

        if (background)
        {
            Instantiate(background, SpawnBG, Quaternion.identity);
        }
    }

    public void SpawnBackGroundAfterPause()
    {
        Vector2 SpawnBG = new Vector2(0, 16);

        if (background)
        {
            Instantiate(background, SpawnBG, Quaternion.identity);
        }
    }
    public void SetScore(int value)
    {
        m_score = value;
    }

    public int GetScore()
    {
        return m_score;
    }

    public void ScoreIncrement()
    {
        if (m_isGameOver || m_ui.IsGamePause())
            return;
        m_score++;
        m_ui.SetScoreText("Score: " + m_score);
    }

    public void HealthPointDecre()
    {
        HP--;
        string HPString = string.Empty;
        for (int i = 0; i < HP; i++)
        {
            HPString += "❤";
        }

        m_ui.SetHealthText(HPString);
    }
    public bool IsGameOver()
    {
        return m_isGameOver;
    }

    public void SetGameOver(bool State)
    {
        m_isGameOver = State;
    }
}
