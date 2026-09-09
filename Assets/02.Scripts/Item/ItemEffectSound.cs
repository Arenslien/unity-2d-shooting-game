using System.Collections;
using UnityEngine;

public class ItemEffectSound : MonoBehaviour
{
    [SerializeField] private int _repeatCount = 1;
    private AudioSource _itemAudioSource;

    private void Awake()
    {
        _itemAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(RepeatPlayingSound(_repeatCount));
    }

    private IEnumerator RepeatPlayingSound(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _itemAudioSource.Play();

            yield return new WaitForSeconds(_itemAudioSource.clip.length);
            // yield return null;
        }
    }
}