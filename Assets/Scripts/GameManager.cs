using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseGame;
    public GameObject settingGame;

    public void PauseBtn()
    {
        pauseGame.SetActive(true);
        AudioManager.instance.PauseMusic();
        Time.timeScale = 0;
    }

    public void HomeBtn()
    {
        SceneManager.LoadSceneAsync("Menu");
        pauseGame.SetActive(false);
        Time.timeScale = 1;
    }

    public void SettingBtn()
    {
        pauseGame.SetActive(false);
        settingGame.SetActive(true);
    }

    public void ResumeBtn()
    {
        pauseGame.SetActive(false);
        Time.timeScale = 1;
        AudioManager.instance.UnpauseMusic();
    }

    public void BackToPausePanel ()
    {
        settingGame.SetActive(false);   
    }
}
