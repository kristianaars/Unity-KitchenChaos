using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private const string IS_WALKING_KEY = "IsWalking";

    [SerializeField] private Player player;
    
    private Animator _animator;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(IS_WALKING_KEY, player.IsWalking);
    }

    private void Update()
    {
        _animator.SetBool(IS_WALKING_KEY, player.IsWalking);
    }
}
