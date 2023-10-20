using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImageAnimation : MonoBehaviour
{
    public List<GameObject> images; // Danh sách các ảnh
    public float delay = 1f; // Thời gian chờ giữa các ảnh

    private int currentIndex = 0; // Chỉ số hiện tại của ảnh đang được hiển thị

    private void Start()
    {
        // Tắt tất cả các ảnh ban đầu
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // Bắt đầu hoạt ảnh
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        while (true)
        {
            // Bật ảnh hiện tại
            images[currentIndex].SetActive(true);

            // Chờ một khoảng thời gian trước khi tắt ảnh hiện tại
            yield return new WaitForSeconds(delay);

            // Tắt ảnh hiện tại
            images[currentIndex].SetActive(false);

            // Tăng chỉ số để chuyển đến ảnh tiếp theo
            currentIndex = (currentIndex + 1) % images.Count;
        }
    }
}