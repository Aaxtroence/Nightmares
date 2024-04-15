using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform PlayerTF;
    [SerializeField] private Animator animator;
    [SerializeField] private Info Info;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] HitSounds;// 0 - miss, 1 - hit

    private bool Dir;//true - Right, false - Left
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Dir = false;
            Swap();
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Dir = true;
            Swap();
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        Vector2 rayOrigin = PlayerTF.position + (Dir ? (Vector3)PlayerTF.right : (Vector3)Vector2.left) * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, (Dir ? (Vector3)PlayerTF.right : (Vector3)Vector2.left), 1.75f);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            PlaySound(1);
            hit.collider.gameObject.GetComponent<MonsterScript>().Hit();
            Info.Combo++;
            Info.UpdateCombo();
        }
        else
        {
            PlaySound(0);
            Info.Combo = 0;
            Info.UpdateCombo();
            Info.TakeDamage();
        }
    }

    private void PlaySound(int Index)
    {
        audioSource.clip = HitSounds[Index];
        audioSource.Play();
    }

    private void Swap()
    {
        Vector3 newScale = PlayerTF.localScale;
        newScale.x = Dir ? Mathf.Abs(newScale.x) : -Mathf.Abs(newScale.x);
        PlayerTF.localScale = newScale;
    }
}
