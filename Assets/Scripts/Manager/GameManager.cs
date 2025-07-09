using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject _karePrefab;
    [SerializeField] private Transform _karePanel;
    [SerializeField] private CanvasGroup _panel;

    private float fadeDuration = 1.0f;

    private GameObject[] karelerDizi = new GameObject[25];

    private void Start()
    {
        StartCoroutine(FadeOut());
        kareOlustur();
    }

    System.Collections.IEnumerator FadeOut()
    {
        float startAlpha = _panel.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            _panel.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }
        _panel.alpha = 0f;
    }

    public void kareOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(_karePrefab, _karePanel);
            karelerDizi[i]= kare;
        }
        StartCoroutine(DoFadeRoutine());
        DegerYazdýr();
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.15f);
        }
        
    }

    void DegerYazdýr()
    {
        foreach (var kare in karelerDizi)
        {
            int RastgeleDeger = UnityEngine.Random.Range(1, 13);

            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RastgeleDeger.ToString();
        }
    }

}
