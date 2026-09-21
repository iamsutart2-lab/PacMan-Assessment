using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Background Music")]
    public AudioClip introMusic;
    public AudioClip normalGhostMusic;

    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        
        if (introMusic != null && normalGhostMusic != null)
        {
            StartCoroutine(PlayMusicSequence());
        }
        else
        {
            Debug.LogWarning("Please assign both audio clips in the Inspector.");
        }
    }

    private IEnumerator PlayMusicSequence()
    {
        // 1. Play the intro background music
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();

        // 2. Calculate wait time: whichever is shorter between 3 seconds and the clip's total length
        float waitTime = Mathf.Min(introMusic.length, 3.0f);
        
        // Wait for that duration
        yield return new WaitForSeconds(waitTime);

        // 3. Switch to the normal ghost state music and loop it
        audioSource.clip = normalGhostMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}