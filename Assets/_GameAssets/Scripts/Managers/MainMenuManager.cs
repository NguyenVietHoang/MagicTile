using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MainMenuView mainMenuView;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuView.Init();
        mainMenuView.onLevelClick += LoadLevel;
        mainMenuView.onQuickGameClick += QuitGame;

    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void LoadLevel(int levelID)
    {
        PlayerPrefs.SetInt("MusicId", levelID);
        StartCoroutine(LoadMainGameAsync());
    }

    IEnumerator LoadMainGameAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
