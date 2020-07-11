
namespace Engine.Pipeline.Interfaces {
    /// <summary>
    ///     для трасировки стейждей
    /// </summary>
    public interface ITracing {
        /// <summary> Нужно только для отображения элементов красивой трассировки. </summary>
        ulong UniqueId { get; set; }
    }
}