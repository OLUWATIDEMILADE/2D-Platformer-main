using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player;
    private Vector3 lastSafePosition;

    // Set timer to 8 minutes (480 seconds)
    public float countTime = 480f;
    private float currentTime;

    public TextMeshProUGUI timerText;
    public GameObject TimerPanel;
    public float jumpForce = 10f;

    void Start()
    {
        lastSafePosition = player.position;
        currentTime = countTime;
        UpdateTimerDisplay();
    }

    public void HandlePlayerRespawn(Vector3? waterPosition = null)
    {
        if (waterPosition.HasValue)
        {
            player.position = waterPosition.Value + new Vector3(2f, 5f, 0);
        }
        else
        {
            player.position = lastSafePosition + new Vector3(0, 3f, 0);
        }
    }

    private bool PlayerInWater()
    {
        return player.position.y < -5;
    }

    private void Update()
    {
        if (!PlayerInWater())
        {
            lastSafePosition = player.position;
        }

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                TimerEnded();
            }
            UpdateTimerDisplay();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        // Check for space key press to make the player jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
    }

    // Player jump with Space key pressed
    public void PlayerJump()
    {
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        Debug.Log("Time Up");
        TimerPanel.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(DelayOnTimerEnd());
    }

    IEnumerator DelayOnTimerEnd()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("GameScene-ALU");
    }
}
