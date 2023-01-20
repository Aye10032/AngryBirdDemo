using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bird : MonoBehaviour
{
    private bool _isClick = false;
    [HideInInspector] public SpringJoint2D sp;
    private Rigidbody2D _rigidbody;
    private BirdTrail _trail;

    public Transform rightPos;
    public Transform leftPos;
    public LineRenderer left;
    public LineRenderer right;
    public float maxDis = 1.3f;
    public GameObject boom;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trail = GetComponent<BirdTrail>();
    }

    private void OnMouseDown()
    {
        sp.enabled = true;
        this.enabled = true;
        _isClick = true;
        _rigidbody.isKinematic = true; //关闭物理计算
        _rigidbody.gravityScale = 1;
    }

    private void OnMouseUp()
    {
        _isClick = false;
        _rigidbody.isKinematic = false;
        Invoke(nameof(Fly), 0.1f); //延时调用

        right.enabled = false;
        left.enabled = false;
    }

    private void Update()
    {
        if (_isClick)
        {
            transform.position = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.position += new Vector3(0, 0, 10);
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized; //单位化取方向向量
                pos *= maxDis;
                // ReSharper disable once Unity.InefficientPropertyAccess
                transform.position = pos + rightPos.position;
            }

            DrawLine();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _trail.TrailClean();
    }

    void Fly()
    {
        _trail.TrailStart();
        sp.enabled = false;
        Invoke(nameof(Next), 5f);
    }

    void DrawLine()
    {
        right.enabled = true;
        left.enabled = true;
        var position = transform.position;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, position); //index:直线的第几个点

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, position);
    }

    void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }
}