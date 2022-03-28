using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bgm;
    private GameObject[] duplicateBGM;
    private bool notFirst;
    private void Awake()
    {
        duplicateBGM = GameObject.FindGameObjectsWithTag("BGM");
        foreach (GameObject tmp_bgm in duplicateBGM)
        {
            if (tmp_bgm.scene.buildIndex == -1)
            {
                notFirst = true;
            }
        }
        if (notFirst)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMusic()
    {
        if (bgm.isPlaying) return;
        bgm.Play();
    }

    public void StopMusic()
    {
        bgm.Stop();
    }
}
