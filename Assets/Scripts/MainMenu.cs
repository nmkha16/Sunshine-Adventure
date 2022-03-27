using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject targetMenu;
    private void Awake()
    {
        targetMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    
}
