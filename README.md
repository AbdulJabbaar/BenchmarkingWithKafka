# Kafka Producer Benchmarking Project

## Overview

This project benchmarks the performance of Kafka producers in .NET using both synchronous and asynchronous message production. It utilizes the Confluent Kafka client library to produce messages to a Kafka broker, measuring the time taken for each operation and the memory allocation during the process.

## Project Structure

- **KafkaProducerBenchmarks.cs**: This file contains the benchmark tests for both synchronous and asynchronous Kafka message producers.
- **Docker-compose.yml file**: A Docker configuration file to set up a Kafka environment.

## Prerequisites

Before you can run this project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Creating Kafka Topics:-
You can use install this [VS Code extension](https://marketplace.visualstudio.com/items?itemName=jeppeandersen.vscode-kafka)
- Create two topics `kafka_topic_produce_sync` and `kafka_topic_produce_async`

## Benchmark Results:

| Method             | Mean          | Error      | StdDev     | Gen0   | Gen1   | Gen2   | Allocated |
|------------------- |--------------:|-----------:|-----------:|-------:|-------:|-------:|----------:|
| KafkaProducerSync  |      2.380 us |  0.0457 us |  0.1203 us | 0.1106 | 0.1068 | 0.0038 |   1.35 KB |
| KafkaProducerAsync | 15,389.778 us | 51.7859 us | 40.4310 us |      - |      - |      - |   2.71 KB |