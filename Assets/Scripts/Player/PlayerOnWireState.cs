using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerOnWireState : PlayerState
{
    #region component
    private LineRenderer line;
    private GameObject hook;
    #endregion

    public PlayerOnWireState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, GameObject _hook, LineRenderer _line) : base(_player, _stateMachine, _animBoolName)
    {
        line = _line;
        hook = _hook;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetCoroutine("OnWire");
    }

    public override void Exit()
    {
        base.Exit();

        player.isHookActive = false;
        player.isLineMax = false;
        hook.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, hook.transform.position);
    }


}
