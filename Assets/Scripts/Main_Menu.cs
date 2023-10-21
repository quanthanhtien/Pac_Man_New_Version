using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame() {
        StartCoroutine(PlayGameDelay());
    }
    private IEnumerator PlayGameDelay() {
        yield return new WaitForSeconds(1f); // Tạo delay 2 giây
        SceneManager.LoadScene("Pacman");
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("quit game");        
    }
}
