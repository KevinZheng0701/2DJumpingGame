using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource[] audioSources;
    private AudioSource soundTrack;
    private AudioSource jumpSoundEffect;
    private AudioSource shieldBreakSoundEffect;
    private AudioSource gameOverSoundEffect;
    private AudioSource boostSoundEffect;
    private AudioSource activateShieldSoundEffect;
    private AudioSource getPointsSoundEffect;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        soundTrack = audioSources[0];
        jumpSoundEffect = audioSources[1];
        shieldBreakSoundEffect = audioSources[2];
        gameOverSoundEffect = audioSources[3];
        boostSoundEffect = audioSources[4];
        activateShieldSoundEffect = audioSources[5];
        getPointsSoundEffect = audioSources[6];
        soundTrack.loop = true;
    }

    private void PlaySoundEffect(AudioSource audioSource)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }

    public void PlaySoundTrack()
    {
        if (soundTrack && !soundTrack.isPlaying)
        {
            soundTrack.Play();
        }
    }

    public void stopSoundTrack()
    {
        if (soundTrack && soundTrack.isPlaying)
        {
            soundTrack.Stop();
        }
    }

    public void ResetSoundTrack()
    {
        if (soundTrack)
        {
            soundTrack.time = 0;
        }
    }

    public void PlayJumpSound()
    {
        PlaySoundEffect(jumpSoundEffect);
    }

    public void PlayBreakShieldSound()
    {
        PlaySoundEffect(shieldBreakSoundEffect);
    }

    public void PlayGameOverSound()
    {
        PlaySoundEffect(gameOverSoundEffect);
    }

    public void PlayBoostSound()
    {
        PlaySoundEffect(boostSoundEffect);
    }

    public void PlayShieldSound()
    {
        PlaySoundEffect(activateShieldSoundEffect);
    }

    public void PlayPointsSound()
    {
        PlaySoundEffect(getPointsSoundEffect);
    }
}