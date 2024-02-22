using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWireJumpState : PlayerState
{
    private GameObject hook;
    public PlayerWireJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, GameObject _hook) : base(_player, _stateMachine, _animBoolName)
    {
        hook = _hook;
    }

    public override void Enter()
    {
        base.Enter();

        SoundManager.Instance.SetEffectSound("WireJump");

        Time.timeScale = 1.5f;

        rb.isKinematic = false;
        player.isAttach = false;
        player.isHookActive = false;
        player.isLineMax = false;
        hook.SetActive(false);

       player.SetCoroutine("WireJump");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

 
}
