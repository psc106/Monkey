using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerPool : MonoBehaviour
{
    public List<Spawner> spawners;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<Spawner>().ToList();        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
