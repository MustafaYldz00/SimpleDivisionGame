using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;


public class GameManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject _karePrefab;
    [SerializeField] private Transform _karePanel;
    [SerializeField] private CanvasGroup _panel;
    [SerializeField] private Transform _soruPanel;
    [SerializeField] private TMP_Text _soruText;
    [SerializeField] private GameObject _finalPanel;
    [SerializeField] private Timer _timerTextUI;
    [SerializeField] private Sprite[] _resimler;

    private float fadeDuration = 1.0f;
    private int _bolunenSay�, _bolenSay�;
    private int _kac�nc�Soru;
    private int _butonDegeri;
    private int _dogruSonuc;
    private int _kalanHak;
    private bool _butonaBas�ld�m�;
    private string _zorlukderecesi;
    private GameObject _GecerliKare;

    private GameObject[] karelerDizi = new GameObject[25];
    List<int> _bolumdegerleriListesi = new List<int>();

    HealthManager _healthManager;
    ScoreManager _scoreManager;

    private void Awake()
    {
        
        
        _finalPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        _butonaBas�ld�m� = false;
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
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBas�ld�());
            karelerDizi[i] = kare;
            kare.transform.GetChild(1).GetComponent<Image>().sprite = _resimler[UnityEngine.Random.Range(0, _resimler.Length)];
        }
        StartCoroutine(DoFadeRoutine());
        DegerYazd�r();
        Invoke(nameof(SoruPanelAc), 3.5f);
        //Timer.Instance.StartTimer();
        
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var kare in karelerDizi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.15f);
        }

    }

    private void ButonaBas�ld�()
    {
        if (_butonaBas�ld�m�)
        {
            
            _butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            _GecerliKare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            //Debug.Log(_butonDegeri);
            SonucuKontrolEt();
        }
    }

    private void SonucuKontrolEt()
    {
        if (_butonDegeri == _dogruSonuc)
        {
            _GecerliKare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            _GecerliKare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
            _GecerliKare.transform.GetComponent<Button>().interactable = false;
            _scoreManager.PuanArt�r(_zorlukderecesi);
            //Debug.Log(_zorlukderecesi+ " --- "+ _scoreManager._topPuan);
            //Debug.Log("Do�ru");
            AudioManager.Instance.Play(SoundType.CorrectSound);
            _bolumdegerleriListesi.RemoveAt(_kac�nc�Soru);

            if (_bolumdegerleriListesi.Count > 0)
            {
                SoruPanelAc();

            }
            else
            {
                OyunBitti();
                AudioManager.Instance.Play(SoundType.WinSound);
            }

        }
        else 
        {
        
            AudioManager.Instance.Play(SoundType.inCorrentSound);
            _kalanHak--;
            _healthManager.CanKontrol(_kalanHak);
            Debug.Log("Yanl��");
        }
        if (_kalanHak <= 0)
        {
            OyunBitti();
            AudioManager.Instance.Play(SoundType.LoseSound);
        }
    }

    private void OyunBitti()
    {
        Debug.Log("Oyun bitti");
        _butonaBas�ld�m� = false;
        _finalPanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
        _timerTextUI.StopTimer();
        
    }
    private void DegerYazd�r()
    {
        foreach (var kare in karelerDizi)
        {
            int RastgeleDeger = UnityEngine.Random.Range(3, 13);
            _bolumdegerleriListesi.Add(RastgeleDeger);
            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = RastgeleDeger.ToString();
        }
    }

    public void SoruPanelAc()
    {
        SoruyuSor();
        _soruPanel.GetComponent<RectTransform>().DOScale(1, 0.5f).SetEase(Ease.OutBack);
        _butonaBas�ld�m� = true;
        
    }

    private void SoruyuSor()
    {
        _bolenSay� = UnityEngine.Random.Range(3, 15);

        _kac�nc�Soru = UnityEngine.Random.Range(0, _bolumdegerleriListesi.Count);

        _dogruSonuc = _bolumdegerleriListesi[_kac�nc�Soru];

        _bolunenSay� = _bolenSay� * _dogruSonuc;

        if (_bolunenSay� <= 50)
        {
            _zorlukderecesi = "Kolay";
        }
        else if (50 < _bolunenSay� && _bolunenSay� <= 80)
        {
            _zorlukderecesi = "Orta";
        }
        else
        {
            _zorlukderecesi = "Zor";
        }

        _soruText.text = _bolunenSay�.ToString() + " / " + _bolenSay�.ToString();
    }
}



