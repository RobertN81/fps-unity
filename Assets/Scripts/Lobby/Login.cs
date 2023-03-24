using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    const int characterLimitMin = 3;
    const int characterLimitMax = 16;
    public TMP_Text waringText;
    public TMP_InputField idInputField;
    public Button loginButton;
    public Button exitButton;


    void Start()
    {
        idInputField.characterLimit = characterLimitMax;
        loginButton.onClick.AddListener(LoginBtn);
        exitButton.onClick.AddListener(ExitBtn);
    }

    private void LoginBtn()
    {
        if (idInputField.text.Length <= characterLimitMax && idInputField.text.Length >= characterLimitMin)
        {
            PlayerPrefs.SetString("Nickname", idInputField.text);
            SceneManager.LoadScene("Main");
        }
        else
        {
            waringText.gameObject.SetActive(true);
        }
    }

    private void ExitBtn()
    {
        Application.Quit();
    }
}
