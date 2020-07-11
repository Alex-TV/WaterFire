
namespace Engine.Pipeline.Interfaces
{
    public enum PipelineStates
    {
        /// <summary> Идёт формирование стадии. На этом этапе она ещё ни на что не реагирует. Дефолтное значение. </summary>
        Prepearing = 0,

        /// <summary> Стадия конвейера готова к использованию. Если у неё нет предыдущих, то при установке этого этапа она запустится. </summary>
        Ready,

        /// <summary> Стадия конвейера исполняется, то есть уже был дёрнут Processing, но ещё не был StageFinished </summary>
        Execution,

        /// <summary> Исполнение стадии закончено </summary>
        Complete,

        /// <summary> Исполнение стадии прервано </summary>
        Canceled
    }
}
