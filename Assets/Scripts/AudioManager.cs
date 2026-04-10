using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource labAmbience;
    public AudioSource apartmentAmbience;
    public AudioSource uiClick;

    public float fadeTime = 2.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        if (uiClick != null) uiClick.Play();
    }

    public void SwitchToLab()
    {
        StartCoroutine(FadeOut(apartmentAmbience));
        StartCoroutine(FadeIn(labAmbience));
    }

    public void SwitchToApartment()
    {
        StartCoroutine(FadeOut(labAmbience));
        StartCoroutine(FadeIn(apartmentAmbience));
    }

    private IEnumerator FadeIn(AudioSource source)
    {
        if (source == null) yield break;
        source.Play();
        float startVol = source.volume;
        while (source.volume < 0.5f)
        {
            source.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        if (source == null) yield break;
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }
        source.Stop();
    }
}
