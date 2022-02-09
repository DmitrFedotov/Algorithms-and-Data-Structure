using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hell_Work2._0
{
	public struct PointStruct
	{
		public int X;
		public int Y;
		public void qoo()
		{
			var point = new PointStruct { X = 42, Y = 42 };
			var array = new PointStruct[1];

			array[0] = point;
			array[0].Y = 33;

			PrintPoint(point, "1. point local variable");
			PrintPoint(array[0], "2. point in array");

			ChangePoint(point);

			PrintPoint(point, "3. point local variable");
			PrintPoint(array[0], "4. point in array");

			point = new PointStruct() { X = 7, Y = 7 };

			PrintPoint(point, "5. point local variable");
			PrintPoint(array[0], "6. point in array");
		}

		public static void ChangePoint(PointStruct pointClass)
		{
			pointClass.X = 13;
			pointClass.Y = 13;
		}

		public static void PrintPoint(PointStruct pointClass, string tag)
		{
			Console.WriteLine($"{tag}\t X:{pointClass.X}, Y:{pointClass.Y}");
		}


	}
}
