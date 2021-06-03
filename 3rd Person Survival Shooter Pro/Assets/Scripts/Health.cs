using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHP;
    [SerializeField]
    private int _minHP;
    [SerializeField]
    private int _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    public void Damage(int dmgAmount)
    {
        _currentHP -= dmgAmount;

        if (_currentHP <= _minHP)
            Destroy(this.gameObject);
    }

}
