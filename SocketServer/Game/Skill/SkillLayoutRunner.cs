﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    class SkillLayoutRunner : GameObjectComponent
    {
        public SkillStateMachine stateMachine;

        public void Run()
        {
            foreach(var c in gameObject.GetChildren())
            {
                var logic = c.GetComponent<SkillLogicComponent>();
                logic.Run();
            }
        }
        /// <summary>
        /// 对目标NPC造成伤害
        /// </summary>
        public void DoDamage(GameObjectActor actor)
        {
            stateMachine.DoDamage(actor);
        }
    }
}