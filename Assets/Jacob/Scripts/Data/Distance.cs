using System;
using UnityEngine.Serialization;

namespace Jacob.Scripts.Data
{
	[Serializable]
	public struct Distance
	{
		/// <summary>
		/// Base unit of the Distance struct.
		/// </summary>
		public float meter; 
		
		public Distance(float meter)
		{
			this.meter = meter;
		}

		/// <summary>
		/// Distance represented in Millimeters
		/// </summary>
		public float Millimeter => meter * 1000;
		/// <summary>
		/// Distance represented in Centimeters.
		/// </summary>
		public float Centimeter => meter * 100;
		/// <summary>
		/// Distance represented in Kilometers
		/// </summary>
		public float Kilometer => meter / 1000;

		/// <summary>
		/// Create a new Distance struct from Millimeters.
		/// </summary>
		/// <param name="millimetre">Distance in Millimeters.</param>
		/// <returns>A Distance struct with your distance converted into Meters.</returns>
		public static Distance Millimetre(float millimetre)
		{
			return new Distance(millimetre / 100);
		}
		
		/// <summary>
		/// Create a new Distance struct from Centimeters.
		/// </summary>
		/// <param name="centimetre">Distance in Centimeters.</param>
		/// <returns>A Distance struct with your distance converted into Meters.</returns>
		public static Distance Centimetre(float centimetre)
		{
			return new Distance(centimetre / 100);
		}

		/// <summary>
		/// Create a new Distance struct from Kilometers.
		/// </summary>
		/// <param name="kilometre">Distance in Kilometers.</param>
		/// <returns>A Distance struct with your distance converted into Meters.</returns>
		public static Distance Kilometre(float kilometre)
		{
			return new Distance(kilometre * 1000);
		}

		public static float operator +(float a, Distance b) => a + b.meter;

		public static implicit operator float(Distance a) => a.meter;
	}
}