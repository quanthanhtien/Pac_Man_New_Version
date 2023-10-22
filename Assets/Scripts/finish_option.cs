using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish_option : MonoBehaviour
{
    // Start is called before the first frame update
    public void Home() {
        SceneManager.LoadScene("home_game");
    }
    public void QuitGame_finish() {
        Application.Quit();
        Debug.Log("quit game");        
    }
}
