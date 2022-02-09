using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Hell_Work2._0
{
	public class PointClass
	{
		public int X;
		public int Y;

		public void  foo()
		{
			var point = new PointClass() { X = 42, Y = 42 };
			var array = new PointClass[1];
			array[0] = point;

			PrintPoint(point, "1. point local variable");
			PrintPoint(array[0], "2. point in array");

			ChangePoint(point);

			PrintPoint(point, "3. point local variable");
			PrintPoint(array[0], "4. point in array");

			point = new PointClass() { X = 7, Y = 7 };

			PrintPoint(point, "5. point local variable");
			PrintPoint(array[0], "6. point in array");
		}

		public static void ChangePoint(PointClass pointClass)
		{
			pointClass.X = 13;
			pointClass.Y = 13;
		}

		public static void PrintPoint(PointClass pointClass, string tag)
		{
			Console.WriteLine($"{tag}\t X:{pointClass.X}, Y:{pointClass.Y}");
		}

	}



}
