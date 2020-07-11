using System;
using Engine.Pipeline.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Engine.Models.Interfaces;

namespace Engine.Pipeline
{
    /// <summary>
    /// Тут происходит вызов Pipeline и вся работа.
    /// </summary>
    public abstract class AbstractPipelineEngine
    {
        private bool _logInstant = false;
        private string _logMessage = "";
        public Dictionary<string, int> CountLog = new Dictionary<string, int>();
        public Dictionary<string, long> TimeLog = new Dictionary<string, long>();

        private string _prefix = null;
        public Action PipelineEngineExecuted;
        public Action<IGameStateEntity> PipelineStageExecuted;

        public List<IPipelineStage> Stages { get; } = new List<IPipelineStage>();

        public void AddCreatedStage(IPipelineStage stage)
        {
            stage.CompleteEvent += OnCompleteStage;
            stage.Log += AddLogString;
            stage.LogStages = _prefix != null || _logInstant;
            Stages.Add(stage);
        }

        private void OnCompleteStage(object source, IGameStateEntity entity)
        {
            var stage = source as IPipelineStage;
            stage.CompleteEvent -= OnCompleteStage;
            stage.Log -= AddLogString;
            Stages.Remove(stage);
            PipelineStageExecuted?.Invoke(entity);

            if (Stages.Count == 0)
            {
                PipelineEngineExecuted?.Invoke();
                if (_prefix != null || _logInstant || _logMessage != "")
                {
                    Debug.Log(_prefix + " PIPELINE EXECUTION\n" + _logMessage + "\n");
                    _logMessage = "";
                }
            }
        }

        private void OnCancelStage(object source, IGameStateEntity entity)
        {
            OnCompleteStage(source, entity);
        }

        /// <summary> HACK хаменяет стейдж который уже есть в системе, в том числе уже может выполняться, на другой стейдж. </summary>
        public void ReplaceStage(IPipelineStage targetToRemove, IPipelineStage newStage)
        {
            if (newStage.State != PipelineStates.Ready)
            {
                Debug.LogError("ReplaceStage(" + targetToRemove + "," + newStage
                               + ") // Новый стейдж находится в неподходящем состоянии для этой и так кривой операции");
                return;
            }
            // Все, кто был за этим элементом в очереди больше не помеха.
            foreach (var stage in Stages.Where(stage => stage.Previous.Contains(newStage)))
            {
                stage.RemovePreviouse(newStage);
            }
            // И самому ему никаких предыдущих больше не надо.
            while (newStage.Previous.Any())
            {
                newStage.RemovePreviouse(newStage.Previous.First());
            }
            // Теперь его предыдущие это предыдущие тех, кого он заменяет.
            foreach (var stage in targetToRemove.Previous)
            {
                newStage.AddPreviouse(stage);
            }
            // И сам он предыдущий тем, кому раньше был предыдущим заменяемый.
            foreach (var stage in Stages.Where(stage => stage.Previous.Contains(targetToRemove)))
            {
                stage.AddPreviouse(newStage);
            }
        }

        public void ExecuteAll()
        {
            var dump = Stages.ToArray();
            foreach (var stage in dump)
            {
                if (stage.State == PipelineStates.Prepearing)
                {
                    stage.Ready();
                }
            }
        }

        /// <summary>
        ///     Выполняет те Stage которые удовлетворяют условию
        /// </summary>
        /// <param name="condition"></param>
        public void СonditionExecute(Func<IPipelineStage, bool> condition)
        {
            var conditionStage = Stages.FindAll(condition.Invoke);
            foreach (var stage in conditionStage)
            {
                if (stage.State == PipelineStates.Prepearing)
                {
                    stage.Ready();
                }
            }
        }

        /// <summary> Заставляет все стадии срать в лог информацией о том, что они меняют состояния. Удобно для отладки. </summary>
        /// <param name="flag"></param>
        public void LogStages(string prefix)
        {
            _logInstant = false;
            _prefix = prefix;
            var flag = _prefix != null;
            Stages.ForEach(stage => stage.LogStages = flag);
        }

        public void LogStagesInstant(bool flag)
        {
            _logInstant = flag;
            _prefix = null;
            Stages.ForEach(stage => stage.LogStages = flag);
        }

        private void AddLogString(string msg)
        {
            if (_logInstant)
            {
                Debug.Log(msg);
            }
            _logMessage += msg + "\n";
        }

        public string ToDetailedString()
        {
            var msg = "";
            var oldOrder = Stages.ToList();
            var newOrder = new List<IPipelineStage>();
            while (oldOrder.Count > 0)
            {
                var filtered = oldOrder.Where(stage => !stage.Previous.Intersect(oldOrder).Any());
                if (!filtered.Any() && oldOrder.Count > 0)
                {
                    oldOrder = oldOrder.OrderBy(stage => stage.UniqueId).ToList();
                    Debug.LogError(
                        "Имеющиеся в PipelineEngine элементы зациклены и потому не могут выполняться!\nЭлементы которые не удаётся запустить:\n"
                        + StageListToString(oldOrder, true));
                    // У нас имеется замкнутый цикл
                    int prevCount;
                    do
                    {
                        prevCount = oldOrder.Count;
                        oldOrder = oldOrder.SelectMany(stage => stage.Previous).Distinct().ToList();
                    }
                    while (oldOrder.Count < prevCount);
                    oldOrder = oldOrder.OrderBy(stage => stage.UniqueId).ToList();
                    Debug.LogError(
                        "Имеющиеся в PipelineEngine элементы зациклены и потому не могут выполняться!\nЗациклены элементы:\n"
                        + StageListToString(oldOrder, true));
                    // Повыкидывать всех, кто не является кому-нибудь предком, чтобы просто в списке остались только те, кто образует цикл.
                    msg += " Без сортировки:\n";
                    newOrder = Stages.ToList();
                    break;
                }
                var filteredItem = filtered.First();
                newOrder.Add(filteredItem);
                oldOrder.Remove(filteredItem);
            }
            msg += StageListToString(newOrder, false);
            return msg;
        }

        private static string StageListToString(IEnumerable<IPipelineStage> list, bool withProducer)
        {
            var msg = "";
            foreach (var stage in list)
            {
                msg += msg != string.Empty ? "\n" : "";
                msg += stage.Previous.Aggregate(string.Empty,
                    (accumulated, prev) => accumulated + (accumulated != string.Empty ? ", " : string.Empty) + prev.UniqueId);
                msg += " > " + stage.ToShortString();
            }
            return msg;
        }
    }
}
