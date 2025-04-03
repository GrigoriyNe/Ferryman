﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        for (int i = 0; i < _audioClips.Count; i++)
        {
            _audioSource.PlayOneShot(_audioClips[i]);

            while (_audioSource.isPlaying)
                yield return null;

            if (i == _audioClips.Count - 1)
                i = 0;
        }
    }
}
