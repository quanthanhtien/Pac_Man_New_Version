using UnityEngine;

public class GhostDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Skill"))
        {
            FindObjectOfType<GameManager>().sound_ghost_death();
            GhostFrightened ghost = GetComponent<GhostFrightened>();
            ghost.Eaten();
            // Thêm code xử lý khi Ghost chết ở đây
        }
    }
}