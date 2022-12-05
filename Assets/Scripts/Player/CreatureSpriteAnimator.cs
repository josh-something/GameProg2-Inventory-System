using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpriteAnimator : MonoBehaviour
{
    protected Animator _animator;
    protected Rigidbody2D _rigidbody2D;
    public bool IsFacingRight = true;

    [SerializeField] string MovingParam;
    [SerializeField] 

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if ( _rigidbody2D.velocity.magnitude > .3 ) { _animator.SetBool(MovingParam, true); }
        else { _animator.SetBool(MovingParam, false); }

        FaceTowardsMovement();
    }

    void FaceTowardsMovement()
    {
        float horizontalSpeed = _rigidbody2D.velocity.x;
        if (horizontalSpeed == 0) { return; }
        {
            if ((!IsFacingRight && horizontalSpeed > 0.3f) ||
                (IsFacingRight && horizontalSpeed < -0.3f))
            {
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
                IsFacingRight = !IsFacingRight;
            }
        }
    }
}
