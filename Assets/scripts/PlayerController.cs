using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using CnControls;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Color phantomColor;
    public Rigidbody2D rb;
    public GameObject dotlinePrefab;
    public IUsable usable;
    public GameObject ghostModeEffect;
    public float flySpeed;
    public bool phantomMode
    {
        get { return isPhantom; }
        set
        {
            if (isPhantom == value)
            {
                return;
            }

            isPhantom = value;
            if (value)
            {
                ghostModeEffect.SetActive(true);
                playerAnimation.SetInteger("velocity", 0);
            }
            else
            {
                ghostModeEffect.SetActive(false);
            }

            if (EnteredPhantomMode != null)
            {
                EnteredPhantomMode();
            }
        }
    }

    public bool insideObject
    {
        get { return inObject; }
        set
        {
            if (inObject == value)
            {
                return;
            }

            inObject = value;
            if (value == true)
            {
                sprite.enabled = false;
            }
            else
            {
                sprite.enabled = true;
            }
            if(ObjectInteraction != null)
            {
                ObjectInteraction();
            }
        }
    }
    public Animator playerAnimation;
    public bool stunned;

    private playerHealth health;
    private SpriteRenderer sprite;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isPhantom;
    private bool inObject;
    private bool isOnLadder;
    private Vector2 vertical;
    private Color defaultColor;
    private SpriteRenderer playerSprite;
    private float b_gravity;
    private enemyStateController enemy;
    private GameObject dotLine;
    private Vector3 scale;
    private LineRenderer dotLineScript;

    // Use this for initialization
    void Start()
    {
        stunned = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = playerSprite.color;
        phantomMode = false;
        playerAnimation = gameObject.GetComponent<Animator>();
        health = GetComponent<playerHealth>();
        scale = transform.localScale;
    }

	// Update is called once per frame
	void FixedUpdate()
    {
        if(!insideObject&&!phantomMode&&!stunned)
        {
            Controller2d();
        }
    }

    void Update()
    {
        NewController();
    }

    private void Use()
    {
        if (usable != null)
        {
            usable.Use(this);            
            insideObject = true;
        }
    }

    private void HitEnemy()
    {
        if(enemy != null)
        {
            enemy.DealDamage();
        }
    }

    private void Controller2d()
    {
        float x = CnInputManager.GetAxis("Horizontal")/2;
        float y = CnInputManager.GetAxis("Vertical");

        if (x > 1)
        {
            x = 1;
        }
        if(Input.GetKey(KeyCode.E))
        {
            playerAnimation.SetInteger("velocity", 3);
        }
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        if (x > 0)
        {
            transform.localScale = scale;
            playerAnimation.SetInteger("velocity", 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            playerAnimation.SetInteger("velocity", 1);
        }
        else
        {
            playerAnimation.SetInteger("velocity", 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            playerAnimation.SetInteger("velocity", -1);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            phantomMode = true;
        }
    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))
        {
            phantomMode = false;
        }
    }

    private void NewController()
    {
        if (phantomMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dotLine = Instantiate(dotlinePrefab, transform.position, Quaternion.identity);     
                dotLineScript = dotLine.GetComponent<LineRenderer>();
                lightCheck check = dotLine.gameObject.GetComponent<lightCheck>();
                check.ExitPM += ExitPM;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 start = this.transform.position;
                target.z = -1;
                start.z = -1;

                if (dotLine != null)
                {
                    dotLineScript.SetPosition(0, start);
                    dotLineScript.SetPosition(1, target);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                
                /*if(dotLineScript.CanFly())
                {
                    return;
                }*/

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;

                    if (hitObject.tag == "usable")
                    {
                        usable = hitObject.GetComponent<IUsable>();
                        startPos = transform.position;
                        endPos = hitObject.transform.position;
                        StartCoroutine(flyIntoObject());
                    }
                }
                
                if (dotLine != null)
                {
                    Destroy(dotLine.gameObject);
                }
                phantomMode = false;
            }
        }
        if(!phantomMode&&dotLine != null)
        {
           Destroy(dotLine.gameObject);
        }
    }

    private void changePlayerSorting(bool tumbler)
    {
        if (tumbler)
        {
            playerSprite.sortingOrder = playerSprite.sortingOrder - 2;
        }
        else
        {
            playerSprite.sortingOrder = playerSprite.sortingOrder + 2;
        }
    }

    private void ExitPM()
    {
        phantomMode = false;
    }

    public void ExitObject()
    {
        insideObject = false;
        transform.parent = null;
        usable = null;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "lightSource"&&!insideObject)
        {
            //Death();
        }
    }

    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public event Action ObjectInteraction;

    public event Action EnteredPhantomMode;

    IEnumerator flyIntoObject()
    {
        bool t = false;
        float timer = Time.time;
        rb.simulated = false;
        Vector3 a = endPos - startPos;
        Vector3 direction = a / a.magnitude; 
        while ((transform.position - endPos).magnitude > 0.5f)
        {
            transform.position = transform.position + direction*flySpeed;
            yield return null;
        }
        transform.position = endPos;
        Use();
        yield return new WaitForEndOfFrame();
    }
}
