﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    public enum AIStateEnum
    {
        IDLE,
        MOVE,
        ATTACK,
    }

    public class AIState
    {
        public AIStateEnum type;
        public AICharacter aiCharacter;
        protected bool inState = false;

        /// <summary>
        /// 添加完状态之后首次初始化
        /// </summary>
        public virtual void Init()
        {

        }
        public virtual void EnterState()
        {
            inState = true;
        }
        public virtual void ExitState()
        {
            inState = false;
        }


        public virtual void OnEvent(AIEvent evt)
        {

        }

        public virtual async Task RunLogic()
        {

        }

    }

    public class IdleState : AIState
    {
        public IdleState()
        {
            type = AIStateEnum.IDLE;
        }
    }
    public class MoveState : AIState
    {
        public MoveState()
        {
            type = AIStateEnum.MOVE;
        }
    }
    public class AttackState : AIState
    {
        public AttackState()
        {
            type = AIStateEnum.ATTACK;
        }
    }
}
