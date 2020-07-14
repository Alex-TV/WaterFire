
using UnityEngine;

namespace Scripts.Controllers.Helpers
{
    public class CoordinateConverter
    {
        public static float CellSize = 2;
        public static int GridSizeX = 6;
        public static int GridSizeY = 7;
        public static Vector3 GridCentre { get; set; } = Vector3.zero;

        /// <summary>
        ///     Конвертация координат грида в позицию на экране.
        /// </summary>
        /// <param name="fieldCoords">координаты грида</param>
        /// <returns>позицию на экране</returns>
        public static Vector3 FieldCoordsToPosition(FieldCoords fieldCoords)
        {
            var сentreBias = new Vector3(GridSizeX * CellSize / 2, GridSizeY * CellSize / 2) + GridCentre;
            return new Vector3(fieldCoords.X * CellSize + CellSize / 2, fieldCoords.Y * CellSize + CellSize / 2) - сentreBias;
        }

        /// <summary>
        /// Конвертация позиции на экране в координат грида.
        /// </summary>
        /// <param name="position">позиции на экране</param>
        /// <returns>координат грида</returns>
        public static FieldCoords CameraPositionToFieldCoords(Vector3 position)
        {
            position = Camera.main.ScreenToWorldPoint(position);
            return PositionToFieldCoords(position);
        }

        /// <summary>
        /// Конвертация позиции в координат грида.
        /// </summary>
        /// <param name="position">позиции на экране</param>
        /// <returns>координат грида</returns>
        public static FieldCoords PositionToFieldCoords(Vector3 position)
        {
            var сentreBias = new Vector3(GridSizeX * CellSize / 2, GridSizeY * CellSize / 2) + GridCentre;
            var x = Mathf.Floor((position.x + сentreBias.x) / CellSize);
            var y = Mathf.Floor((position.y + сentreBias.y) / CellSize);
            return new FieldCoords(x, y);
        }
    }
}