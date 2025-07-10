using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject _karePrefab;
    [SerializeField] private Transform _karePanel;
    [SerializeField] private CanvasGroup _panel;
    [SerializeField] private Transform _soruPanel;
    [SerializeField] private TMP_Text _soruText;

    private float fadeDuration = 1.0f;
    private int _bolunenSay�, _bolenSay�;
    private int _kac�nc�Soru;
    private GameObject[] karelerDizi = new GameObject[25];
    List<int> _bolumdegerleriListesi = new List<int>();

    private void Start()
    {
        _soruPanel.GetComponent<RectTransform>().localScale = Vector3.zero ;

        StartCoroutine(FadeOut());
        kareOlustur();
    }

    private void Update()
    {
        
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
        DegerYazd�r();
        Invoke("SoruPanelAc", 1.5f);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.15f);
        }
        
    }

    void DegerYazd�r()
    {
        foreach (var kare in karelerDizi)
        {
            int RastgeleDeger = UnityEngine.Random.Range(4, 12);
            _bolumdegerleriListesi.Add(RastgeleDeger); 
            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RastgeleDeger.ToString();
        }
    }

    public void SoruPanelAc()
    {
        SoruyuSor();
        _soruPanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    
    private void SoruyuSor()
    {
        _bolenSay� = UnityEngine.Random.Range(3, 13);
        _kac�nc�Soru = UnityEngine.Random.Range(0,_bolumdegerleriListesi.Count);
        Debug.Log(_kac�nc�Soru);
        _bolunenSay� = _bolenSay� * _bolumdegerleriListesi[_kac�nc�Soru];
        _soruText.text = _bolunenSay�.ToString() + " / " + _bolenSay�.ToString();
    }


}
