
using Assets.Scripts.EngineCore.Engine.Pipeline;
using Engine.Models.Interfaces;
using Engine.Pipeline;
using Engine.Rules.Interfaces;

namespace Engine.Rules
{
    /// <summary>
    ///     общий предок всех воркеров, вызываемых для всех ситуаций на игровом поле. Хотя не факт ещё, что им потребуется
    ///     общий предок.
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
                new ReturnEntityPipeline<T>(engineInternal, entity, this);
            }
        }
    }
}