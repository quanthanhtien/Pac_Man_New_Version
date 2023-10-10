using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text gameOverText;
    public Text scoreText;
    public Text livesText;
    public AnimatedSprite skill_1;
    public AnimatedSprite skill_2;
    public AnimatedSprite skill_3;


    private bool canUseSkill_1 = true;
    private bool canUseSkill_2 = true;
    private bool canUseSkill_3 = true;


    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public GameObject portal;

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
        if (score >=1000) {
            portal.SetActive(true);
        }
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
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

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
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        pacman.DeathSequence();

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

}
