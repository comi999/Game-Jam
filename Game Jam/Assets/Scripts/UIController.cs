using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : ISingleton< UIController >
{
    public string MainMenu = "MainMenu";
    public string PauseMenu = "PauseMenu";
    public string ScoreMenu = "ScoreMenu";

    public ScoreDisplayController ScoreController;

    public KeyCode PauseMenuKey;

    public bool IsMainMenuLoaded { get; private set; }
    public bool IsPauseMenuLoaded { get; private set; }
    public bool IsScoreMenuLoaded { get; private set; }
    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
            ScoreController.PushScore();
        }
    }

    public AudioPool[] AllAudioPools;

    private void Awake()
    {
        AudioPool.PopulateRegistry( AllAudioPools );
        SceneManager.LoadSceneAsync( MainMenu, LoadSceneMode.Additive ).completed += a => IsMainMenuLoaded = true;
    }

    private void Start()
    {
        SoundController.Instance.Play( "BackgroundMusic", true );
    }

    private void Update()
    {
        if ( Input.GetKeyDown( PauseMenuKey ) && !IsMainMenuLoaded && !IsScoreMenuLoaded )
        {
            if ( IsPauseMenuLoaded )
            {
                SceneManager.UnloadSceneAsync( PauseMenu ).completed += a => IsPauseMenuLoaded = false;
                PointerController.Instance.IsActive = true;
                ScoreController.gameObject.SetActive( true );
                Time.timeScale = 1;
            }
            else
            {
                SceneManager.LoadSceneAsync( PauseMenu, LoadSceneMode.Additive ).completed += a => IsPauseMenuLoaded = true;
                PointerController.Instance.IsActive = false;
                ScoreController.gameObject.SetActive( false );
                Time.timeScale = 0;
            }
        }
    }

    // Called by TrainManager when two trains collide
    public void LoadScoreMenu()
    {
        if ( !IsMainMenuLoaded && !IsPauseMenuLoaded )
        {
            SceneManager.LoadSceneAsync(ScoreMenu, LoadSceneMode.Additive).completed += a => IsScoreMenuLoaded = true;
            PointerController.Instance.IsActive = false;
            ScoreController.gameObject.SetActive( false );
        }
    }


    public void MainMenu_OnPlay()
    {
        Score = 0;
        SceneManager.UnloadSceneAsync( MainMenu ).completed += a => IsMainMenuLoaded = false;
        MenuCrashController.Instance.TriggerCrash();
        PointerController.Instance.IsActive = true;
        SoundController.Instance.Play( "BackgroundMusic", true );
        TrainManager.Instance.isDemoMode = false;
                ScoreController.gameObject.SetActive( true );
    }

    public void MainMenu_OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void PauseMenu_OnResume()
    {
        SceneManager.UnloadSceneAsync( PauseMenu ).completed += a => IsPauseMenuLoaded = false;
        PointerController.Instance.IsActive = true;
        ScoreController.gameObject.SetActive( true );
        Time.timeScale = 1;
    }

    public void PauseMenu_OnQuit()
    {
        SceneManager.UnloadSceneAsync( PauseMenu ).completed += a => IsPauseMenuLoaded = false;
        SceneManager.LoadSceneAsync( MainMenu, LoadSceneMode.Additive ).completed += a => IsMainMenuLoaded = true;
        ScoreController.gameObject.SetActive( false );
        PointerController.Instance.IsActive = false;

        Time.timeScale = 1;
    }

    public void PauseMenu_OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ScoreMenu_OnQuit()
    {
        SceneManager.UnloadSceneAsync( ScoreMenu ).completed += a => IsScoreMenuLoaded = false;
        SceneManager.LoadSceneAsync( MainMenu, LoadSceneMode.Additive ).completed += a => IsMainMenuLoaded = true;

        TrainManager.Instance.ResetManager();
    }

    public void ScoreMenu_OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public int[] GetScoreDigits()
    {
        int score = Score > 999 ? 999 : Score;
        int[] digits = new int[ 3 ];
        digits[ 0 ] = score % 10;
        digits[ 1 ] = ( score % 100 - digits[ 0 ] ) / 10;
        digits[ 2 ] = ( score - digits[ 0 ] - digits [ 1 ] * 10 ) / 100;

        return digits;
    }

    private int m_Score;
}