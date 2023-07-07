using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public float speed = default;
    private Rigidbody enviromentRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        enviromentRigidBody = GetComponent<Rigidbody>();
        enviromentRigidBody.velocity = new Vector3(0, 0, -1) * speed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
