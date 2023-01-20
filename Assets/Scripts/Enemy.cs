using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float minSpeed = 4f;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > maxSpeed) //relativeVelocity:相对速度(向量) magnitude:取模 
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude < maxSpeed && collision.relativeVelocity.magnitude > minSpeed)
        {
            _renderer.sprite = hurt;
        }
        // Debug.Log(collision.relativeVelocity.magnitude);
    }

    void Dead()
    {
        GameObject go = null;
        if (CompareTag("Pig"))
        {
            GameManager._instance.pigs.Remove(this);
            go = Instantiate(score, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else if (CompareTag("Block"))
        {
            go = Instantiate(score, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(go, 1.5f);
    }
}