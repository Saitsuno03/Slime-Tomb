using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Player player;

    public void PlayBackgroundMusic()
    {
        if(player.healthplayer <= 0)
        {
            backgroundMusic.Stop();
        }
        backgroundMusic.Play();
    }
}

