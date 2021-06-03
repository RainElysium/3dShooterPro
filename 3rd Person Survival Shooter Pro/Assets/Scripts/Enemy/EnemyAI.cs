using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }


    private CharacterController _controller;
    [SerializeField]
    private GameObject _target;
    private float _speed = 1f;
    [SerializeField]
    private Vector3 _velocity;
    private float _gravity = -1;
    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (!_controller)
            Debug.LogError("Character Controller not found!");
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Attack:
                    if (Time.time > _nextAttack)
                    {
                        _nextAttack = Time.time + _attackDelay;
                        DamageTarget();
                    }
                    break;
            case EnemyState.Chase:
                if (_target)
                    CalculateMovement();
                break;
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            Vector3 direction = _target.transform.position - transform.position;
            _velocity = direction * _speed;
            direction.y = 0;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction);
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void DamageTarget()
    {
        Debug.Log("Damaging");
        Health hp = _target.GetComponent<Health>();

        if (!hp)
            return;

        hp.Damage(10);
    }

    public void StartAttack(GameObject target)
    {
        _target = target;
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }
}
