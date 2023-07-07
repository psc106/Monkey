using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnerID = default;
    public EnemyPool enemyPool = default;

    public Line[] queue { get; private set; }

    private Enemy[] Enemies = default;
    private float timeAfterSpawn = default;
    private bool canAdd = default;
    private float spawnerRate = default;


    void Start()
    {
        queue = new Line[30];

        queue[queue.Length - 1] = new Line(this, queue.Length - 1);
        for (int i = queue.Length - 2; i >= 0; i--)
        {
            queue[i] = new Line(this, i);
            queue[i].nextLine = queue[i + 1];
        }

        Enemies = enemyPool.pools.ToArray();

        spawnerRate = Random.Range(3, 6);
        timeAfterSpawn = -3;

        canAdd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.live)
        {
            if (!canAdd)
            {
                timeAfterSpawn += Time.deltaTime;

                if (spawnerRate <= timeAfterSpawn)
                {
                    timeAfterSpawn = 0;
                    spawnerRate = Random.Range(3, 6);
                    canAdd = true;
                }
            }
            else
            {
                if (queue[0].enemy == null)
                {
                    int enemyID = Random.Range(0, Enemies.Length);
                    GameObject enemy = Instantiate(Enemies[(int)enemyID].gameObject, new Vector3(transform.position.x, 0f, transform.position.z), Quaternion.identity, enemyPool.transform);
                    enemy.transform.LookAt(new Vector3(transform.position.x, 0, transform.position.z - 1));
                    queue[0].enemy = enemy.GetComponent<Enemy>();
                    queue[0].enemy.currLine = queue[0];
                    queue[0].enemy.id = enemyID;
                }
                canAdd = false;
            }
        }

    }


}

public class Line
{
    //스포너 아이디
    private int id;

    //이동 좌표
    public Vector3 currPos { get; private set; }
    public Vector3 nextPos { get; private set; }

    //안에 있는 적
    public Enemy enemy = default;
    public Line nextLine = default;

    public Line(Spawner spawner, int i)
    {
        id = spawner.spawnerID;
        currPos = new Vector3(spawner.transform.position.x, 0f, spawner.transform.position.z - (i * 4));
        nextPos = new Vector3(spawner.transform.position.x, 0f, spawner.transform.position.z - ((i + 1) * 4));
    }

}
