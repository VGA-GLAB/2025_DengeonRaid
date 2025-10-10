using System;
using UnityEngine;

public class Figures : MonoBehaviour
{
    public int _wallet;
    public int _playerHP;

    public bool _death;

    void Start()
    {
        
    }

    void Update()
    {
        if (_playerHP <= 0)
        {
            _death = true;
        }
        else
        {
            _death = false;
        }
    }
}
