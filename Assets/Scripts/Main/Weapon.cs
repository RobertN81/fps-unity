using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet; // �Ѿ� ������
    public Transform bulletPos; // �Ѿ� �߻� ��ġ
    public int bulletForce = 30; // �Ѿ� �߻� ��
    public int currentBulletCount; // ���� �Ѿ� ����
    public int maxBulletCount = 30; // �ִ� �Ѿ� ����
    public float fireRate = 0.125f; // ����� (0.125 * 8 = 1)
    public float currentFireRate; // ���� �߻� ������ �ð�
    public float reloadTime = 1.0f; // ������ ������

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
