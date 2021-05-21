using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResultsHandler : MonoBehaviour
{
    [SerializeField] private ActionsDemonstrator _actionsDemonstrator;
    [SerializeField] private LevelFailScreen _failScreen;
    [SerializeField] private LevelWinScreen _winScreen;

    private void OnEnable()
    {
        _actionsDemonstrator.AllActionsCompleted += OnAllActionsCompleted;
    }

    private void OnDisable()
    {
        _actionsDemonstrator.AllActionsCompleted -= OnAllActionsCompleted;
    }

    private void OnAllActionsCompleted(bool isLevelCompleted)
    {
        if (isLevelCompleted)
        {
            _winScreen.Show();
        }
        else
        {
            _failScreen.Show();
        }
    }
}
