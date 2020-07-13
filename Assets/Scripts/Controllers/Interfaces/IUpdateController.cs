
using Scripts.Controllers.Helpers;

namespace Controllers.Interfaces
{
    /// <summary>
    ///  Общий интерфейс обновления 
    /// </summary>
    public interface IUpdateController : ICoroutine
    {
        void Active();
        void AddComponent(UpdatablesName componentName, ICustomUpdatable component);
        void PauseComponent(UpdatablesName componentName);
        void ResumeComponent(UpdatablesName componentName);
    }
}
