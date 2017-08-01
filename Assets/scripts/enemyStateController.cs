using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStateController : MonoBehaviour
{

    public enum enemyState {none, disabled, kamushek};

    public enemyState currentState
    {
        get { return state; }
        set
        {
            if (state == value) return;
            state = value;
            if (state == enemyState.disabled)
            {
                script.enabled = false;
                gameObject.transform.Rotate(Vector3.forward, 90);
            }
            else if (state == enemyState.none)
            {
                script.enabled = true;
                gameObject.transform.Rotate(Vector3.forward, 90);
            }
        }
    }

    public Color disabledColor;
    public Color deadColor;
    private enemyState state;
    private enemyScript script;
    private SpriteRenderer enemySprite;

	void Start ()
    {
        script = gameObject.GetComponent<enemyScript>();
        enemySprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
		
	}

    public void DealDamage()
    {
        switch(currentState)
        {
            case enemyState.none:
                {
                    currentState = enemyState.kamushek;
                    enemySprite.color = deadColor;
                    script.DestroyEnemy();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
