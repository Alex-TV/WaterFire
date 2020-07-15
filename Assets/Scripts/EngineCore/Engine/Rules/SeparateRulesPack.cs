
using System;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;

namespace Engine.Rules
{
    /// <summary> Выполняет все стейджи что внутри. </summary>
    public class SeparateRulesPack : IRule
    {
        public RulesPack Childs = new RulesPack();

        public SeparateRulesPack(params IRule[] rules)
        {
            Childs.RuleList.AddRange(rules);
        }

        /// <summary> Само по себе правило новых стэйджей не создаёт </summary>
        public void CreatePipeline(IGameStateModel model, AbstractPipelineEngine engine)
        {
            engine.PipelineStageExecuted = entity => Array.ForEach(entity, e => model.AddEntity(e));
            Childs.Execute(model, engine);
            engine.СonditionExecute(s => Childs.RuleList.Contains(s.Producer));
        }
    }
}