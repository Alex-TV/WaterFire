
using Engine.Models;
using Engine.Rules;

namespace Engine.Pipeline
{
    /// <summary>
    /// Специальный стейдж для блокирования части конвеера до истечения какого-нибудь внешнего события, происходящего
    /// вне конвеера.
    /// </summary>
    public class PipelineLockerStage<T> : PipelineStage<T> where T : class, IGameStateEntity
    {
        public PipelineLockerStage(PipelineEngine engine, T entity, Rule<T> rule) : base(engine, entity, rule) { }
        protected override void Processing() { } // do nothing
        public void FinishLockerStage() => FinishStage(Entity);
    }
}