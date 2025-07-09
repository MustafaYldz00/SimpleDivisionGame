using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private CanvasGroup _group;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _ExitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
        });

    }

    void Start()
    {
        ForPlay();
    }

    void Update()
    {

    }

    public void ForPlay()
    {
        _playButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        _ExitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
