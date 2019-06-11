﻿using Bing.Sequence.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Bing.Sequence.Tests
{
    public class SnowflakeTest : TestBase
    {
        private readonly ISequence _sequence;

        public SnowflakeTest(ITestOutputHelper output) : base(output) => _sequence = GetSnowflakeSequence();

        [Fact]
        public void Test()
        {
            for (int i = 0; i < 4000000; i++)
            {
                _sequence.NextValue();
                Output.WriteLine($"id:{_sequence.NextValue()}");
            }
        }
    }
}
