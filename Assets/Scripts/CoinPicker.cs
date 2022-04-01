using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinPicker : MonoBehaviour
{
    private int coinCount = 0;
    public TextMeshProUGUI coinCountDisplayer, highScore;
    [SerializeField] public GameObject endMenu;
    public Animator animator;
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            coinCount++;
            player.moveSpeed += coinCount * 0.01f;
            coinCountDisplayer.text = coinCount.ToString();
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Trap")
        {
            animator.Play("Die");
            animator.SetBool("isDead",true);
            EndMenu();
        }
    }

    void Awake()
    {
        endMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void EndMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endMenu.SetActive(true);
        highScore.text = "Your score: " + coinCount.ToString() +"\n" +
            "Highest score: " + PlayerPrefs.GetInt("highscore").ToString();
        if (PlayerPrefs.GetInt("highscore") <= coinCount)
        {
            PlayerPrefs.SetInt("highscore", coinCount);
            PlayerPrefs.Save();
        }
        Time.timeScale = 0f;
    }
}
