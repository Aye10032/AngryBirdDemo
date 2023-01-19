using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrail : MonoBehaviour
{
    public WeaponTrail myTrail;

    private float _t = 0.033f;
    private float _tempT = 0;
    private float _animationIncrement = 0.003f;

    void Start()
    {
        // 默认没有拖尾效果
        myTrail.SetTime(0.0f, 0.0f, 1.0f);
    }

    void LateUpdate()
    {
        _t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (_t > 0)
        {
            while (_tempT < _t)
            {
                _tempT += _animationIncrement;

                if (myTrail.time > 0)
                {
                    myTrail.Itterate(Time.time - _t + _tempT);
                }
                else
                {
                    myTrail.ClearTrail();
                }
            }

            _tempT -= _t;

            if (myTrail.time > 0)
            {
                myTrail.UpdateTrail(Time.time, _t);
            }
        }
    }

    public void TrailStart()
    {
        //设置拖尾时长
        myTrail.SetTime(2.0f, 0.0f, 1.0f);
        //开始进行拖尾
        myTrail.StartTrail(0.5f, 0.4f);
    }

    public void TrailClean()
    {
        //清除拖尾
        myTrail.ClearTrail();
    }
}