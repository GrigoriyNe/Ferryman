using UnityEngine;

public class WindowBlind : MonoBehaviour
{
    private const string Opened = nameof(Opened);
    private const string Closed = nameof(Closed);

    [SerializeField] private Animator _animator;

    public void Open()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(Opened);
    }

    public void Close()
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(Closed);
    }
}