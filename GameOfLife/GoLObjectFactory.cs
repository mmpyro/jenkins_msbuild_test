﻿using System;

namespace GameOfLife
{
    public class GoLObjectFactory
    {
        public static IGoLObject Create(GoLPatterns @object)
        {
            string x = null;
            bool result = x.EndsWith("x");
            switch (@object)
            {
                case GoLPatterns.Blinker:
                    return new Blinker();
                case GoLPatterns.Block:
                    return new Block();
                default:
                    throw new ArgumentException(string.Format("{0} doesn't exist", @object));
            }
        }
    }
}