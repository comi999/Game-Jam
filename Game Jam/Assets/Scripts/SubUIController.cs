using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubUIController : MonoBehaviour
{
    public void MainMenu_OnPlay() => UIController.Instance.MainMenu_OnPlay();
    public void MainMenu_OnExit() => UIController.Instance.MainMenu_OnExit();
    public void PauseMenu_OnResume() => UIController.Instance.PauseMenu_OnResume();
    public void PauseMenu_OnQuit() => UIController.Instance.PauseMenu_OnQuit();
    public void PauseMenu_OnExit() => UIController.Instance.PauseMenu_OnExit();
    public void ScoreMenu_OnQuit() => UIController.Instance.ScoreMenu_OnQuit();
    public void ScoreMenu_OnExit() => UIController.Instance.ScoreMenu_OnExit();

    
}
