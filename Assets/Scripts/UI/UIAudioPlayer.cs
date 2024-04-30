using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/UIAudioPlayer")]
public class UIAudioPlayer : ScriptableObject
{
    [SerializeField] private AudioClip clickAudionClip;
    [SerializeField] private AudioClip commitAudionClip;
    [SerializeField] private AudioClip selectAudionClip;

    private void PlayAudio(AudioClip audionClipToPlay)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(audionClipToPlay);
    }

    public void PlayClick()
    {
        PlayAudio(clickAudionClip);
    }
    
    public void PlayCommit()
    {
        PlayAudio(commitAudionClip);
    }
    
    public void PlaySelect()
    {
        PlayAudio(selectAudionClip);
    }

}
