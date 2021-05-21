using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinScreen : LevelScreen
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private string _nextLevelSceneName;

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(RestartLevel);
    }

    private void RestartLevel()
    {
        if (_nextLevelSceneName != string.Empty)
        {
            SceneManager.LoadScene(_nextLevelSceneName);
        }
    }
}
