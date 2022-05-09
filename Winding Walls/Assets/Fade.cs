using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Fade : MonoBehaviour
{

    public float fadeSpeed;
    public string sceneName;
    public bool startFromFade;

    // Start is called before the first frame update
    void Start()
    {
        if (startFromFade)
            FadeFrom();
    }

    public void SetFadeSpeed(float newFadeSpeed) {
        fadeSpeed = newFadeSpeed;
    }

    public void SetSceneName(string newSceneName) {
        sceneName = newSceneName;
    }

    public void FadeTo() {
        StartCoroutine(FadeInEnumerator());
    }

    public void FadeFrom() {
        StartCoroutine(FadeOutEnumerator());
    }

    private IEnumerator FadeInEnumerator() {
        Color objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a < 1) {
            fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOutEnumerator() {
        Color objectColor = gameObject.GetComponent<Image>().color;
        float fadeAmount;

        while (gameObject.GetComponent<Image>().color.a > 0) {
            fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            gameObject.GetComponent<Image>().color = objectColor;
            yield return null;
        }
    }
}
