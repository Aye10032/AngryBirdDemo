using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Enemy> pigs;
    public GameObject win;
    public GameObject loss;
    public static GameManager _instance;

    private Vector3 _originPos;

    private void Awake()
    {
        _instance = this;
        if (birds.Count != 0)
        {
            _originPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[0].gameObject.transform.position = _originPos;
            }
            birds[i].enabled = false;
            birds[i].sp.enabled = false;
        }
    }

    public void NextBird()
    {
        Debug.Log(pigs.Count);
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只
                Init();
            }
            else
            {
                //输
                loss.SetActive(true);
                Debug.Log("loss!");
            }
        }
        else
        {
            //赢
            win.SetActive(true);
            Debug.Log("win!");
        }
    }

    public void ShowStar()
    {
        
    }
}