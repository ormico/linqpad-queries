<Query Kind="Statements">
  <NuGetReference>MathNet.Numerics</NuGetReference>
  <Namespace>MathNet.Numerics.Distributions</Namespace>
  <Namespace>MathNet.Numerics.Random</Namespace>
</Query>

var guid = Guid.NewGuid();
guid.Dump();
Convert.ToBase64String(guid.ToByteArray()).Dump();

guid = new Guid("56bfe6db-2024-4038-857a-f2348414a694");
guid.Dump();
Convert.ToBase64String(guid.ToByteArray()).Dump();
// 56bfe6db-2024-4038-857a-f2348414a694
///////////////////////////////////////////////////////////////////////////////////////
// create an array with 1000 random values
double[] samples = SystemRandomSource.Doubles(1000, RandomSeed.Robust());

// now overwrite the existing array with new random values
//SystemRandomSource.Doubles(samples);

// we can also create an infinite sequence of random values:
IEnumerable<double> sampleSeq = SystemRandomSource.DoubleSequence();

// take a single random value
System.Random rng = SystemRandomSource.Default;
double sample = rng.NextDouble();
decimal sampled = rng.NextDecimal();

byte[] byteArray = rng.NextBytes(8);
BitConverter.ToString(byteArray).Replace("-", string.Empty).Dump();