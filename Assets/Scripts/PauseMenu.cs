using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPause) {
                Resume();
            } else {
                Pause();
            }
        }
        // if (Input.GetKeyDown(KeyCode.B)) {
        //     LoadMenu();
        // }
        if (Input.GetKeyDown(KeyCode.P)) {
            LoadQuit(); 
        }
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    // public void LoadMenu() {
    //     SceneManager.LoadScene("home_game");
    // }
    public void LoadQuit() {
        Application.Quit();
        Debug.Log("QUIT ");
    }
}
