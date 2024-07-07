using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtr : MonoBehaviour
{
    public AudioSource backgroundMusic; // 背景音乐的AudioSource组件
    public AudioClip[] musicClips;      // 可选的背景音乐剪辑列表
    private int currentClipIndex = 0;   // 当前播放的音乐剪辑索引
    private void Awake()
    {
        // 确保在场景切换时不会销毁此对象
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = GetComponent<AudioSource>();
        }

        if (musicClips.Length > 0)
        {
            PlayMusic(currentClipIndex);
        }
    }

    // 播放指定索引的背景音乐
    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length)
        {
            Debug.LogWarning("音乐剪辑索引超出范围");
            return;
        }

        currentClipIndex = clipIndex;
        backgroundMusic.clip = musicClips[clipIndex];
        backgroundMusic.Play();
    }

    // 暂停当前播放的背景音乐
    public void PauseMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    // 恢复播放背景音乐
    public void ResumeMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.UnPause();
        }
    }

    // 停止播放背景音乐
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    // 调整背景音乐的音量
    public void SetVolume(float volume)
    {
        backgroundMusic.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    // 播放下一首音乐
    public void PlayNextMusic()
    {
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        PlayMusic(currentClipIndex);
    }

    // 播放上一首音乐
    public void PlayPreviousMusic()
    {
        currentClipIndex = (currentClipIndex - 1 + musicClips.Length) % musicClips.Length;
        PlayMusic(currentClipIndex);
    }
}
