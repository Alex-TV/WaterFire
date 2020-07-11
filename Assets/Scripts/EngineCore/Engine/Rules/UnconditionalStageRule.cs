using System;
using Engine.Models.Interfaces;
using Engine.Pipeline;

namespace Engine.Rules
{
    // TODO: summary
    public sealed class UnConditionalStageRule<T> : Rule<T> where T  : class, IGameStateEntity
    {
        private readonly Action<T, PipelineEngine, Rule<T>> _stageFactory;

        public UnConditionalStageRule(Action<T, PipelineEngine, Rule<T>> stageFactory, string alias)
        {
            _stageFactory = stageFactory;
            Alias = alias;
        }

        public string Alias { get; private set; }

        public override void CheckRule(T entity, PipelineEngine engine)
        {
            _stageFactory?.Invoke(entity, engine, this);
        }
    }
}