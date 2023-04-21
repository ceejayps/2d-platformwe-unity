using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{

    public int damageAmount = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void OnTriggerEnter2D(Collider2D other) {
        
    if (other.gameObject.CompareTag("Player"))
        {
             Debug.Log("player");
            // Call the TakeDamage method on the player's health script
            other.gameObject.GetComponent<health>().TakeDamage(damageAmount);
        }

        Debug.Log("hit");
}
}