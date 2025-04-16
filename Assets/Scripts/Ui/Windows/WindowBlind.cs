using UnityEngine;

public class WindowBlind : MonoBehaviour
{
    private const string Opened = nameof(Opened);
    private const string Closed = nameof(Closed);

    [SerializeField] private Animator _animator;
    [SerializeField] private Soungs _sougs;

    public void Open()
    {
        _animator.SetTrigger(Opened);
        _sougs.PlayGarageSoung();
    }

    public void Close()
    {
        _animator.SetTrigger(Closed);
        _sougs.PlayGarageSoung();
    }
}