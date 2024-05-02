﻿using System;

namespace AI.Lab10.Tools.ANN
{
    internal class AssertionFailedException : Exception
    {
        private string v;

        public AssertionFailedException(string v) : base(v)
        {
            this.v = v;
        }
    }
}