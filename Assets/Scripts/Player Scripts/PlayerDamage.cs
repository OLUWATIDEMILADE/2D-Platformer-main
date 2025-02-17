using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;
    public GameManager gameManager;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;

        canDamage = true;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage(Vector3? waterPosition = null)
    {
        if (canDamage)
        {

            lifeScoreCount--;

            if (lifeScoreCount > 0)
            {
                lifeText.text = "x" + lifeScoreCount;
                gameManager.HandlePlayerRespawn(waterPosition);
            }

            if (lifeScoreCount == 0)
            {
                // Go to Menu scene
                SceneManager.LoadScene("Menu");
                //Time.timeScale = 0f;
                //lifeText.text = "x" + lifeScoreCount;
                //StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GameScene-ALU");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Vector3 waterPosition = collision.transform.position;
            DealDamage(waterPosition);

        }
    }

} // class








































