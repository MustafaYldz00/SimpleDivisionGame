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
    private int _bolunenSayý, _bolenSayý;
    private int _kacýncýSoru;
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
        DegerYazdýr();
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

    void DegerYazdýr()
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
        _bolenSayý = UnityEngine.Random.Range(3, 13);
        _kacýncýSoru = UnityEngine.Random.Range(0,_bolumdegerleriListesi.Count);
        Debug.Log(_kacýncýSoru);
        _bolunenSayý = _bolenSayý * _bolumdegerleriListesi[_kacýncýSoru];
        _soruText.text = _bolunenSayý.ToString() + " / " + _bolenSayý.ToString();
    }


}
