using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject flowPanel;
    public TMP_Text flowText;
    public TMP_Text currentBulletCount;
    public TMP_Text maxBulletCount;
    public TMP_Text reloadText;
    public TMP_Text timeText;
    public TMP_Text bossHPText;
    public TMP_Text playerHPText;
    public Slider bossHPBar;


    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFlowPanel(bool isActice)
    {
        flowPanel.SetActive(isActice);
    }

    public void ShowFlowMsg(string text)
    {
        ShowFlowPanel(true);
        flowText.text = text;
    }

    public void ShowCurrentBullet(int num)
    {
        currentBulletCount.text = num.ToString();
    }

    public void ShowMaxBullet(int num)
    {
        maxBulletCount.text = $"/{num}";
    }

    public void UpdateReloadText(bool isActice)
    {
        reloadText.gameObject.SetActive(isActice);
    }

    public void UpdateTimeText(int num)
    {
        timeText.text = num.ToString();
    }

    public void UpdateBossHPBar(float num)
    {
        bossHPBar.value = num;
    }

    public void UpdateBossHPText(int num)
    {
        bossHPText.text = $"HP: {num}";
    }

    public void UpdatePlayerHPText(int num)
    {
        playerHPText.text = $"HP: {num}";
    }
}
