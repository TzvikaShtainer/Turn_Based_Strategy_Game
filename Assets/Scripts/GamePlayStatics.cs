using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class GamePlayStatics : MonoBehaviour
{
    class AudionSrcContext : MonoBehaviour
    {
    }

    static private ObjectPool<AudioSource> AudioPool;
    private void Start()
    {
        GameStarted();
    }

    private static void GameStarted()
    {
        AudioPool = 
            new ObjectPool<AudioSource>(CreateAudioSrc, null, null, DestroyAudioSrc, false, 5, 10);
    }
    private static void DestroyAudioSrc(AudioSource audioSource)
    {
        Destroy(audioSource.gameObject);
    }

    private static AudioSource CreateAudioSrc()
    {
        GameObject audioSrcGameObject = new GameObject("AudioSrcGameObject", typeof(AudioSource), typeof(AudionSrcContext));
        AudioSource audioSource = audioSrcGameObject.GetComponent<AudioSource>();

        audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        
        return audioSource;
    }

    public static void PlayAudioAtLoc(AudioClip audioToPlay, Vector3 location, float volume)
    {
        AudioSource newAudionSrc = AudioPool.Get();

        newAudionSrc.volume = volume;
        newAudionSrc.gameObject.transform.position = location;
        
        newAudionSrc.PlayOneShot(audioToPlay);

        newAudionSrc.GetComponent<AudionSrcContext>().StartCoroutine(ReleaseAudion(newAudionSrc, audioToPlay.length));
    }

    private static IEnumerator ReleaseAudion(AudioSource newAudionSrc, float length)
    {
        yield return new WaitForSeconds(length);
        AudioPool.Release(newAudionSrc);
    }
}
