using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawner : MonoBehaviour
{
    public EnviromentPool pool;

    private Enviroment[] env = default;

    private float timeAfterSpawn = default;
    private float spawnerRate = default;


    // Start is called before the first frame update
    void Start()
    {
        env = pool.enviroments.ToArray();

        spawnerRate = Random.Range(.1f, 1f);
        timeAfterSpawn = spawnerRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (spawnerRate <= timeAfterSpawn)
        {
            timeAfterSpawn = 0;
            spawnerRate = Random.Range(.1f, 1f);

            int enemyID = Random.Range(0, env.Length);
            GameObject enemy = Instantiate(env[(int)enemyID].gameObject, new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.identity, pool.transform);
        }
    }
}

