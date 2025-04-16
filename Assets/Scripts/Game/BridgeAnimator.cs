using UnityEngine;

public class BridgeAnimator : MonoBehaviour
{
    private const string Opened = nameof(Opened);
    private const string Closed = nameof(Closed);

    [SerializeField] private Animator _animator;

    private bool _isClose = true;

    public void Open()
    {
        _animator.SetTrigger(Opened);
    }

    public void Close()
    {
        if (_isClose)
            _animator.SetTrigger(Closed);

        _isClose = true;
    }

    public void CloseOnRound()
    {
        _isClose = false;
        _animator.SetTrigger(Closed);
    }
}