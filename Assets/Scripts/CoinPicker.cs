using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinPicker : MonoBehaviour
{
    private float coinCount = 0;
    public TextMeshProUGUI coinCountDisplayer, highScore;
    [SerializeField] public GameObject endMenu, inGameHUD;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            coinCount++;
            coinCountDisplayer.text = coinCount.ToString();
            Destroy(collision.gameObject);
        }
    }

    void Awake()
    {
        endMenu.SetActive(false);
        inGameHUD.SetActive(true);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        inGameHUD.SetActive(true);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    public void EndMenu()
    {
        endMenu.SetActive(true);
        highScore.text = "Your score: " + coinCount.ToString();
        Time.timeScale = 0f;
    }
}
