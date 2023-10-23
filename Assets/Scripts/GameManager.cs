using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public AnimatedSprite skill_1;
    public AnimatedSprite skill_2;
    public AnimatedSprite skill_3;

    public AudioSource aus;
    public AudioClip pellet_eaten;
    public AudioClip pacman_death;
    public AudioClip ghost_death;

    private bool canUseSkill_1 = true;
    private bool canUseSkill_2 = true;
    private bool canUseSkill_3 = true;

    // public ScoreManager scoreManager;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get;  set; }
    public int lives { get; private set; }
    public GameObject portal;
    public GameObject portal_1;


    private void Start()
    {
        pacman = FindObjectOfType<Pacman>();
        NewGame();
        skill_1.enabled = true;
        skill_1.spriteRenderer.enabled = true;
        skill_2.enabled = true;
        skill_2.spriteRenderer.enabled = true;
        skill_3.enabled = true;
        skill_3.spriteRenderer.enabled = true;
        int lives = PlayerPrefs.GetInt("Lives", 3);
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
        int score = PlayerPrefs.GetInt("Score", 0);
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');     
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
        if (Input.GetKeyDown(KeyCode.Q) && canUseSkill_1) {
            pacman.ActivateSkill1();
            canUseSkill_1 = false;
            skill_1.enabled = false;
            StartCoroutine(BlinkSkill_1()); // Bắt đầu coroutine nhấp nháy
            Invoke(nameof(ResetSkillCooldown1), 10f);
        }
        if (Input.GetKeyDown(KeyCode.E) && canUseSkill_2) {
            pacman.ActivateSkill2();
            canUseSkill_2 = false;
            skill_2.enabled = false;
            StartCoroutine(BlinkSkill_2()); // Bắt đầu coroutine nhấp nháy
            Invoke(nameof(ResetSkillCooldown2), 10f);
        }
        if (Input.GetKeyDown(KeyCode.R) && canUseSkill_3) {
            pacman.ActivateSkill3();
            canUseSkill_3 = false;
            skill_3.enabled = false;
            StartCoroutine(BlinkSkill_3()); // Bắt đầu coroutine nhấp nháy
            Invoke(nameof(ResetSkillCooldown3), 10f);
        }
        if (score >= 1000) {
            portal.SetActive(true);
        }
        if (score >= 2500) {
            portal_1.SetActive(true);
        }
    }

    public void  sound_pellet() {
        if (!aus.isPlaying) {
        aus.PlayOneShot(pellet_eaten);
        aus.SetScheduledEndTime(Time.time + 0.001f);
    }
    }
    public void  sound_ghost_death() {
        aus.PlayOneShot(ghost_death);
        aus.SetScheduledEndTime(Time.time + 0.001f);
    }

    private IEnumerator BlinkSkill_1() {
        float duration = 10f; // Thời gian invoke
        float blinkInterval = 0.5f; // Khoảng thời gian nhấp nháy

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            skill_1.spriteRenderer.enabled = isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
            isVisible = !isVisible;
        }

        skill_1.spriteRenderer.enabled = true; // Đảm bảo sprite hiển thị khi kết thúc invoke
        canUseSkill_1 = true; // Cho phép sử dụng kỹ năng tiếp theo
    }
    private IEnumerator BlinkSkill_2() {
        float duration = 10f; // Thời gian invoke
        float blinkInterval = 0.5f; // Khoảng thời gian nhấp nháy

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            skill_2.spriteRenderer.enabled = isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
            isVisible = !isVisible;
        }

        skill_2.spriteRenderer.enabled = true; // Đảm bảo sprite hiển thị khi kết thúc invoke
        canUseSkill_2 = true; // Cho phép sử dụng kỹ năng tiếp theo
    }
    private IEnumerator BlinkSkill_3() {
        float duration = 10f; // Thời gian invoke
        float blinkInterval = 0.5f; // Khoảng thời gian nhấp nháy

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            skill_3.spriteRenderer.enabled = isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
            isVisible = !isVisible;
        }

        skill_3.spriteRenderer.enabled = true; // Đảm bảo sprite hiển thị khi kết thúc invoke
        canUseSkill_3 = true; // Cho phép sử dụng kỹ năng tiếp theo
    }

    private void ResetSkillCooldown1()
    {
        canUseSkill_1 = true;
        skill_1.enabled = true;
        skill_1.spriteRenderer.enabled = true;
    }
    private void ResetSkillCooldown2()
    {
        canUseSkill_2 = true;
        skill_2.enabled = true;
        skill_2.spriteRenderer.enabled = true;
    }
    private void ResetSkillCooldown3()
    {
        canUseSkill_3 = true;
        skill_3.enabled = true;
        skill_3.spriteRenderer.enabled = true;
    }

    private void NewGame()
    {
        NewRound();
    }

    private void NewRound()
    {

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver()
    {
        StartCoroutine(BlinkGameOverText()); // Bắt đầu coroutine nhấp nháy và delay
    }

    private IEnumerator BlinkGameOverText() 
    {
        float duration = 4f; // Thời gian delay trước khi nhấp nháy
        float blinkInterval = 0.5f; // Khoảng thời gian nhấp nháy

        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < duration)
        {
            gameOverText.enabled = isVisible;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
            isVisible = !isVisible;
        }

        // gameOverText.enabled = true; // Đảm bảo hiển thị gameOverText khi kết thúc delay
        SceneManager.LoadScene("finish"); // Chuyển sang màn hình khác
    }
    private void SetLives(int newlives)
    {
        this.lives = newlives;
        PlayerPrefs.SetInt("Lives", newlives);
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int newscore)
    {
        this.score = newscore;
        PlayerPrefs.SetInt("Score", newscore);
        scoreText.text = newscore.ToString().PadLeft(2, '0');
    }
    public int  GetScore() {
        return score;
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();
        aus.PlayOneShot(pacman_death);
        aus.SetScheduledEndTime(Time.time + 0.001f);
        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        
        SetScore(score + pellet.points);
        
        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }

}
