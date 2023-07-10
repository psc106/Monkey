using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;

    private Transform playerTransform;

    private Vector3[] shakingValue;
    private int shakingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        shakingIndex = 0;

        shakingValue = new Vector3[5];
        shakingValue[0] = new Vector3(0, 0, 0);
        shakingValue[1] = new Vector3(0, 0, .2f);
        shakingValue[2] = new Vector3(0, 0, -.2f);
        shakingValue[3] = new Vector3(0, .2f, 0);
        shakingValue[4] = new Vector3(0, -.2f, 0);

        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        if (shakingIndex > 0)
        {
            transform.LookAt((new Vector3(playerTransform.position.x, playerTransform.position.y - 5, playerTransform.position.z + 10)) + shakingValue[shakingIndex]);
            shakingIndex+=1;
            if (shakingIndex > 4)
            {
                shakingIndex = 0;
            }
        }
        else
        {
            transform.LookAt(new Vector3 (playerTransform.position.x, playerTransform.position.y-5, playerTransform.position.z+10));
        }
    }
    // Update is called once per frame
    public void Shake()
    {
        shakingIndex = 1;
    }
}
