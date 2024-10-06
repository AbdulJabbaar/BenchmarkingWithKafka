// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using MessagingWithKafka;

Console.WriteLine("Starting Producer and ProduceAsync Benchmark");
BenchmarkRunner.Run<KafkaProducerBenchmarks>();
