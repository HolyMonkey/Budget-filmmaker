using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class LevelScreen : MonoBehaviour
{
    [SerializeField] private string _appearAnimationTrigger;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(_appearAnimationTrigger);
    }
}
