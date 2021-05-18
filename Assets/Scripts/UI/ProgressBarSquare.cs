using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarSquare : MonoBehaviour
{
    [SerializeField] private Image _levelNumber;

    public void Init(Sprite numberSprite)
    {
        _levelNumber.sprite = numberSprite;
    }
}
