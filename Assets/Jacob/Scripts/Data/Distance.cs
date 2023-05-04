namespace Jacob.Scripts.Data
{
	public readonly struct Distance
	{
		/// <summary>
		/// Base unit of the Distance struct.
		/// </summary>
		public float Meter { get; }
		
		public Distance(float meter)
		{
			Meter = meter;
		}

		/// <summary>
		/// Distance represented in Millimeters
		/// </summary>
		public float Millimeter => Meter * 1000;
		/// <summary>
		/// Distance represented in Centimeters.
		/// </summary>
		public float Centimeter => Meter * 100;
		/// <summary>
		/// Distance represented in Kilometers
		/// </summary>
		public float Kilometer => Meter / 1000;

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

		public static float operator +(float a, Distance b) => a + b.Meter;

		public static implicit operator float(Distance a) => a.Meter;
	}
}