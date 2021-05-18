using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFailScreen : LevelScreen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private string _levelSceneName;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartLevel);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(_levelSceneName);
    }
}
