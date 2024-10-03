using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBody
{
    internal class Body
    {
        private float _mass;
        private Vector2 _position;
        private Vector2 _velocity;
        private Vector2 _acceleration;
        private bool _immovable;
        private int _size;

        //private Vector2 _prevPosition;

        public float Mass { get { return _mass; } set { _mass = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }
        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
        public Vector2 Acceleration { get { return _acceleration; } set { _acceleration = value; } }
        public int Size { get { return _size; } set { _size = value; } }
        //public bool Immovable { get { return _immovable; } set { _immovable = value; } }
        //public Vector2 PrevPosition { get { return _prevPosition; } set { _prevPosition = value; } }


        //internal Body(Vector2 pos, Vector2 vel)
        //{
        //    _mass = 10000;
        //    //int x = rng.Next(0, w);
        //    //int y = rng.Next(0, h);
        //    _position = pos;
        //    //int yRelToCenter = (y - h / 2);
        //    //int xRelToCenter = (x - w/2);
        //    _velocity = vel;
        //    _acceleration = Vector2.Zero;
        //}
        internal Body(Vector2 pos, Vector2 vel, int size = 1, int mass = 10000, bool immovable = false)
        {
            _mass = mass;
            _position = pos;
            _velocity = vel;
            _size = size;
            _acceleration = Vector2.Zero;
            //_prevPosition = Position;
            _immovable = immovable;
        }


        internal void Update(float dt)
        {
            if (_immovable) return;
            //Position += Velocity.Length() > 0.01 ? Velocity*dt : Vector2.Zero;
            //_prevPosition = Position;
            Position += Velocity * dt;
            Velocity += Acceleration * dt;
            Acceleration = Vector2.Zero;
        }
    }
}
