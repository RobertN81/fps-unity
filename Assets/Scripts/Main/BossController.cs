using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int bulletForce = 30;
    public float fireRate = 0.1f;
    public float currentFireRate;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float attackRange = 20f;

    private Rigidbody rg;

    private void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Player")) return;
        FireRateCalc();

        float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distance <= attackRange)
        {
            Fire();
        }
        else
        {
            BossMove();
        }

        rg.velocity = Vector3.zero;
    }

    private void BossMove()
    {
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void FireRateCalc()
    {
        if (currentFireRate < fireRate)
            currentFireRate += Time.deltaTime;
    }

    private void Fire()
    {
        if (currentFireRate < fireRate) return;

        GameObject bulletObj = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = bulletObj.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * bulletForce;

        currentFireRate = 0.0f;
    }
}