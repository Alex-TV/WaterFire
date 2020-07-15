
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;

namespace Engine.Rules
{
    /// <summary>
    ///  Общий предок всех воркеров, вызываемых для всех ситуаций на игровом поле. Хотя не факт ещё, что им потребуется
    /// </summary>
    public abstract class Rule<T> : IRule where T : class, IGameStateEntity
    {
        public abstract bool CheckRule(T entity, PipelineEngine engine);

        public virtual void CreatePipeline(IGameStateModel model, AbstractPipelineEngine engine)
        {
            var entity = model.TryGetEntity<T>();
            var engineInternal = engine as PipelineEngine;
            if (!CheckRule(entity, engineInternal))
            {
                new LambdaPipelineStage<T>(engine, t => t, entity, this, "Return entity pipeline");
            }
        }
    }
}