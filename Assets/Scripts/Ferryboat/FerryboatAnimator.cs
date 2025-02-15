using System.Collections;
using UnityEngine;

public class FerryboatAnimator : MonoBehaviour
{
    private const string Start = nameof(Start);
    private const string Finish = nameof(Finish);

    [SerializeField] private Animator _animator;

    public void PlayStart()
    {
        _animator.SetBool(Start, true);
        StartCoroutine(ResetAnimation());
    }

    public void PlayFinish()
    {
        _animator.SetBool(Finish, true);
        StartCoroutine(ResetAnimation());
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(3f);

        _animator.SetBool(Start, false);
        _animator.SetBool(Finish, false);
    }
}
