using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bulletPool = default;
    public Transform effectPool = default;
    public GameObject bulletPrefab = default;
    public Rigidbody playerRigid = default;

    public float speed = default;
    public int hp = default;

    private bool isFire = default;
    private float fireDelay = 0;

    private Animator animator;

    public static bool live = true;

    private float stunDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        live = true;
        animator = GetComponent<Animator>();
        stunDelay = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (live && stunDelay==0)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0 || z != 0)
            {
                animator.SetBool("move", true);
                float xSpeed = x * speed;
                float zSpeed = z * speed;

                Vector3 velocity = new Vector3(xSpeed, 0f, zSpeed);

                playerRigid.velocity = velocity;

            }
            else if (x == 0 && z == 0)
            {
                animator.SetBool("move", false);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetBool("Attack", true);
                isFire = true;
            }

            if (Input.GetKeyUp(KeyCode.Z))
            {
                animator.SetBool("Attack", false);
                isFire = false;
            }

            if (isFire)
            {
                animator.SetBool("Swim", true);
                

                fireDelay += Time.deltaTime;
                if (fireDelay >= 0.1)
                {
                    GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z + 0.5f), Quaternion.identity, bulletPool);
                    bullet.transform.LookAt(transform.forward);
                    bullet.GetComponent<Bullet>().effectPool = effectPool;
                    fireDelay = 0;
                }
            }
        }

        else if (stunDelay >= 0)
        {
            stunDelay -= Time.deltaTime;
            if (stunDelay < 0)
            {
                stunDelay = 0;
            }
        }
    }

    public void Hit(float damage)
    {
        hp -= (int)damage;
        animator.Play("Hit");
    }

    public void Stun(float time)
    {
        stunDelay = time;
        animator.Play("Spin");
    }

    public void Die()
    {
        live = false;
        playerRigid.velocity = Vector3.zero;
        animator.Play("Death");

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
