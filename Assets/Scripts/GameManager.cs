using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerObj;
    public Transform[] playerSpawnPoints;

    public int timer = 30;
    public int bossDamage = 5;
    public int bulletDamage = 20;
    public int currentPlayerHP;
    public int maxPlayerHP = 20;
    public float currentBossHP;
    public float maxBossHP = 500f;
    public bool isShot = false;

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        StartCoroutine(StartGameCoroutine());
    }

    private void SetHP()
    {
        currentBossHP = maxBossHP;
        currentPlayerHP = maxPlayerHP;
        UIManager.Instance.UpdateBossHPBar(currentBossHP / maxBossHP);
        UIManager.Instance.UpdatePlayerHPText(currentPlayerHP);
    }

    void Update()
    {
        UpdateBossHPBar();
    }

    public void AttackBoss() => HadleBossDamage();
    public void AttackPlyaer() => HadlePlayerDamage();

    private void HadleBossDamage()
    {
        currentBossHP -= bulletDamage;
        UIManager.Instance.UpdateBossHPText((int)currentBossHP);

        if (currentBossHP == 0)
        {
            WinGame();
        }
    }

    private void HadlePlayerDamage()
    {
        currentPlayerHP -= bossDamage;
        UIManager.Instance.UpdatePlayerHPText(currentPlayerHP);

        if (currentPlayerHP == 0)
        {
            LoseGame();
        }
    }

    private void WinGame()
    {
        StartCoroutine(EndGameCoroutine("승리!"));
    }

    private void LoseGame()
    {
        StartCoroutine(EndGameCoroutine("패배!"));
    }

    private void TimeOver()
    {
        StartCoroutine(EndGameCoroutine("시간초과!"));
    }

    private void SpawnPlayer()
    {
        int num = Random.Range(0, playerSpawnPoints.Length);
        Instantiate(playerObj, playerSpawnPoints[num].transform.position, playerSpawnPoints[num].rotation);
    }

    private void UpdateBossHPBar()
    {
        UIManager.Instance.UpdateBossHPBar(currentBossHP / maxBossHP);
    }

    IEnumerator TimerCoroutine()
    {
        for (int i = timer; i >= 0; i--)
        {
            UIManager.Instance.UpdateTimeText(i);
            yield return new WaitForSeconds(1f);
        }

        TimeOver();
    }

    IEnumerator StartGameCoroutine()
    {
        UIManager.Instance.ShowFlowMsg("게임준비");
        yield return new WaitForSeconds(1f);

        for (int i = 3; i > 0; i--)
        {
            UIManager.Instance.ShowFlowMsg(i.ToString());
            yield return new WaitForSeconds(1f);
        }

        UIManager.Instance.ShowFlowPanel(false);
        StartCoroutine(TimerCoroutine());
        SpawnPlayer();
        SetHP();
    }

    IEnumerator EndGameCoroutine(string msg)
    {
        UIManager.Instance.ShowFlowMsg(msg);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        UIManager.Instance.ShowFlowPanel(false);
        SceneManager.LoadScene("Lobby");
    }
}
