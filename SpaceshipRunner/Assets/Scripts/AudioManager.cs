using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private SoundClip[] _soundClips;

    private Dictionary<SoundType, AudioClip> _sfx = new Dictionary<SoundType, AudioClip>();

    protected override void Awake()
    {
        base.Awake();
        SoundClipToDict();
    }

    private void Start()
    {
        _musicSource.clip = _sfx[SoundType.Music];
        _musicSource.Play();
    }

    public void PlaySfx(SoundType type)
    {
        _sfxSource.PlayOneShot(_sfx[type], 1f);
    }

    private void SoundClipToDict()
    {
        foreach(var clip in _soundClips)
        {
            if(!_sfx.ContainsKey(clip.Type))
            {
                _sfx.Add(clip.Type, clip.AudioClip);
            }
        }
    }

}

[System.Serializable]
public struct SoundClip
{
    public SoundType Type;
    public AudioClip AudioClip;
}

public enum SoundType
{
    Jump,
    Flip,
    Click,
    Death,
    Music
}