using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {
    public int baseHealth;
    public int currentHealth;
    public float bloodGUIDuration;
    public Image bloodGUIColor;
    public AudioClip deathSound;
    private Color colorStart = Color.white;
    private Color colorEnd = Color.white;
    private PlayerController player;
    private Vector3 startScale;
    private Vector3 newScale;
    private AudioSource hitSound;

    // Use this for initialization
    void Start () {
        currentHealth = baseHealth;
        colorStart.a = 0;
        player = GetComponent<PlayerController>();
        startScale = transform.localScale;
        newScale = startScale * 1.5f;
        hitSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "lightSource")
        {
            DealDamage(1);
            //Destroy(collision.gameObject);
        }
    }
    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        
        StopCoroutine("showBloodGUI");
        
        player.playerAnimation.SetInteger("velocity", 0);
        player.stunned = true;
        if (currentHealth <= 0)
        {
            player.playerAnimation.SetBool("dead", true);
            hitSound.clip = deathSound;
            StartCoroutine("Death");
            hitSound.Play();
        }
        else
        {
            hitSound.Play();
            player.playerAnimation.SetBool("pain", true);
            StartCoroutine("showBloodGUI");
            transform.localScale = newScale;
        }
    }
    IEnumerator showBloodGUI()
    {
        float currentTime = Time.time;
        float lerp = 0;
        float direction = transform.localScale.x ;
        if (direction > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
       
        while (Time.time - currentTime <= bloodGUIDuration)
        {
            
            lerp = Mathf.PingPong(Time.time - currentTime, bloodGUIDuration / 2) / (bloodGUIDuration / 2);
            bloodGUIColor.color = Color.Lerp(colorStart, colorEnd, lerp);
            
            player.rb.velocity = new Vector2(direction * 0.5f, 0);
            yield return null;
        }
        bloodGUIColor.color = colorStart;
        player.playerAnimation.SetBool("pain", false);
        player.stunned = false;
        transform.localScale = startScale;

        //yield return new WaitForSeconds(bloodGUIDuration);
    }
    IEnumerator Death()
    {
        float currentTime = Time.time;
        float lerp = 0;
        while (Time.time - currentTime <= 3)
        {
            
            lerp = Mathf.PingPong(Time.time - currentTime, bloodGUIDuration / 2) / (bloodGUIDuration / 2);
            bloodGUIColor.color = Color.Lerp(colorStart, colorEnd, lerp);
            
            player.rb.velocity = new Vector2(0, 5f);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IncreaseScale()
    {
        transform.localScale = newScale;
    }
    public void DecreaseScale()
    {
        transform.localScale = startScale;
    }
}
