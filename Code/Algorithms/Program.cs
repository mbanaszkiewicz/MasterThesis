﻿using System;
using BenchmarkDotNet.Running;

namespace Benchmarking
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BenchmarkRunner.Run(typeof(Program).Assembly);
    }
  }
}