using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = default;
    private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = new Vector3(0,0,1) * speed;
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();


            if (enemy != null)
            {
                enemy.hp -= 1;
                Destroy(gameObject);

                if(enemy.hp <= 0){
                    Destroy(other.gameObject);
                    GameManager.score += 1;
                }
            }
        }
    }
}
