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
        const float dt = 0.000004f;
        const float MIN_MAGSQ = 1;
        const int CENTER_MASS = 150000;
        const int MAX_MASS = 50000;
        const int MIN_MASS = 1000;
        const int INCLINE_DEGREE = 60;
        //const int MAX_DIST = 200;
        bool closedSpace;
        int w, h;

        public Simulation(int w, int h, int n, bool spin = true, bool centerOfMass = false, bool closedSpace = false)
        {
            this.h = h;
            this.w = w;
            this.closedSpace = closedSpace;
            var rng = new Random();
            for (int i = 0; i < n; i++)
            {
                int x = rng.Next(0, w);
                int y = rng.Next(0, h);
                int mass = rng.Next(MIN_MASS, MAX_MASS);
                int size = (mass - MIN_MASS) / ((MAX_MASS - MIN_MASS) / 5);
                if (spin)
                {
                    var rads = Math.PI / 180 * INCLINE_DEGREE;
                    var cs = (float)Math.Cos(rads);
                    var sn = (float)Math.Sin(rads);
                    int velY90 = x - w / 2;
                    int velX90 = -(y - h / 2);

                    var velX = velX90 * cs - velY90 * sn;
                    var velY = velX90 * sn + velY90 * cs;
                    Vector2 vel = new Vector2(velX, velY) * rng.Next(0, 100);
                    bodies.Add(new Body(new Vector2(x, y), vel, size, mass));

                }
                else
                {
                    bodies.Add(new Body(new Vector2(x, y), Vector2.Zero, size, mass));
                }
            }
            if (centerOfMass)
            {
                bodies.Add(new Body(new Vector2(w / 2, h / 2), Vector2.Zero, 5, CENTER_MASS, true));
            }
        }


        public void Step()
        {
            Calculate();
            foreach (var body in bodies)
            {
                body.Update(dt);
                if (closedSpace)
                {
                    if (body.Position.X < 0) body.Position += new Vector2(w, 0);
                    if (body.Position.X > w) body.Position += new Vector2(-w, 0);
                    if (body.Position.Y < 0) body.Position += new Vector2(0, h);
                    if (body.Position.Y > h) body.Position += new Vector2(0, -h);

                }
            }
            bodies.RemoveAll(body => body.Position.X < -100 || body.Position.Y < -300 || body.Position.X > w + 100 || body.Position.Y > h + 300);
        }

        private void Calculate() //TODO something's off. Heavy planets are affected too much by the light ones
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    var p1 = bodies[i].Position;
                    var m1 = bodies[i].Mass;
                    var p2 = bodies[j].Position;
                    var m2 = bodies[j].Mass;

                    var relPos = p2 - p1;
                    var rSq = relPos.X * relPos.X + relPos.Y * relPos.Y;
                    //if (rSq > MAX_DIST*MAX_DIST) { continue; }
                    rSq = rSq < MIN_MAGSQ ? MIN_MAGSQ : rSq;
                    float r = MathF.Sqrt(rSq);

                    var t = relPos / (rSq * r);
                    var acc1 = m2 * t;
                    var acc2 = m1 * -t;

                    bodies[i].Acceleration += m2 * acc1;
                    bodies[j].Acceleration += m1 * acc2;
                }
            }
        }
    }
}
