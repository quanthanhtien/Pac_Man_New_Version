using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    public Ghost Ghost { get; private set; }
    public AnimatedSprite deathSequence;
    // public AnimatedSprite Skill_1;
    public SpriteRenderer spriteRenderer { get; private set; }
    public new Collider2D collider { get; private set; }
    public Movement movement { get; private set; }
    public AnimatedSprite skill_1;
    public AnimatedSprite skill_2;
    public AnimatedSprite skill_3;

    public float speed = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     RandomActivateSkill();
        // }
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }
        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        collider.enabled = true;
        deathSequence.enabled = false;
        deathSequence.spriteRenderer.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        collider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.spriteRenderer.enabled = true;
        deathSequence.Restart();
    }

    public void Skill_1() {
        enabled = true;
        spriteRenderer.enabled = true;
        skill_1.GetComponent<Collider2D>().enabled = true;
        movement.enabled = false;
        skill_1.enabled = true;
        skill_1.spriteRenderer.enabled = true;
    }
    public void Skill_2() {
        enabled = true;
        spriteRenderer.enabled = true;
        skill_2.GetComponent<Collider2D>().enabled = true;
        movement.enabled = false;
        skill_2.enabled = true;
        skill_2.spriteRenderer.enabled = true;
    }
    public void Skill_3() {
        enabled = true;
        spriteRenderer.enabled = true;
        skill_3.GetComponent<Collider2D>().enabled = true;
        movement.enabled = false;
        skill_3.enabled = true;
        skill_3.spriteRenderer.enabled = true;
    }
    public IEnumerator ActivateSkill_1() {
        Skill_1(); // Gọi hàm Skill_1()
    
        yield return new WaitForSeconds(speed); // Đợi 1 giây

        // Tắt các thành phần đã được kích hoạt trong hàm Skill_1()
        spriteRenderer.enabled = true;
        skill_1.GetComponent<Collider2D>().enabled = false;
        movement.enabled = true;
        skill_1.enabled = false;
        skill_1.spriteRenderer.enabled = false;
    }
    public IEnumerator ActivateSkill_2() {
        Skill_2(); // Gọi hàm Skill_1()

        yield return new WaitForSeconds(speed); // Đợi 1 giây

        // Tắt các thành phần đã được kích hoạt trong hàm Skill_1()
        spriteRenderer.enabled = true;
        skill_2.GetComponent<Collider2D>().enabled = false;
        movement.enabled = true;
        skill_2.enabled = false;
        skill_2.spriteRenderer.enabled = false;
    }
    public IEnumerator ActivateSkill_3() {
        Skill_3(); // Gọi hàm Skill_1()

        yield return new WaitForSeconds(speed); // Đợi 1 giây

        // Tắt các thành phần đã được kích hoạt trong hàm Skill_1()
        spriteRenderer.enabled = true;
        skill_3.GetComponent<Collider2D>().enabled = false;
        movement.enabled = true;
        skill_3.enabled = false;
        skill_3.spriteRenderer.enabled = false;
    }
    public void ActivateSkill1()
    {
        StartCoroutine(ActivateSkill_1());     
    }
    public void ActivateSkill2()
    {
        StartCoroutine(ActivateSkill_2());     
    }
    public void ActivateSkill3()
    {
        StartCoroutine(ActivateSkill_3());     
    }
}
