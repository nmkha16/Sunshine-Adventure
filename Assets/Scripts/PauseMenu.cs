using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject targetMenu, endMenu;
    void Awake()
    {
        targetMenu.SetActive(false);
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        targetMenu.SetActive(false);
        SceneManager.LoadScene(sceneBuildIndex:0);
        SceneManager.LoadScene(sceneBuildIndex: 1, LoadSceneMode.Additive);
    }

    public void pauseMenu()
    {
        targetMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        targetMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetSceneAt(0).name != "MainMenu" && !endMenu.activeInHierarchy)
            {
                pauseMenu();
            }
        }
    }
}
