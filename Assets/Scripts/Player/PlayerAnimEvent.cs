using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();    
    }
    
    public void DeadAnimEnd()
    {
        player.Dead();
    }
}
