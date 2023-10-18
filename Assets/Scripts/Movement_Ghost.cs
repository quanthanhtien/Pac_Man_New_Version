using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Ghost : MonoBehaviour
{
    public Movement movementScript; // Tham chiếu đến script Movement

    private void Start()
    {
        StartCoroutine(IncreaseSpeedCoroutine());
    }

    private IEnumerator IncreaseSpeedCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f); // Đợi 5 giây

            movementScript.speed += 1f; // Tăng speed lên 1

            // In ra giá trị speed mới
            Debug.Log("New speed: " + movementScript.speed);
        }
    }
}