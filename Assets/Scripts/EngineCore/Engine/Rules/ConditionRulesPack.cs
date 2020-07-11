
using System;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;

namespace Engine.Rules
{
    /// <summary> Пак правил с условием </summary>
    public sealed class ConditionRulesPack<T> : RulesPack where T : class, IGameStateEntity
    {
        public ConditionRulesPack(params IRule[] initialRules) : base(initialRules)
        {
        }

        public void Applay(IGameStateModel model, AbstractPipelineEngine engine, Func<T, bool> condition, string trace = null)
        {
            if (!condition.Invoke(model.TryGetEntity<T>())) return;
            base.Applay(model, engine, trace);
        }
    }
}