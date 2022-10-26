using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button buttonPlayAgain;
    [SerializeField] private Button buttonMainMenu;

    private void Start()
    {
        AudioListener.pause = true;
        Time.timeScale = 0;
    }
    private void Awake()
    {
        buttonPlayAgain.onClick.AddListener(ReplayGame);
        buttonMainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        AudioListener.pause = false;
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void ReplayGame()
    {
        AudioListener.pause = false;
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
