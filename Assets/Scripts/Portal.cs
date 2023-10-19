using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Portal : MonoBehaviour
{
    public GameObject screne;
    public GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(DelayAndLoadScene());
        }
    }
    private IEnumerator DelayAndLoadScene()
    {
        int lives = gameManager.lives;
        PlayerPrefs.SetInt("Lives", lives);
        int score = gameManager.score;
        PlayerPrefs.SetInt("Score", score);

        screne.SetActive(true);
        yield return new WaitForSeconds(1f); // Tạo delay 2 giây
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
