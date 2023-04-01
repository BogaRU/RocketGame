using System;
using System.Collections.Generic;
using System.Drawing;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();

		public static IEnumerable<Level> CreateLevels()
		{
			yield return new Level("Zero",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) => Vector.Zero, standardPhysics);
			yield return new Level("Heavy",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) => new Vector(0, 0.9), standardPhysics);
			yield return new Level("Up",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(700, 500),
				(size, v) => new Vector(0, -300 / (size.Height - v.Y + 300.0)), standardPhysics);
			yield return new Level("WhiteHole",
				new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
				new Vector(600, 200),
				(size, v) =>
				{
					var distance = new Vector(600 - v.X, 200 - v.Y);
					var gravity = 140 * distance.Length / (distance.Length * distance.Length + 1);
                    return new Vector(-gravity * Math.Cos(distance.Angle), -gravity * Math.Sin(distance.Angle));
				}, standardPhysics);
            yield return new Level("BlackHole",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) =>
                {
                    var distance = new Vector(400 - v.X, 350 - v.Y);
                    var gravity = 300 * distance.Length / (distance.Length * distance.Length + 1);
                    return new Vector(gravity * Math.Cos(distance.Angle), gravity * Math.Sin(distance.Angle));
                }, standardPhysics);
            yield return new Level("BlackAndWhite",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),
                new Vector(600, 200),
                (size, v) => (GetGravity(v, true) + GetGravity(v, false)) / 2, standardPhysics);
        }

		public static Vector GetGravity(Vector v, bool isBlackHole)
		{
			var x = 600;
			var y = 200;
			var k = 140;
			if (isBlackHole)
			{
				x = 400;
				y = 350;
				k = 300;
			}
            var distance = new Vector(x - v.X, y - v.Y);
            var gravity = k * distance.Length / (distance.Length * distance.Length + 1);
			return new Vector(gravity * Math.Cos(distance.Angle), gravity * Math.Sin(distance.Angle)) * (isBlackHole? 1 : -1);
        }
	}
}