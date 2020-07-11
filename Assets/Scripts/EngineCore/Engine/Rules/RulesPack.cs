using Engine.Pipeline;
using Engine.Rules.Interfaces;
using System.Collections.Generic;
using Engine.Models;
using Engine.Models.Interfaces;

namespace Engine.Rules
{
    public class RulesPack
    {
        /// <summary>
        ///     Сверхкостыль. Если эту переменную выставят откуда-нибудь вся обработка рулепака упадёт в трейс. Использую для
        ///     отладки пользовательского ввода, чтобы не смотреть на 100500 одинаковых строк в логе.
        /// </summary>
        public static string ExtremeCrutch;

        public List<IRule> RuleList = new List<IRule>();

        public RulesPack(params IRule[] initialRules)
        {
            RuleList.AddRange(initialRules);
        }

        /// <summary>
        /// Запустить на выполнние
        /// </summary>
        /// <param name="model">модел откуда будут боатся entity</param>
        /// <param name="engine">движок</param>
        public void Execute(IGameStateModel model, AbstractPipelineEngine engine)
        {
            foreach (var rule in RuleList)
            {
                rule.CreatePipeline(model, engine);
            }
        }

        /// <summary> Строит массив матчей, пополняет на их основе конвейер и запускает его. </summary>
        /// <param name="engine"></param>
        /// <param name="trace">
        ///     Если этот параметр выставлен в какой-нибудь не null то все матчи и состояние движка после
        ///     пополнения пайплайна будет скинуто в дебаг.
        /// </param>
        /// <param name="model"></param>
        public virtual void Applay(IGameStateModel model, AbstractPipelineEngine engine, string trace = null)
        {
            Execute(model, engine);
            if (ExtremeCrutch != null)
            {
                trace = ExtremeCrutch;
                ExtremeCrutch = null;
            }

            engine.PipelineStageExecuted = entity => model.AddEntity(entity);
            // Кинем в трейс всю кучу матчей для общего образования.
            engine.ExecuteAll();
        }
    }
}