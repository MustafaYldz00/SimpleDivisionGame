using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SonucManager : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _menuButton;


    private void Start()
    {
        RetryAndMenu();
    }
  
    private void RetryAndMenu()
    {
        _retryButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
        });
        _menuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);
            SceneManager.LoadScene(Consts.SceneNames.MENU_SCENE);
        });
    }
}
