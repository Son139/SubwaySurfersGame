using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    Transform Player;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        scoreText.text = Player.transform.position.z.ToString("0000000");
        bestScoreText.text = PlayerPrefs.GetFloat("highScore", 0).ToString("0000000");
    }

    public void ReplayBtn()
    {
        SceneManager.LoadScene(1);
    }
}
