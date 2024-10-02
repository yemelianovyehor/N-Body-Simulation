using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NBody
{
    internal class Simulation
    {
        public List<Body> bodies = new(); //TODO
        const float dt = 0.00001f;
        const float MIN_MAGSQ = 0.1f;
        const int CENTER_MASS = 99999;
        const int MAX_MASS = 99999;
        int w, h;

        public Simulation(int w, int h, int n, bool spin = true, bool centerOfMass=false)
        {
            this.h = h;
            this.w = w;
            var rng = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = rng.Next(0, w);
                int y = rng.Next(0, h);
                if (spin)
                {
                    int velY = x - w / 2;
                    int velX = -(y - h / 2);
                    Vector2 vel = new Vector2(velX, velY) * 40;
                    bodies.Add(new Body(new Vector2(x, y), vel));

                }
                else
                {
                    bodies.Add(new Body(new Vector2(x, y), Vector2.Zero));
                }
            }
            if (centerOfMass)
            {
                bodies.Add(new Body(new Vector2(w / 2, h / 2), Vector2.Zero, CENTER_MASS, true));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"> Screen width </param>
        /// <param name="h"> Screen Height</param>
        /// <param name="n"> Number of bodies</param>
        /// <param name="distMin">min distance to center</param>
        /// <param name="distMax">max distance to center</param>
        /// <param name="spin">true = system spin around the center</param>
        public Simulation(int w, int h, int n, int distMin, int distMax, bool spin = true)
        {
            this.h = h;
            this.w = w;
            Vector2 center = new Vector2(w / 2, h / 2);
            var rng = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = rng.Next(0, w);
                int y = rng.Next(0, h);
                int mass = rng.Next(0, MAX_MASS);
                bodies.Add(new Body(new Vector2(x, y), Vector2.Zero));
            }
        }


        public void Step()
        {
            Calculate();
            foreach (var body in bodies)
            {
                body.Update(dt);
            }
            bodies.RemoveAll(body => body.Position.X < -100 || body.Position.Y < -300 || body.Position.X > w+100 || body.Position.Y > h+300);
        }

        private void Calculate()
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    var p1 = bodies[i].Position;
                    var m1 = bodies[i].Mass;
                    var p2 = bodies[j].Position;
                    var m2 = bodies[j].Mass;

                    var r = p2 - p1;
                    var magSq = r.X * r.X + r.Y * r.Y;
                    magSq = magSq < MIN_MAGSQ ? MIN_MAGSQ : magSq;
                    float mag = MathF.Sqrt(magSq);

                    var acc = (m2 / (magSq * mag)) * r;

                    bodies[i].Acceleration += m2 * acc;
                    bodies[j].Acceleration -= m1 * acc;
                }
            }
        }
    }
}
