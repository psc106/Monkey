using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = default;
    private Rigidbody bulletRigidbody;
    public GameObject effect;
    public Transform effectPool { private get; set; }

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
                Instantiate(effect, transform.position, Quaternion.identity, effectPool);
                enemy.hp -= 1;
                Destroy(gameObject);
                GameManager.myCamera.Shake();


                if (enemy.hp <= 0){
                    enemy.Die();
                    GameManager.score += 1;
                }
            }
        }
    }
}
