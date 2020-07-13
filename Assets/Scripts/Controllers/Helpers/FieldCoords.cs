using UnityEngine;

namespace Scripts.Controllers.Helpers
{
	public struct FieldCoords
	{
		public static readonly FieldCoords Impossible = new FieldCoords {X = -1000, Y = -1000};
		public static readonly FieldCoords zero = new FieldCoords {X = 0, Y = 0};
		public static readonly FieldCoords one = new FieldCoords {X = 1, Y = 1};
		public static readonly FieldCoords negativeOne = new FieldCoords {X = -1, Y = -1};

		/// <summary> Верх </summary>
		public static readonly FieldCoords top = new FieldCoords {X = 0, Y = 1};

		/// <summary> Право </summary>
		public static readonly FieldCoords right = new FieldCoords {X = 1, Y = 0};

		/// <summary> Низ </summary>
		public static readonly FieldCoords bottom = new FieldCoords {X = 0, Y = -1};

		/// <summary> Лево </summary>
		public static readonly FieldCoords left = new FieldCoords {X = -1, Y = 0};

		public int X;
		public int Y;
		
		/// <summary> Лево </summary>
		public FieldCoords Left => new FieldCoords(X - 1, Y);

		/// <summary> Право </summary>
		public FieldCoords Right => new FieldCoords(X + 1, Y);

		/// <summary> Верх </summary>
		public FieldCoords Up => new FieldCoords(X, Y + 1);

		/// <summary> Низ </summary>
		public FieldCoords Down => new FieldCoords(X, Y - 1);

		public FieldCoords(int x, int y)
		{
			X = x;
			Y = y;
		}

		public FieldCoords(float x, float y)
		{
			X = (int) x;
			Y = (int) y;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is FieldCoords))
			{
				return false;
			}

			var otherPoint = (FieldCoords) obj;

			return otherPoint.X == X && otherPoint.Y == Y;
		}

		public bool Equals(FieldCoords otherPoint) => otherPoint.X == X && otherPoint.Y == Y;

		public static bool operator ==(FieldCoords a, FieldCoords b) => a.Equals(b);

		public static bool operator !=(FieldCoords a, FieldCoords b) => !a.Equals(b);

		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}

		public override string ToString() => $"[{X},{Y}]";

		public Vector2 ToVector2() => new Vector2(X, Y);

		public static FieldCoords operator +(FieldCoords p1, FieldCoords p2) =>
			new FieldCoords {X = p1.X + p2.X, Y = p1.Y + p2.Y};

		public static FieldCoords operator -(FieldCoords p1, FieldCoords p2) => p1 + -1f * p2;

		public static FieldCoords operator *(FieldCoords p, float n) =>
			new FieldCoords {X = Mathf.RoundToInt(p.X * n), Y = Mathf.RoundToInt(p.Y * n)};

		public static FieldCoords operator *(float n, FieldCoords p) => p * n;

		public static FieldCoords operator /(FieldCoords p, float n) =>
			new FieldCoords {X = Mathf.RoundToInt(p.X / n), Y = Mathf.RoundToInt(p.Y / n)};

		/// <summary> Координаты находятся внутри указанного прямоугольника </summary>
		public bool WithinRectengle(int width, int height) => 0 <= X && X < width && 0 <= Y && Y < height;

		/// <summary> Координаты находятся внутри указанного прямоугольника </summary>
		public bool WithinRectengle(int left, int top, int right, int bottom) =>
			left <= X && X < right && top <= Y && Y < bottom;

		public FieldCoords Normalized()
		{
			return new FieldCoords(X == 0 ? 0 : X / Mathf.Abs(X), Y == 0 ? 0 : Y / Mathf.Abs(Y));
		}
	}
}