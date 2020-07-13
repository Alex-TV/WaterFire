
using System;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;
using UnityEngine;

namespace Engine.Rules
{
    /// <summary>
    ///     Правило-контейнер. Содержит в себе рулепак, который крутится до тех пор, пока не перестанет давать новых стайжей.
    /// </summary>
    public sealed class LoopRulesPack<T> : IRule  where T : class, IGameStateEntity
    {
        public RulesPack Childs = new RulesPack();

        /// <summary> Максимальное количество циклов, которые можно гонять пачку правил до аварйного выхода </summary>
        public int MaxLoopsCount = 300;

        public LoopRulesPack(params IRule[] rules)
        {
            Childs.RuleList.AddRange(rules);
        }

        /// <summary> Само по себе правило новых матчей не создаёт </summary>
        public void CreatePipeline(IGameStateModel model, AbstractPipelineEngine engine)
        {
            for (var loops = 0; loops < MaxLoopsCount; loops++)
            {
                var oldCount = engine.Stages.Count;
                engine.PipelineStageExecuted = entity => Array.ForEach(entity, e => model.AddEntity(e));
                Childs.Execute(model, engine);
                if (engine.Stages.Count == oldCount) return;
            }
            var strStages = "";
            engine.Stages.ForEach(s => strStages += $" Rule: {s.Producer} Stage: {s} name: {s.ToShortString()}\n");
            Debug.LogError(
                $"Произошёл аварийный выход из цикла правил после {MaxLoopsCount} циклов. В последнем цикле добавлены следующие pipeline:\n {strStages} ");
        }

        public void CheckRule(T state, PipelineEngine engine) { }
    }
}