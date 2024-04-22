using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public int Health;
    public float Speed;
    public bool Dir;//true - Right, false - Left
    private float _dir;

    private bool InAir = false;
    private bool CanMove = true;

    private int LayerIgnoreRaycast;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform tf;
    [SerializeField] private Transform spriteTf;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer MonsterSprite;
    private bool _FadeOut = false;
    
    private void Start() 
    {
        LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        _dir = (Dir ? 1f : -1f);
        spriteTf.localScale = new Vector3(spriteTf.localScale.x * _dir, spriteTf.localScale.y, spriteTf.localScale.z);
    }

    private void FixedUpdate() 
    {
        if(_FadeOut)
        {
            MonsterSprite.color -= new Color(0, 0, 0, Time.deltaTime);
            if (MonsterSprite.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }


        if(!InAir && Health > 0 && CanMove)
        {
            animator.SetBool("IsWalking", true);
            rb.velocity = new Vector2(_dir * Speed, rb.velocity.y);
        }

        if(!InAir && Health <= 0 && CanMove)
        {
            CanMove = false;
            GameObject.Find("Info").GetComponent<Info>().Kill();

            //Death Anim
            _FadeOut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(),other.GetComponent<Collider2D>());
        }

        if(other.gameObject.CompareTag("Player"))   
        {
            animator.SetBool("IsWalking", false);
            GameObject.Find("Info").GetComponent<Info>().TakeDamage();
            gameObject.layer = LayerIgnoreRaycast;
            rb.velocity = Vector3.zero;
            CanMove = false;

            //Scream Anim
            _FadeOut = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Floor") && InAir)
        {
            gameObject.layer = 0;
            InAir = false;
        }
    }

    public void Hit()
    {
        if(!InAir)
        {
            animator.SetBool("IsWalking", false);
            gameObject.layer = LayerIgnoreRaycast;
            InAir = true;
            Health -= 1;
            rb.velocity = new Vector2(-_dir * 4, 4);
        }
    }
}