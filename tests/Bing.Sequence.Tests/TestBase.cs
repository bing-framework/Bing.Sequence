using Bing.Sequence.Abstractions;
using Bing.Sequence.Snowflake;
using Xunit.Abstractions;

namespace Bing.Sequence.Tests
{
    public class TestBase
    {
        protected ITestOutputHelper Output;

        public TestBase(ITestOutputHelper output) => Output = output;

        /// <summary>
        /// ��ȡѩ���㷨���к�������
        /// </summary>
        /// <returns></returns>
        protected ISequence GetSnowflakeSequence() => SnowflakeSequenceBuilder.Create(1, 2).Build();
    }
}
