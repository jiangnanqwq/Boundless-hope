using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundCtr : MonoBehaviour
{

    public AudioSource groundMusic; 
    public AudioClip[] musicClips;      // 可选的背景音乐剪辑列表
    private int currentClipIndex = 0;   // 当前播放的音乐剪辑索引 
    void Start()
    {
        if (groundMusic == null)
        {
            groundMusic = GetComponent<AudioSource>();
        }
    }
    public void PlayeMoveAudio()
    {
        PlayMusic(0);
    }
    public void PlayerShootAudio()
    {
        PlayMusic(1);
    }
    public void PlayerCutAudio()
    {
        PlayMusic(2);
    }


    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length)
        {
            Debug.LogWarning("音乐剪辑索引超出范围");
            return;
        }

        currentClipIndex = clipIndex;
        groundMusic.clip = musicClips[clipIndex];
        groundMusic.Play();
    }

}
