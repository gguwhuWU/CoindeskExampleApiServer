using CoindeskExampleApiServer.Protocols.Services;
using CoindeskExampleApiServer.Services;
using Moq;

namespace CoindeskExampleApiServerTest
{
    [TestFixture]
    public class FacadeSystemTest
    {
        private Mock<ISubSystem> _mockSub1;
        private Mock<ISubSystem> _mockSub2;
        private Mock<ISubSystem> _mockSub3;
        private Mock<ISubSystem> _mockSub4;
        private FacadeSystem _facadeSystem;

        [SetUp]
        public void SetUp()
        {
            _mockSub1 = new Mock<ISubSystem>();
            _mockSub2 = new Mock<ISubSystem>();
            _mockSub3 = new Mock<ISubSystem>();
            _mockSub4 = new Mock<ISubSystem>();

            // Using Moq to create mock instances
            _mockSub1.Setup(s => s.Output()).Returns("MockOutput1");
            _mockSub2.Setup(s => s.Output()).Returns("MockOutput2");
            _mockSub3.Setup(s => s.Output()).Returns("MockOutput3");
            _mockSub4.Setup(s => s.Output()).Returns("MockOutput4");

            // Using Moq's dependency injection
            _facadeSystem = new FacadeSystem(
                _mockSub1.Object,
                _mockSub2.Object,
                _mockSub3.Object,
                _mockSub4.Object);
        }

        [Test]
        public void MethodA_ShouldReturnCorrectOutput()
        {
            // Act
            var result = _facadeSystem.MethodA();

            // Assert
            Assert.That(result, Is.EqualTo("組合A = MockOutput1/MockOutput3"));
        }

        [Test]
        public void MethodB_ShouldReturnCorrectOutput()
        {
            // Act
            var result = _facadeSystem.MethodB();

            // Assert
            Assert.That(result, Is.EqualTo("組合B = MockOutput2/MockOutput4"));
        }
    }
}
