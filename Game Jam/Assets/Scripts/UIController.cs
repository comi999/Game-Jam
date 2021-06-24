using System.Collections;
using System.Collections.Generic;
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
        if ( Input.GetKeyDown( PauseMenuKey ) )
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
    }

    public void MainMenu_OnPlay()
    {
        SceneManager.UnloadSceneAsync( MainMenu ).completed += a => IsMainMenuLoaded = false;
        PointerController.Instance.IsActive = true;
    }

    public void MainMenu_OnExit()
    {
        Application.Quit();
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
        Application.Quit();
    }

    public void ScoreMenu_OnQuit()
    {
        SceneManager.UnloadSceneAsync( Score ).completed += a => IsScoreMenuLoaded = false;
        SceneManager.LoadSceneAsync( MainMenu ).completed += a => IsMainMenuLoaded = true;
    }

    public void ScoreMenu_OnExit()
    {
        Application.Quit();
    }
}