using UnityEngine;
using UnityEngine.SceneManagement;
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
            Destroy(this);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != this) return;
        if (!scene.IsValid() || string.IsNullOrEmpty(scene.name)) return;
        if (scene.name != "SampleScene") return;

        StopAllCoroutines();
        labAmbience = null;
        apartmentAmbience = null;

        var labGo = GameObject.Find("Lab Ambience");
        if (labGo != null) labAmbience = labGo.GetComponent<AudioSource>();
        var aptGo = GameObject.Find("Apartment Ambience");
        if (aptGo != null) apartmentAmbience = aptGo.GetComponent<AudioSource>();

        if (labAmbience != null && apartmentAmbience != null)
            SwitchToApartment();
        else if (apartmentAmbience != null && apartmentAmbience.volume < 0.01f)
            StartCoroutine(FadeIn(apartmentAmbience));
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
        source.enabled = true;
        if (source.gameObject != null && !source.gameObject.activeSelf)
            source.gameObject.SetActive(true);
        if (!source.isPlaying)
            source.Play();
        while (source != null && source.volume < 0.5f)
        {
            source.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        if (source == null) yield break;
        while (source != null && source.volume > 0)
        {
            source.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }
        if (source != null)
            source.Stop();
    }
}
