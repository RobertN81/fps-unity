using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet; // 총알 프리팹
    public Transform bulletPos; // 총알 발사 위치
    public int bulletForce = 30; // 총알 발사 힘
    public int currentBulletCount; // 현재 총알 개수
    public int maxBulletCount = 30; // 최대 총알 개수
    public float fireRate = 0.125f; // 연사력 (0.125 * 8 = 1)
    public float currentFireRate; // 현재 발사 딜레이 시간
    public float reloadTime = 1.0f; // 재장전 딜레이

    private bool isReload = false;
    private Animator animator;

    void Start()
    {
        currentBulletCount = maxBulletCount;
        animator = GetComponentInChildren<Animator>();
        UIManager.Instance.ShowCurrentBullet(currentBulletCount);
        UIManager.Instance.ShowMaxBullet(maxBulletCount);
    }

    void Update()
    {
        FireRateCalc();

        if (Input.GetMouseButtonDown(0))
        {
            CheckBullet();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (currentBulletCount == maxBulletCount) return;
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void CheckBullet()
    {
        if (isReload) return;

        if (currentBulletCount > 0)
        {
            Fire();
        }
        else
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void FireRateCalc()
    {
        if (currentFireRate < fireRate)
            currentFireRate += Time.deltaTime;
    }

    private void Fire()
    {
        if (currentFireRate < fireRate) return; // Check Fire Rate

        GameObject bulletObj = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = bulletObj.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * bulletForce;

        animator.SetTrigger("isFire");

        currentBulletCount--;
        currentFireRate = 0.0f;

        UIManager.Instance.ShowCurrentBullet(currentBulletCount);
    }

    IEnumerator ReloadCoroutine()
    {
        isReload = true;
        UIManager.Instance.UpdateReloadText(isReload);

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = maxBulletCount;
        UIManager.Instance.ShowCurrentBullet(currentBulletCount);

        isReload = false;
        UIManager.Instance.UpdateReloadText(isReload);
    }
}
