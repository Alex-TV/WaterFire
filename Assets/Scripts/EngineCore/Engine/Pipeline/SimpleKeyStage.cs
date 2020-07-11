
using Engine.Models;
using Engine.Rules;
using GameLoop.Entitys;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Pipeline
{
    /// <summary>
    /// Нужен для изменения последовательности выполнения
    /// </summary>
    public class SimpleKeyStage<T> : PipelineStage<T> where T : class, IGameStateEntity
    {
        public string Key;

        public SimpleKeyStage(PipelineEngine engine, T entity, Rule<T> rule) : base(engine, entity, rule) { }

        protected override void Processing() => FinishStage(Entity);

        public static bool IsSimpleKeyStage(PipelineStage<T> stage, string key)
            => stage is SimpleKeyStage<T> && ((SimpleKeyStage<T>)stage).Key == key;

        public override string ToShortString() => base.ToShortString(new Dictionary<string, object> { { "Key", Key } });

        /// <summary>
        ///     Находит последнюю стадию с указанным клюём, присутствующую на участке после последнего брейкпоинта.
        ///     Метод вынесен сюда, потому что реально часто будет использоваться.
        /// </summary>
        public static SimpleKeyStage<T> GetLastKeystage(PipelineEngine engine, string key)
        {
            return
                (from en in engine.Stages
                 where en is SimpleKeyStage<T> && (en as SimpleKeyStage<T>).Key == key
                 select en as SimpleKeyStage<T>).FirstOrDefault();
        }

        /// <summary>
        ///     Находит или создаёт последнюю стадию с указанным клюём, присутствующую на участке после последнего брейкпоинта.
        ///     Метод вынесен сюда, потому что реально часто будет использоваться.
        /// </summary>
        public static SimpleKeyStage<T> GetOrCreateLastKeystage(PipelineEngine engine, T model, Rule<T> producer,
                                                             string key)
        {
            foreach (var en in engine.Stages.Where(en => en is SimpleKeyStage<T> && (en as SimpleKeyStage<T>).Key == key)) return en as SimpleKeyStage<T>;
            return new SimpleKeyStage<T>(engine, model, producer) { Key = key };
        }

        /// <summary>
        ///     Создать новую ключевую точку, пофиг была такая уже раньше или нет.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="producer"></param>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SimpleKeyStage<T> CreateKeystage(PipelineEngine engine, T entity, Rule<T> producer, string key)
            => new SimpleKeyStage<T>(engine, entity, producer) { Key = key };

        public override string ToString() => "SimpleKeyStage: " + Key;
    }
}