﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class levelLoader : MonoBehaviour {
    public GameObject loadingScreen;
    public Slider slider;
    public Text pText;

    private void Start()
    {
        loadLevel(1);
    }
    public void loadLevel(int sceneBuild)
    {
        StartCoroutine(LoadAsync(sceneBuild));
        
         
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            pText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
