using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;

    private void Awake()
    {
        buttonResume.onClick.AddListener(ResumeGame);
        buttonMainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void LoadMainMenu()
    {
        AudioListener.pause = false;
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void ResumeGame()
    {
        AudioListener.pause = false;
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
