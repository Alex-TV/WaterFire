
using System;
using Engine.Rules.Interfaces;
using System.Collections.Generic;
using Engine.Models.Interfaces;

namespace Engine.Pipeline.Interfaces
{
    public interface IPipelineStage : ITracing, ILoged 
    {
        event EventHandler<IGameStateEntity> CompleteEvent;

        /// <summary>Условие выполнения стаэйджа, если не задано или вернет true то стайжд будет выполнен</summary>
        Func<bool> СonditionProcessing { get; set; }

        string[] Tags { get; set; }
        IRule Producer { get; }

        /// <summary> Текущее состояние стадии. </summary>
        PipelineStates State { get; }

        /// <summary> Элементы, которые ещё не выполнились. Эта стадия ждёт их окончания. </summary>
        IEnumerable<IPipelineStage> Previous { get; set; }

        /// <summary> Добавлять В хранилищ предыдущих пачками. </summary>
        IPipelineStage AddPreviouse(IEnumerable<IPipelineStage> stages);

        /// <summary> Добавлять в хранилища предыдущих только через этот метод. </summary>
        IPipelineStage AddPreviouse(IPipelineStage stage);

        /// <summary> Сахарок для добавления нескольких элементов. </summary>
        IPipelineStage AddPreviouse(IPipelineStage stage, params IPipelineStage[] others);

        /// <summary> Убирать из хранилища предыдущих только через этот метод. </summary>
        void RemovePreviouse(IPipelineStage stage);

        void Ready();

        string ToShortString();
    }
}