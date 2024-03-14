using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public delegate void OnEventTrigger();
public delegate void OnEventTrigger<T>(T data);

public class GameSceneManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField]
    private MainGameView gameView;
    [SerializeField]
    private EndGameView endGameView;

    [Header("Tile Objects")]
    [SerializeField]
    private TileControler tilePrefab;
    [SerializeField]
    private List<Transform> spawnPos;

    [Header("Level Data")]
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private List<LevelModel> musicList;

    [Header("Limits")]
    [SerializeField]
    private Transform coolLimit;
    [SerializeField]
    private Transform greatLimit;
    [SerializeField]
    private Transform perfectLimit;
    [SerializeField]
    private Transform missLimit;

    public int CurrentPoint { get; private set; }
    public int MaxScore { get; private set; }
    public float FallSpeed { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentPoint = 0;
        starNb = 0;
        tilePrefab.Init(missLimit.position.y);
        tilePrefab.OnClick += OnEventClicked;

        int musicId = Mathf.Clamp(PlayerPrefs.GetInt("MusicId"), 0, musicList.Count - 1);
        MaxScore = musicList[musicId].MaxScore;
        FallSpeed = musicList[musicId].TileFallSpeed;
        director.playableAsset = musicList[musicId].LevelSong;
        director.Play();

        gameView.Init(MaxScore);
        endGameView.Init();
        endGameView.onMainMenuClick += ToMainMenu;
        endGameView.onQuickGameClick += QuitGame;
    }

    #region Event Feature
    public void SpawnTileAtPos1()
    {
        SpawnTile(0);
    }
    public void SpawnTileAtPos2()
    {
        SpawnTile(1);
    }
    public void SpawnTileAtPos3()
    {
        SpawnTile(2);
    }
    public void SpawnTileAtPos4()
    {
        SpawnTile(3);
    }

    private void ToMainMenu()
    {
        StartCoroutine(LoadMainMenuAsync());
    }
    IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    private void QuitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        Debug.Log("Game Ended...");
        endGameView.SetActive(true);
        endGameView.SetViewData(CurrentPoint,starNb);
    }


    #endregion

    private void SpawnTile(int spawnPosId)
    {
        TileControler tmpTile = Instantiate(tilePrefab, spawnPos[spawnPosId]);
        tmpTile.Init(missLimit.position.y, FallSpeed);
        tmpTile.OnClick += OnEventClicked;
    }

    int currentCombo = 0;
    int starNb = 0;
    private int GetPoint(float pos)
    {
        int scoreGain = 0;
        SCORE_TYPE type = SCORE_TYPE.Miss;
        if (coolLimit.position.y < pos 
            || missLimit.position.y >= pos)
        {
            currentCombo = 0;
            scoreGain = 0;
            type = SCORE_TYPE.Miss;
        }
        else if (coolLimit.position.y > pos
            && greatLimit.position.y <= pos)
        {
            currentCombo = 0;
            scoreGain = 1;
            type = SCORE_TYPE.Cool;
        }
        else if (greatLimit.position.y > pos
            && perfectLimit.position.y <= pos)
        {
            currentCombo = 0;
            scoreGain = 2;
            type = SCORE_TYPE.Great;
        }
        else if (perfectLimit.position.y > pos
            && missLimit.position.y <= pos)
        {
            currentCombo++;
            scoreGain = 3 + Mathf.Clamp(currentCombo, 1, 5);
            type = SCORE_TYPE.Perfect;
        }

        Debug.Log("type: " + type.ToString() + " | Combo: " + currentCombo);

        gameView.SetScoreType(type);
        gameView.SetCombo(currentCombo);
        return scoreGain;
    }

    readonly int maxStar = 3;
    private void OnEventClicked(TileControler tile)
    {        
        CurrentPoint += GetPoint(tile.GetPos());
        if(((float)CurrentPoint / MaxScore) > ((float)(starNb+1)/ maxStar))
        {
            starNb = Mathf.Clamp(starNb + 1, 0, maxStar);
        }
        gameView.SetScore(CurrentPoint, starNb);

        //Debug.Log("Tile clicked: " + tile.GetPos() + " | Point: " + CurrentPoint);
    }
}
