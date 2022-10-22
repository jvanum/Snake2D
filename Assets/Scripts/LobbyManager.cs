using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonSinglePlayer;
    [SerializeField] private Button buttonDualPlayer;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonLevelsClose;
    [SerializeField] private Button buttonHelpClose;

    [SerializeField] private GameObject LobbyScreen;
    [SerializeField] private GameObject LevelScreen;
    [SerializeField] private GameObject HelpScreen;
    private void Awake()
    {
        buttonPlay.onClick.AddListener(ChooseLevels);
        buttonSinglePlayer.onClick.AddListener(StartSinglePlayer);
        buttonDualPlayer.onClick.AddListener(StartDualPlayer);
        buttonHelp.onClick.AddListener(Help);
        buttonQuit.onClick.AddListener(QuitGame);
        buttonLevelsClose.onClick.AddListener(CloseLevelPanel);
        buttonHelpClose.onClick.AddListener(CloseHelpPanel);
    }

    private void StartSinglePlayer()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneManager.LoadScene(1);
    }
    private void StartDualPlayer()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        SceneManager.LoadScene(2);
    }

    private void CloseHelpPanel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        LobbyScreen.SetActive(true);
        HelpScreen.SetActive(false);
    }

    private void CloseLevelPanel()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        LobbyScreen.SetActive(true);
        LevelScreen.SetActive(false);
    }

    private void QuitGame()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        UnityEditor.EditorApplication.ExitPlaymode();
        Application.Quit();
        Debug.Log("Application quit");
    }

    private void Help()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        HelpScreen.SetActive(true);
        LobbyScreen.SetActive(false);

    }

    private void ChooseLevels()
    {
        SoundManager.Instance.Play(SoundTypes.BUTTONCLICK);
        LevelScreen.SetActive(true);
        LobbyScreen.SetActive(false);

    }
}