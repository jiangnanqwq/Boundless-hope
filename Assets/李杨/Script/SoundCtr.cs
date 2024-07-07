using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtr : MonoBehaviour
{
    public AudioSource backgroundMusic; // �������ֵ�AudioSource���
    public AudioClip[] musicClips;      // ��ѡ�ı������ּ����б�
    private int currentClipIndex = 0;   // ��ǰ���ŵ����ּ�������
    private void Awake()
    {
        // ȷ���ڳ����л�ʱ�������ٴ˶���
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

    // ����ָ�������ı�������
    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length)
        {
            Debug.LogWarning("���ּ�������������Χ");
            return;
        }

        currentClipIndex = clipIndex;
        backgroundMusic.clip = musicClips[clipIndex];
        backgroundMusic.Play();
    }

    // ��ͣ��ǰ���ŵı�������
    public void PauseMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    // �ָ����ű�������
    public void ResumeMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.UnPause();
        }
    }

    // ֹͣ���ű�������
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    // �����������ֵ�����
    public void SetVolume(float volume)
    {
        backgroundMusic.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    // ������һ������
    public void PlayNextMusic()
    {
        currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
        PlayMusic(currentClipIndex);
    }

    // ������һ������
    public void PlayPreviousMusic()
    {
        currentClipIndex = (currentClipIndex - 1 + musicClips.Length) % musicClips.Length;
        PlayMusic(currentClipIndex);
    }
}
