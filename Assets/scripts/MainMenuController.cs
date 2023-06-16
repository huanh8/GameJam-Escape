using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject Game_UI;
    public GameObject MainMenu;

    private void Awake()
    {
        MainMenu.SetActive(true);
        Game_UI.SetActive(false);
    }
    public void PlayGame()
    {
        MainMenu.SetActive(false);
        // player.CanMove = true;
        Game_UI.SetActive(true);
        GameManager.instance.RestartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        MainMenu.SetActive(true);
        player.CanMove = false;
        Game_UI.SetActive(false);
    }

}
