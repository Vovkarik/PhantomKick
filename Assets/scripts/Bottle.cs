using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class Bottle : MonoBehaviour, IUsable
{

    public float speed;
    public GameObject point;
    public float direction;
    public float force;
    public Vector3 range;
    private bool isPossessed = false;
    private Transform player;
    private PlayerController chrctr;
    private Vector3 scale;
    private bool activatedThisFrame;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private bool willDestroy;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    void Start()
    { 
       
        activatedThisFrame = false;
        rend = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        willDestroy = false;
    }

    void Update()
    {
        Swipe();
        if (Mathf.Abs(rb.velocity.y) > 3)
        {
            willDestroy = true;
        }
    }
    

    public void ChangeBoxState()
    {
        this.transform.parent = null;
        player.Rotate(Vector3.forward, -90);
        isPossessed = false;
        this.transform.localScale = scale;
        rb.simulated = true;
        Destroy(gameObject);
    }

    public void Use(PlayerController character)
    {
        if (!isPossessed)
        {
            player = character.transform;
            chrctr = character;
            scale = this.transform.localScale;
            isPossessed = true;
            activatedThisFrame = true;
            player.parent = transform;
            chrctr.rb.simulated = false;
        }
    }

    public void Highlight(bool t)
    {
        if (t)
        {
            rend.color = Color.blue;
        }
        else
        {
            rend.color = Color.white;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void BottleControls()
    {
        float x = CnInputManager.GetAxis("Horizontal") / 2;

        if (Mathf.Abs(rb.velocity.y) < 0.001 && x != 0)
        {
            rb.velocity = new Vector2(x * speed, 1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (willDestroy)
        {
            Collider2D[] Objects = Physics2D.OverlapCircleAll(rb.transform.position, 100);
            point.SetActive(true);
            point.transform.position = transform.position + range;
            foreach (Collider2D currObject in Objects)
            {
                if (currObject.gameObject.tag == "enemy")
                {
                    currObject.GetComponent<enemyScript>().RunToBottle(gameObject);
                }
            }
            Destroy(gameObject);
        }
    }

    public void ActiveAbility()
    {
        Debug.Log("w");
        chrctr.rb.simulated = true;
        LeaveObject();
        isPossessed = false;
        rb.AddForce(transform.right * force * direction);
    }

    public void LeaveObject()
    {
        chrctr.rb.simulated = true;
        chrctr.ExitObject();
    }

    public bool IsPossessed()
    {
        return isPossessed;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeaveObject();
        }
    }

    public bool HasAbility()
    {
        return true;
    }
    public void Swipe()
    {

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("up swipe");
                    LeaveObject();
                }
                //swipe down
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    Debug.Log("down swipe");
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("left swipe");
                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    Debug.Log("right swipe");
                }
            }
        }
    }
}
