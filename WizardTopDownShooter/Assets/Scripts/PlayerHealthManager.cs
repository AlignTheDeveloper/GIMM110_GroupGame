using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;

    public float negativeFeedbackLength;
    private float negativeFeedbackCounter;

    private Renderer render;
    private Color storedColor;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        render = GetComponent<Renderer>();
        storedColor = render.material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <=0)
        {
            gameObject.SetActive(false);
        }
        
        if(negativeFeedbackCounter > 0)
        {
            negativeFeedbackCounter -= Time.deltaTime;
        }

        if(negativeFeedbackCounter <= 0)
        {
            render.material.SetColor("_Color", storedColor);
        }

    }

    public void HurtPlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        negativeFeedbackCounter = negativeFeedbackLength;
        render.material.SetColor("_Color", Color.white);
    }
}
