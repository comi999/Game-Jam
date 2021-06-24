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

    public KeyCode PauseMenuKey;

    public bool IsMainMenuLoaded { get; private set; }
    public bool IsPauseMenuLoaded { get; private set; }
    public bool IsScoreMenuLoaded { get; private set; }
    public int Score { get; set; }

    private void Awake()
    {
        SceneManager.LoadSceneAsync( MainMenu, LoadSceneMode.Additive ).completed += a => IsMainMenuLoaded = true;
    }

    private void Update()
    {
        if ( Input.GetKeyDown( PauseMenuKey ) && !IsMainMenuLoaded && !IsScoreMenuLoaded )
        {
            if ( IsPauseMenuLoaded )
            {
                SceneManager.UnloadSceneAsync( PauseMenu ).completed += a => IsPauseMenuLoaded = false;
                PointerController.Instance.IsActive = true;
            }
            else
            {
                SceneManager.LoadSceneAsync( PauseMenu, LoadSceneMode.Additive ).completed += a => IsPauseMenuLoaded = true;
                PointerController.Instance.IsActive = false;
            }
        }

        // Temporary to test score screen.
        //if ( Input.GetKeyDown( KeyCode.Space ) && !IsMainMenuLoaded && !IsPauseMenuLoaded && !IsScoreMenuLoaded )
        //{
        //    SceneManager.LoadSceneAsync( ScoreMenu, LoadSceneMode.Additive ).completed += a => IsScoreMenuLoaded = true;
        //}
    }

    // Called by TrainManager when two trains collide
    public void LoadScoreMenu()
    {
        SceneManager.LoadSceneAsync(ScoreMenu, LoadSceneMode.Additive).completed += a => IsScoreMenuLoaded = true;
    }


    public void MainMenu_OnPlay()
    {
        SceneManager.UnloadSceneAsync( MainMenu ).completed += a => IsMainMenuLoaded = false;
        PointerController.Instance.IsActive = true;
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
    }

    public void PauseMenu_OnQuit()
    {
        SceneManager.UnloadSceneAsync( PauseMenu ).completed += a => IsPauseMenuLoaded = false;
        SceneManager.LoadSceneAsync( MainMenu, LoadSceneMode.Additive ).completed += a => IsMainMenuLoaded = true;
        PointerController.Instance.IsActive = false;
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
    }

    public void ScoreMenu_OnExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}