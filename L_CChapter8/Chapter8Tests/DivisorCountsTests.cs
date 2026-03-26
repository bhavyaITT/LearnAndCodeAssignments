using Chapter8Assignment;

namespace Chapter8Tests
{
    public class DivisorCountsTests
    {
        [Fact]
        public void TestSampleInput()
        {
            int result = DivisorCounter.CountValidN(15);
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestSmallInput()
        {
            int result = DivisorCounter.CountValidN(5);
            Assert.Equal(1, result);
        }

        [Fact]
        public void TestMinimumInput()
        {
            int result = DivisorCounter.CountValidN(2);
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestLargerInput()
        {
            int result = DivisorCounter.CountValidN(50);
            Assert.True(result >= 0); 
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(1)]
        public void TestInvalidInput(int k)
        {
            int result = DivisorCounter.CountValidN(k);
            Assert.Equal(0, result);
        }
    }
}
