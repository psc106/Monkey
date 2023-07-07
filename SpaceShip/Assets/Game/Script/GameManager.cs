using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverUI = default;
    public GameObject playScoreText = default;
    public GameObject recordText = default;
    public GameObject scoreText = default;

    public GameObject[] button = default;

    public Player player = default;

    private bool isGameOver = default;
    public static int score = default;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            playScoreText.SetText(string.Format("Score : {0}", score));
            if (player.hp == 4)
            {
                if (!button[0].activeInHierarchy)
                {
                    button[0].SetActive(true);
                    button[1].SetActive(true);
                    button[2].SetActive(true);
                    button[3].SetActive(true);
                }
            }
            else if (player.hp == 3)
            {
                if (button[3].activeInHierarchy)
                {
                    button[3].SetActive(false);
                }
            }
            else if (player.hp == 2)
            {
                if (button[2].activeInHierarchy)
                {
                    button[3].SetActive(false);
                    button[2].SetActive(false);
                }
            }
            else if (player.hp == 1)
            {
                if (button[1].activeInHierarchy)
                {
                    button[3].SetActive(false);
                    button[2].SetActive(false);
                    button[1].SetActive(false);
                }
            }
            else if (player.hp == 0)
            {
                if (button[0].activeInHierarchy)
                {
                    button[3].SetActive(false);
                    button[2].SetActive(false);
                    button[1].SetActive(false);
                    button[0].SetActive(false);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GFunc.LoadScene("PlayScene");
            }
        }
    }

    public void EndGame()
    {
        isGameOver = true;
        gameoverUI.SetActive(true);
        if (button[0].activeInHierarchy)
        {
            button[3].SetActive(false);
            button[2].SetActive(false);
            button[1].SetActive(false);
            button[0].SetActive(false);
        }

        float bestScore = PlayerPrefs.GetFloat("BestScore");
        if (bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }

        scoreText.SetText(string.Format("score : {0}", score));
        recordText.SetText(string.Format("Record : {0}", bestScore));

    }
}
