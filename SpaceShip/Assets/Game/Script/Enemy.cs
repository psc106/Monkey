
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //1�ʰ� �̵��ϴ� �Ÿ�
    public float distance = default;
    //��� �ð�
    public float waitTime = default;
    //ü��
    public int hp = default;
    public int id;

    //��� �ð�
    private float moveTime;
    private float fireTime;
    //�̵� ��(false), �̵��� ��� ����(true)
    private bool isWait;
    //�̵� �� ������(false) ��� �Ϸ� ����  
    private bool isLineChange;

    //�̵� �Ϸ� ����
    private bool successMove;

    public Line currLine;

    public float fireDelayMin = default;
    public float fireDelayMax = default;

    private float fireDelay = 4f;
    public GameObject bulletPrefab = default;
    private Transform target = default;

    void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        if(player!=null) target = player.transform;
        isWait = false;
        isLineChange = false;
        successMove = false;
        moveTime = 0;
        fireTime = 0;
        fireDelay = Random.Range(fireDelayMin, fireDelayMax);
    }

    private void Update()
    {
        fireTime += Time.deltaTime;

        if (fireDelay <= fireTime)
        {
            if (target != null)
            {
                fireTime = 0;
                GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z + 0.5f), Quaternion.identity);
                bullet.transform.LookAt(target.position);
                fireDelay = Random.Range(fireDelayMin, fireDelayMax);
            }
        }

        if (currLine.nextLine != null)
        {
            //�̵� ����
            if (!isLineChange)
            {
                if (currLine.nextLine.enemy == null)
                {
                    moveTime += Time.deltaTime;
                }
            }

            //1�� ���(�̵�->��� ��ȯ)
            if (!isWait)
            {
                MoveEnemy();
            }


            if (!isWait && moveTime >= 1)
            {
                isWait = true;
            }

            //n�� ���(���->�����̵� ��ȯ)
            if (isWait && moveTime >= waitTime + 1)
            {
                moveTime = 0;
                isLineChange = true;
                ChangeLineEnemy();
            }
            //���� �̵� ��ȯ �Ϸ�(spawner�� �Ǵ�)
            if (successMove)
            {
                isWait = false;
                isLineChange = false;
                successMove = false;
            }
        }
    }

    public void MoveEnemy()
    {
        if (id == 6)
        {
            float easing = moveTime;
            transform.position = Vector3.Lerp(currLine.currPos, new Vector3(transform.position.x, transform.position.y, transform.position.z-17), easing);
        }
        else
        {
            float easing = moveTime < 0.5 ? 16 * moveTime * moveTime * moveTime * moveTime * moveTime : 1 - Mathf.Pow(-2 * moveTime + 2, 5) / 2;
            transform.position = Vector3.Lerp(currLine.currPos, currLine.nextPos, easing);
        }
    }

    public void ChangeLineEnemy()
    {
        if (isWait && isLineChange && !successMove)
        {
            Line nextLine = currLine.nextLine;
            if (currLine == null || currLine.nextLine == null) return;
            if (currLine.nextLine.enemy == null)
            {
                nextLine.enemy = this;
                currLine.enemy = null;
                currLine = nextLine;

                successMove = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null && Player.live)
            {
                Destroy(gameObject);
                player.Stun(0.5f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == ("Finish"))
        {
            Destroy(gameObject);

        }
    }
}
