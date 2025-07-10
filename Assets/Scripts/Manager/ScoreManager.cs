using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ScoreText;

    public int _topPuan;
    public int _PuanArt;
    void Start()
    {
        _ScoreText.text = _topPuan.ToString();  
    }

    public void PuanArtýr(string zorlukseviyesi)
    {
        switch (zorlukseviyesi)
        {
            case "Kolay":
                _PuanArt = 5;
                break;

            case "Orta":
                _PuanArt = 10;
                break;

            case "Zor":
                _PuanArt = 15;
                break;
        }
        _topPuan += _PuanArt;
    _ScoreText.text = _topPuan.ToString(); 
    }
}
