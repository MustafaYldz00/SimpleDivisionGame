using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _ExitButton;

    private void Awake()
    {

    }

    void Start()
    {
        
        ForPlay();
    }

    public void ForPlay()
    {
        _playButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        _ExitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
    }
    public void ExitGame()
    {
        Application.Quit();
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
    }
}
