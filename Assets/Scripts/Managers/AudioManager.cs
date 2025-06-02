using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void playSound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void stopSound(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void FadeIn(
        string soundName,
        float duration,
        float targetVolumeOverride = -1f,
        System.Action onComplete = null
    )
    {
        Sound targetSound = System.Array.Find(sounds, s => s.name == soundName);
        if (targetSound == null)
        {
            Debug.LogWarning("AudioManager: Sound '" + soundName + "' not found for FadeIn.");
            onComplete?.Invoke();
            return;
        }
        StartCoroutine(PerformFadeIn(targetSound, duration, targetVolumeOverride, onComplete));
    }

    private IEnumerator PerformFadeIn(
        Sound soundToFade,
        float fadeDuration,
        float targetVolumeOverride,
        System.Action onFadeComplete
    )
    {
        float finalVolume =
            (targetVolumeOverride == -1f)
                ? soundToFade.volume
                : Mathf.Clamp01(targetVolumeOverride);

        soundToFade.source.volume = 0f;
        if (!soundToFade.source.isPlaying)
        {
            soundToFade.source.Play();
        }

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            soundToFade.source.volume = Mathf.Lerp(0f, finalVolume, elapsedTime / fadeDuration);
            yield return null;
        }
        soundToFade.source.volume = finalVolume;
        onFadeComplete?.Invoke();
    }

    public void FadeOut(string soundName, float duration, System.Action onComplete = null)
    {
        Sound targetSound = System.Array.Find(sounds, s => s.name == soundName);
        if (targetSound == null)
        {
            Debug.LogWarning("AudioManager: Sound '" + soundName + "' not found for FadeOut.");
            onComplete?.Invoke();
            return;
        }

        if (!targetSound.source.isPlaying)
        {
            onComplete?.Invoke();
            return;
        }
        StartCoroutine(PerformFadeOut(targetSound, duration, onComplete));
    }

    private IEnumerator PerformFadeOut(
        Sound soundToFade,
        float fadeDuration,
        System.Action onFadeComplete
    )
    {
        float initialVolume = soundToFade.source.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            soundToFade.source.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        soundToFade.source.volume = 0f;
        soundToFade.source.Stop();
        soundToFade.source.volume = soundToFade.volume; // Reset to default for next Play()
        onFadeComplete?.Invoke();
    }
}
