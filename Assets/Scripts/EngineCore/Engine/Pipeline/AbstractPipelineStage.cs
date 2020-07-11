
using System;
using Engine.Pipeline.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Engine.Rules.Interfaces;
using UnityEngine;
using Engine.Models.Interfaces;

namespace Engine.Pipeline
{
    /// <summary>
    ///     Общий предок всех возможных стадий засовываемых в конвейер.
    ///     Список типов элементов очереди, которые может потребоваться реализовать:
    ///     - OrComposite
    ///     - Delay
    ///     - Twin
    ///     - SetGameFieldState
    ///     - Switch(On/Off)TouchInterface
    ///     -
    /// </summary>
    public abstract class AbstractPipelineStage<T> : IPipelineStage where T : class, IGameStateEntity
    {
#pragma warning disable CS0067 // The event 'PipelineStage.CanceledEvent' is never used
        public event EventHandler CanceledEvent;
#pragma warning restore CS0067 // The event 'PipelineStage.CanceledEvent' is never used
        public event EventHandler<IGameStateEntity> CompleteEvent;

        private System.Diagnostics.Stopwatch _logWatch;
        protected AbstractPipelineEngine Engine;
        protected T Entity;

        protected AbstractPipelineStage(AbstractPipelineEngine engine, T entity, IRule rule)
        {
            UniqueId = _nextFreeId++;
            Entity = entity;
            State = PipelineStates.Prepearing;
            Engine = engine;
            Producer = rule;
            engine.AddCreatedStage(this);
        }

        public string[] Tags { get; set; }
        public IRule Producer { get; }

        /// <summary> Сбрасывать в лог информацию о смене стейджей. </summary>
        public bool LogStages { get; set; }

        public Action<string> Log { get; set; }

        /// <summary>Условие выполнения стаэйджа, если не задано или вернет true то стайжд будет выполнен</summary>
        public Func<bool> СonditionProcessing { get; set; }

        /// <summary> Этот метод надо переопределять в наследниках, чтобы запихивать в него функционал конкретной стадии.</summary>
        protected abstract void Processing();

        #region Функционал очередей исполнения

        /// <summary> Текущее состояние стадии. </summary>
        public PipelineStates State { get; protected set; }

        /// <summary> Внутреннее хранилище. Изменения только через методы Add/Remove </summary>
        private readonly List<IPipelineStage> _previouse = new List<IPipelineStage>();

        /// <summary> Элементы, которые ещё не выполнились. Эта стадия ждёт их окончания. </summary>
        public IEnumerable<IPipelineStage> Previous
        {
            get { return _previouse.AsReadOnly(); }
            set
            {
                // Это полная установка с нуля, всех предыдущих убрать, всех новых засыпать.
                while (_previouse.Count > 0)
                    // Этот кусок про убирание, вообще-то не факт, что когда-нибудь будет использован. Метод в основном для красивой инициализации.
                    RemovePreviouse(_previouse[0]);
                if (value == null) return;
                foreach (var stage in value) AddPreviouse(stage);
            }
        }

        /// <summary> Добавлять В хранилищ предыдущих пачками. </summary>
        public IPipelineStage AddPreviouse(IEnumerable<IPipelineStage> stages)
        {
            foreach (var stage in stages) AddPreviouse(stage);
            return this;
        }

        /// <summary> Добавлять в хранилища предыдущих только через этот метод. </summary>
        public IPipelineStage AddPreviouse(IPipelineStage stage)
        {
            if (stage == null) return this;
            if (State < PipelineStates.Execution)
            {
#if UNITY_EDITOR
                if (!_previouse.Contains(stage))
                {
#endif
                    _previouse.Add(stage);
                    stage.CompleteEvent += OnPreviouseComplete;
#if UNITY_EDITOR
                }
                else
                {
                    UnityEngine.Debug.LogWarning(
                        $"К стадии {this} повторно добавляется один и тот же предыдущий {stage}. Это крайне подозрительно и, вероятно, ошибка.");
                }
#endif
            }
            else
                UnityEngine.Debug.LogWarning(
                    $"К стадии {this} добавляется предыдущий, хотя стадия уже неподходит для этого. Это крайне подозрительно и, вероятно, ошибка.");
            return this;
        }

        /// <summary> Сахарок для добавления нескольких элементов. </summary>
        public IPipelineStage AddPreviouse(IPipelineStage stage, params IPipelineStage[] others)
        {
            AddPreviouse(stage);
            foreach (var prev in others) AddPreviouse(prev);
            return this;
        }

        /// <summary> Убирать из хранилища предыдущих только через этот метод. </summary>
        public void RemovePreviouse(IPipelineStage stage)
        {
            if (_previouse.Contains(stage))
            {
                _previouse.Remove(stage);
                stage.CompleteEvent -= OnPreviouseComplete;
                // Во случаях, когда удаляется ограничение это может стать поводом запустить данную стадию конвеера, по-этому этот код воткнут сюда.
                if (State == PipelineStates.Ready) // Удалене может произойти когда стадия ещё только подготавливается.
                    TryExecute();
            }
        }

        protected void OnPreviouseComplete(object source, IGameStateEntity entity)
        {
            RemovePreviouse(source as IPipelineStage);
        }


        public void Ready()
        {
            if (State == PipelineStates.Prepearing)
            {
                State = PipelineStates.Ready;
                TryExecute();
            }
            else
            {
                Debug.LogError(ToShortString() + ".Ready() // Невыполнимо в данном состоянии.");
            }
        }

        protected void TryExecute()
        {
            if (State == PipelineStates.Ready)
            {
                if (_previouse.Count == 0)
                {
                    if (LogStages && Log != null)
                    {
                        _logWatch = System.Diagnostics.Stopwatch.StartNew();
                    }
                    State = PipelineStates.Execution;

                    if (СonditionProcessing == null || СonditionProcessing()) Processing();
                    else FinishStage(Entity);
                }
            }
            else
            {
                Debug.LogError(ToShortString() + ".TryExecute() // Невыполнимо в данном состоянии.");
            }
        }

        /// <summary> Этот метод дёргать когда надо закончить стадию.  </summary>
        protected void FinishStage(IGameStateEntity entity)
        {
            if (State == PipelineStates.Execution)
            {
                if (LogStages && Log != null)
                {
                    _logWatch.Stop();
                    var time = _logWatch.ElapsedMilliseconds;

                    if (!Engine.CountLog.ContainsKey(ToLogName()))
                        Engine.CountLog[ToLogName()] = 0;
                    Engine.CountLog[ToLogName()]++;

                    if (!Engine.TimeLog.ContainsKey(ToLogName()))
                        Engine.TimeLog[ToLogName()] = 0;
                    Engine.TimeLog[ToLogName()] += _logWatch.ElapsedTicks;

                    if (time > 0)
                        Log($"Finish Stage: {ToShortString()} whatch: {_logWatch.ElapsedMilliseconds} ms ({_logWatch.ElapsedTicks} ticks)");
                    _logWatch = null;
                }
                State = PipelineStates.Complete;
                CompleteEvent?.Invoke(this, entity);
                Engine = null;
            }
            else
            {
                Debug.LogError(ToShortString() + ".FinishStage() // Невыполнимо в данном состоянии.");
            }
        }

        /// <summary>
        /// Этот метод дёргать когда надо закончить стадию при выполнении условия. 
        /// </summary>
        /// <param name="condition">условие завершения</param>
        protected void FinishStage(Func<bool> condition, IGameStateEntity entity)
        {
            if (condition.Invoke())
            {
                FinishStage(entity);
            }
        }

        #endregion

        #region Функционал для трассировки

        private static ulong _nextFreeId;

        /// <summary> Нужно только для отображения элементов красивой трассировки. </summary>
        public ulong UniqueId { get; set; }

        public virtual string ToShortString()
        {
            return ToShortString("");
        }

        /// <summary> Служебный метод, чтобы переопределять было проще. </summary>
        protected string ToShortString(string content)
        {
            return "[" + GetType().Name + ":id" + UniqueId + " " + State
                   + (_previouse.Count > 0 ? " Previouse:" + _previouse.Count : "")
                   + (!string.IsNullOrEmpty(content) ? " " + content : "") + "]";
        }

        public virtual string ToLogName()
        {
            return GetType().Name;
        }


        protected virtual string ToShortString(Dictionary<string, object> content)
        {
            if (content == null || !content.Any()) return null;
            return
                ToShortString(
                    content.Select(o => o.Key + "=" + (o.Value?.ToString() ?? "null"))
                        .Aggregate((composed, next) => composed + (composed != "" ? " " : "") + next));
        }

        #endregion
    }
}