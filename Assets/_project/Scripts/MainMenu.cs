using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen = null;

    [SerializeField]
    private GameObject controlsScreen = null;
    [SerializeField]
    private GameObject creditsScreen = null;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.AudioClipID.MENU_MUSIC);
    }

    public void OnPlayButtonClick()
    {
        PlayButtonSFX();
        loadingScreen.SetActive(true);
        const int GAME_SCENE_INDEX = 1;
        SceneManager.LoadSceneAsync(GAME_SCENE_INDEX);

        SoundManager.Instance.PlayMusic(SoundManager.AudioClipID.GAMEPLAY_MUSIC);
    }

    public void OnControlsButtonClick()
    {
        PlayButtonSFX();
        controlsScreen.SetActive(true);
    }
    public void OnControlsBackButtonClick()
    {
        PlayButtonSFX();
        controlsScreen.SetActive(false);
    }
    public void OnCreditsButtonClick()
    {
        PlayButtonSFX();
        creditsScreen.SetActive(true);
    }
    public void OnCreditsBackButtonClick()
    {
        PlayButtonSFX();
        creditsScreen.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        PlayButtonSFX();
        Application.Quit();
    }

    private void PlayButtonSFX()
    {
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.MENU_BUTTON_CLICK);
    }
}
