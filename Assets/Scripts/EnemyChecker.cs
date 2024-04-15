using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChecker : MonoBehaviour
{
    [SerializeField] private Image image;
    private List<Collider2D> enemyColliders = new List<Collider2D>();
    
    private int LayerIgnoreRaycast;

    private void Start() 
    {
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        UpdateImageColor();
    }

    private void Update()
    {
        for(int i = 0; i == enemyColliders.Count; i++)
        {
            if(enemyColliders.Count > 0)
            {
                if(enemyColliders[i].gameObject.layer == LayerIgnoreRaycast)
                {
                    enemyColliders.Remove(enemyColliders[i]);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyColliders.Add(other);
            UpdateImageColor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyColliders.Remove(other);
            UpdateImageColor();
        }
    }

    private void UpdateImageColor()
    {
        if (enemyColliders.Count > 0)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.red;
        }
    }
}
