using UnityEngine;

public class BridgeAnimator : MonoBehaviour
{
    private const string Opened = nameof(Opened);
    private const string Closed = nameof(Closed);

    [SerializeField] private Animator _animator;

    public void Open()
    {
        _animator.SetTrigger(Opened);
    }

    public void Close()
    {
        _animator.SetTrigger(Closed);
    }
}