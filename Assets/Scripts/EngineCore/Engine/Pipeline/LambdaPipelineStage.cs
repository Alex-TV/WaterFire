using Engine.Rules.Interfaces;
using System;
using Engine.Models.Interfaces;

namespace Engine.Pipeline
{
    /// <summary>
    ///   Lambda стайдж
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LambdaPipelineStage<T> : AbstractPipelineStage<T> where T : class, IGameStateEntity
    {
        public string Alias { get; protected set; }
        public Func<T, T> Lambda { get; protected set; }

        public LambdaPipelineStage(AbstractPipelineEngine engine, Func<T, T> lambda, T entity,
            IRule rule, string alias) : base(engine, entity, rule)
        {
            Lambda = lambda;
            Alias = alias;
        }

        protected override void Processing()
        {
            FinishStage(Lambda?.Invoke(Entity));
        }

        public override string ToShortString() => base.ToShortString(Alias);

        public override string ToLogName()
        {
            return base.ToLogName() + Alias;
        }
    }
}