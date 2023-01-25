using System;
using System.Collections;
using System.Collections.Generic;
using Lockstep.Math;
using RVO;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class GameAgent : MonoBehaviour
{
    [HideInInspector] public int sid = -1;

    /** Random number generator. */
    private Random m_random = new Random();

    void Update()
    {
        if (sid >= 0)
        {
            LFloat2 pos = Simulator.Instance.getAgentPosition(sid);
            LFloat2 vel = Simulator.Instance.getAgentPrefVelocity(sid);
            transform.position = new float3(pos.x.ToFloat(), transform.position.y, pos.y.ToFloat());
            if (Math.Abs(vel._val.x) > 1 && Math.Abs(vel._val.y) > 1)
                transform.forward = math.normalize(new float3(vel.x.ToFloat(), 0, vel.y.ToFloat()));
        }

        if (!Input.GetMouseButton(1))
        {
            Simulator.Instance.setAgentPrefVelocity(sid, LFloat2.zero);
            return;
        }

        LFloat2 goalVector = GameMainManager.Instance.mousePosition - Simulator.Instance.getAgentPosition(sid);
        if (RVOMath.absSq(goalVector) > LFloat.one)
        {
            goalVector = RVOMath.normalize(goalVector);
        }

        Simulator.Instance.setAgentPrefVelocity(sid, goalVector);

        /* Perturb a little to avoid deadlocks due to perfect symmetry. */
        LFloat angle = new LFloat(m_random.NextInt()) * new LFloat(2)* LMath.PI;
        LFloat dist = new LFloat(m_random.NextInt()) * LFloat.EPSILON;

        Simulator.Instance.setAgentPrefVelocity(sid, Simulator.Instance.getAgentPrefVelocity(sid) +
                                                     dist *
                                                     new LFloat2(LMath.Cos(angle), LMath.Sin(angle)));
    }
}