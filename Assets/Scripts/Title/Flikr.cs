using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Flikr : MonoBehaviour
{
    public TMP_Text pressAnyKey;

    void Start()
    {
        StartCoroutine(FlickrCoroutine(pressAnyKey));
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    IEnumerator FlickrCoroutine(TMP_Text targetText)
    {
        targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, 1);

        while (targetText.color.a > 0.0f)
        {
            targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, targetText.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }

        StartCoroutine(FlickrCoroutine(targetText));
    }
}
