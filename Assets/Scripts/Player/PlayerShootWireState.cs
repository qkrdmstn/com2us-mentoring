using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerShootWireState : PlayerState
{
    #region component
    private LineRenderer line;
    private GameObject hook;
    #endregion

    public PlayerShootWireState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName, GameObject _hook, LineRenderer _line) : base(_player, _stateMachine, _animBoolName)
    {
        this.line = _line;
        this.hook = _hook;
    }

    public override void Enter()
    {
        base.Enter();

        //Line Setting
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, hook.transform.position);
        line.useWorldSpace = true;
        player.isAttach = false;

        //Hook Setting
        hook.transform.position = rb.transform.position;
        player.isHookActive = true;
        hook.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, hook.transform.position);

        if (player.isHookActive && !player.isLineMax)
        {
            hook.transform.Translate(player.dir.normalized * Time.deltaTime * player.hookSpeed);

            if (Vector2.Distance(rb.transform.position, hook.transform.position) > player.maxDist)
            {
                player.isLineMax = true;
            }
        }
        else if (player.isHookActive && player.isLineMax && !player.isAttach)
        {
            hook.transform.position = Vector2.MoveTowards(hook.transform.position, rb.transform.position, Time.deltaTime * player.hookSpeed);
            if (Vector2.Distance(rb.transform.position, hook.transform.position) < 0.1f)
            {
                player.isHookActive = false;
                player.isLineMax = false;
                hook.gameObject.SetActive(false);
                stateMachine.ChangeState(player.fallState);
            }
        }
        else if (player.isAttach)
        {
            Debug.Log("asd");
            float dist = Vector2.Distance(rb.transform.position, hook.transform.position);
            Vector2 invDir = -dist * player.dir;

            stateMachine.ChangeState(player.onWireState);
        }
    }
}
