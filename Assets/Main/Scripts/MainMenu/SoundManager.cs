using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicPlayer;
    [SerializeField] private AudioSource _sfxPlayer;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _mainMenuClip;
    [SerializeField] private AudioClip _gameLoopClip;

    [SerializeField] private AudioClip _cardOpenClip;
    [SerializeField] private AudioClip _levelComplateClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void SetMusicAudioSourceSoundVolume(float value)
    {
        _musicPlayer.volume = value;
    }
    public float GetMusicAudioSourceSoundVolume()
    {
        return _musicPlayer.volume;
    }
    
    public void SetSfxAudioSourceSoundVolume(float value)
    {
        _sfxPlayer.volume = value;
    }
    public float GetSfxAudioSourceSoundVolume()
    {
        return _sfxPlayer.volume;
    }
    
    public void PlayMenuMusic()
    {
        if (_musicPlayer != null)
        {
            _musicPlayer.clip = _mainMenuClip;
            _musicPlayer.loop = true;
            _musicPlayer.Play();
        }
    }
    public void PlayGameLoopMusic()
    {
        if (_musicPlayer != null)
        {
            _musicPlayer.clip = _gameLoopClip;
            _musicPlayer.loop = true;
            _musicPlayer.Play();
        }
    }

    public void PlayCardOpenSfx()
    {
        if (_sfxPlayer != null)
            _sfxPlayer.PlayOneShot(_cardOpenClip);
    }    
    public void LevelComplateSfx()
    {
        if (_sfxPlayer != null)
            _sfxPlayer.PlayOneShot(_levelComplateClip);
    }
}
