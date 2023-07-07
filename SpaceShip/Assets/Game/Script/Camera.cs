using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        transform.LookAt(new Vector3 (playerTransform.position.x, playerTransform.position.y-5, playerTransform.position.z+10));
    }
}
