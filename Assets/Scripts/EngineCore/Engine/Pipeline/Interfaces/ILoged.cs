using System;

namespace Engine.Pipeline.Interfaces
{
    /// <summary>
    ///  для логировния стейджей
    /// </summary>
    public interface ILoged
    {
        /// <summary> Сбрасывать в лог информацию о смене стейджей. </summary>
        bool LogStages { get; set; }

        Action<string> Log { get; set; }
    }
}