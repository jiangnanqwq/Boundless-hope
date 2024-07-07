using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundCtr : MonoBehaviour
{

    public AudioSource groundMusic; 
    public AudioSource walkSound;
    public AudioClip[] musicClips;      // ��ѡ�ı������ּ����б�
    private int currentClipIndexcuan = 0;   // ��ǰ���ŵ����ּ������� 
    private bool isWalking = false;
    void Start()
    {
        if (groundMusic == null)
        {
            groundMusic = GetComponent<AudioSource>();
        }
    }
    public void OpenWalkAudio()
    {
        if (isWalking == false)
        {
            isWalking = true;
            walkSound.Play();
        }
    }
    public void CloseWalkAudio()
    {
        if (isWalking == true)
        {             
            isWalking = false;
            walkSound.Stop();
        }
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

        currentClipIndexcuan = clipIndex;
        groundMusic.clip = musicClips[clipIndex];
        groundMusic.Play();
    }

}
