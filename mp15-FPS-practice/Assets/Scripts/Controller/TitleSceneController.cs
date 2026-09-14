using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleSceneController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable()
    {
        BindButtonEvents();
    }

    private void OnDisable()
    {
        UnBindButtonEvents();
    }

    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(LoadGameScene);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void UnBindButtonEvents()
    {
        _startButton.onClick.RemoveListener(LoadGameScene);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
