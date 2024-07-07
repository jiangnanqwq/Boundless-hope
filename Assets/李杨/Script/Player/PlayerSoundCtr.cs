using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundCtr : MonoBehaviour
{

    public AudioSource groundMusic; 
    public AudioClip[] musicClips;      // ��ѡ�ı������ּ����б�
    private int currentClipIndex = 0;   // ��ǰ���ŵ����ּ������� 
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
            Debug.LogWarning("���ּ�������������Χ");
            return;
        }

        currentClipIndex = clipIndex;
        groundMusic.clip = musicClips[clipIndex];
        groundMusic.Play();
    }

}
