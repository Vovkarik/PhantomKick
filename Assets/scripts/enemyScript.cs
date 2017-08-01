using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public float speed;
    public GameObject start;
    public GameObject finish;
    public int direction;
    private Rigidbody2D rb;
    private float angle;

    //public Bottle bottle;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        angle = 180;
    }

    void Update()
    {
        Vector2 temp = rb.velocity;
        rb.velocity = new Vector3(direction * speed, temp.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == start.tag)
        {
            direction = -direction;
            transform.rotation = (direction < 0) ? Quaternion.Euler(new Vector3(0, 0, 0)) : Quaternion.Euler(new Vector3(0, angle, 0));
        }
        else if (other.tag == "bottlePoint")
        {
            speed = 0;
        }
    }

    public void RunToBottle(GameObject bottle)
    {
        direction = (bottle.transform.position.x - transform.position.x < 0) ? -1 : 1;
        transform.rotation = (direction < 0) ? Quaternion.Euler(new Vector3(0, 0, 0)) : Quaternion.Euler(new Vector3(0, angle, 0));

        speed += 1;
        start.SetActive(false);
        finish.SetActive(false);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}