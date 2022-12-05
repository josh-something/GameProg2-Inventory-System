using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameprog2.Player
{
    public class TopDownPlayerMovement : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        private InputAction _moveAction;
        [SerializeField]
        private float _moveSpeed;

        private void Awake()
        {
            
        }
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        
        void Update()
        {
            Vector2 moveInput = _moveAction.ReadValue<Vector2>();
            _rigidbody2D.velocity = moveInput * _moveSpeed;
        }
    }

}