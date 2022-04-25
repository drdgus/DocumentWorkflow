// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess;
using DocumentWorkflowBenchmarks;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<MyBenchmarks>();