using System.Collections;
using UnityEngine;

public class Ferryboat : MonoBehaviour
{
    [SerializeField] private FerryboatAnimator _animator;

    public void Activate()
    {
        _animator.PlayStart();
    }

    public void Finish()
    {
        _animator.PlayFinish();
    }
}
