using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;


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
    private int _butonDegeri;
    private int _dogruSonuc;
    private int _kalanHak;
    private bool _butonaBasýldýmý;
    private string _zorlukderecesi;

    private GameObject[] karelerDizi = new GameObject[25];
    List<int> _bolumdegerleriListesi = new List<int>();

    HealthManager _healthManager;
    ScoreManager _scoreManager;

    private void Awake()
    {
        _butonaBasýldýmý = false;
        _soruPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        StartCoroutine(FadeOut());
        kareOlustur();

        _kalanHak = 3;
        _healthManager = UnityEngine.Object.FindAnyObjectByType<HealthManager>();
        _healthManager.CanKontrol(_kalanHak);

        _scoreManager = UnityEngine.Object.FindAnyObjectByType<ScoreManager>();
    }

    private void Start()
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
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasýldý());
            karelerDizi[i] = kare;
        }
        StartCoroutine(DoFadeRoutine());
        DegerYazdýr();
        Invoke(nameof(SoruPanelAc), 3.5f);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.15f);
        }

    }

    private void ButonaBasýldý()
    {
        if (_butonaBasýldýmý)
        {
            _butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            //Debug.Log(_butonDegeri);
            SonucuKontrolEt();
        }
    }

    private void SonucuKontrolEt()
    {
        if (_butonDegeri == _dogruSonuc)
        {
            _scoreManager.PuanArtýr(_zorlukderecesi);
            //Debug.Log(_zorlukderecesi+ " --- "+ _scoreManager._topPuan);
            //Debug.Log("Doðru");
        }
        else
        {
            _kalanHak--;
            _healthManager.CanKontrol(_kalanHak);
            Debug.Log("Yanlýþ");
        }
    }
    private void DegerYazdýr()
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
        _butonaBasýldýmý = true;
    }

    private void SoruyuSor()
    {
        _bolenSayý = UnityEngine.Random.Range(3, 13);

        _kacýncýSoru = UnityEngine.Random.Range(0, _bolumdegerleriListesi.Count);

        _dogruSonuc = _bolumdegerleriListesi[_kacýncýSoru];

        _bolunenSayý = _bolenSayý * _dogruSonuc;

        if (_bolunenSayý <= 50)
        {
            _zorlukderecesi = "Kolay";
        }
        else if (50 < _bolunenSayý && _bolunenSayý <= 80)
        {
            _zorlukderecesi = "Orta";
        }
        else
        {
            _zorlukderecesi = "Zor";
        }

        _soruText.text = _bolunenSayý.ToString() + " / " + _bolenSayý.ToString();
    }
}



