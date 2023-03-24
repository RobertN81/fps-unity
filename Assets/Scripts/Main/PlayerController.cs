using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private Animator animator;
    private Rigidbody rg;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rg = GetComponent<Rigidbody>();
        FindObjectOfType<Billboard>().gameObject.transform.SetParent(transform, false);
    }

    void Update()
    {
        Move();
        rg.velocity = Vector3.zero;
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;

        transform.LookAt(transform.position + dir);

        animator.SetBool("isMove", dir != Vector3.zero);
    }
}
