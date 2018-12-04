﻿using UnityEngine;

public abstract class BaseEnv : ScriptableObject
{
    protected float epsilon = 0.9f;
    protected float alpha = 0.1f;
    protected float gamma = 0.9f;
    protected int last_r = 1;
    protected int[] last_state;
    protected BirdAction last_action = BirdAction.PAD;

    public virtual void Init()
    {
        EventHandle.AddCommandHook(COMMAND_TYPE.GAME_START, OnStart);
        EventHandle.AddCommandHook(COMMAND_TYPE.SCORE, OnScore);
        EventHandle.AddCommandHook(COMMAND_TYPE.GAME_OVERD, OnDied);
    }

    void OnStart(object o)
    {
        last_r = 0;
        last_state = null;
    }

    void OnScore(object arg)
    {
        last_r = 20;
    }

    void OnDied(object arg)
    {
        last_r = -100;
    }

    public virtual void OnApplicationQuit() { }

    public int[] GetCurrentState()
    {
#if ENABLE_PILLAR
        int[] p_st = PillarManager.S.GetPillarState();
        int b_st = GameMgr.S.mainBird.GetState();
        int[] rst = new int[3];
        rst[0] = p_st[0];
        rst[1] = p_st[1];
        rst[2] = b_st;
        return rst;
#else
        return new int[GameManager.S.mainBird.GetState()];
#endif
    }

    public virtual void OnUpdate(float delta) { }

    public abstract void OnTick();

    public abstract BirdAction choose_action(int[] state);

    public abstract void UpdateState(int[] state, int[] state_, int rewd, BirdAction action);
    
    public virtual void OnRestart(int[] state) { }

    public virtual void OnInspector() { }

}