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

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
        soundTrack = audioSources[0];
        jumpSoundEffect = audioSources[1];
        shieldBreakSoundEffect = audioSources[2];
        gameOverSoundEffect = audioSources[3];
        boostSoundEffect = audioSources[4];
        activateShieldSoundEffect = audioSources[5];
        getPointsSoundEffect = audioSources[6];
    }
    void Start()
    {
        soundTrack.loop = true;
    }

    private void playSoundEffect(AudioSource audioSource)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }
    public void playSoundTrack()
    {
        if(soundTrack && !soundTrack.isPlaying)
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
    public void resetSoundTrack()
    {
        if (soundTrack)
        {
            soundTrack.time = 0;
        }
    }
    public void playJumpSound()
    {
        playSoundEffect(jumpSoundEffect);
    }
    public void playBreakShieldSound()
    {
        playSoundEffect(shieldBreakSoundEffect);
    }
    public void playGameOverSound ()
    {
        playSoundEffect(gameOverSoundEffect);
    }
    public void playBoostSound()
    {
        playSoundEffect(boostSoundEffect);
    }
    public void playShieldSound()
    {
        playSoundEffect(activateShieldSoundEffect);
    }
    public void playPointsSound()
    {
        playSoundEffect(getPointsSoundEffect);
    }
}