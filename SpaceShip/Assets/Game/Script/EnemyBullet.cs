using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = default;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {

        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            Player player = other.GetComponent<Player>();


            if (player != null && Player.live)
            {
                player.Hit(1);
                Destroy(gameObject);

                if (player.hp <= 0)
                {
                    player.Die();

                    //other.gameObject.SetActive(false);
                    //Destroy(other.gameObject);
                }
            }
        }

        if (other.tag == ("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
