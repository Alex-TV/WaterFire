namespace GameLoop.Helpers
{
    public enum EventTypeEnum {
        Undefined,
        /// <summary>Стартуем движок (запуск уровня)</summary>
        LevelStart,
        /// <summary> Кнопка мыши отпущена </summary>
        EventMouseUp,
        /// <summary> Кнопка мыши нажата </summary>
        EventMouseDown,
        /// <summary> Перемещение мыши </summary>
        EventMouseMove,
        /// <summary> Двойной клик мыши </summary>
        EventMouseDoubleClick,
    }
}
