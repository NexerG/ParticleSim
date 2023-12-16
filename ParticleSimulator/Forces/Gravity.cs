﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulator.Forces
{
    public class Gravity : Force
    {
        public Gravity(PointF force) : base(force)
        {
        }
        public Gravity(Vector3 force) : base(force)
        {
        }
    }
}
