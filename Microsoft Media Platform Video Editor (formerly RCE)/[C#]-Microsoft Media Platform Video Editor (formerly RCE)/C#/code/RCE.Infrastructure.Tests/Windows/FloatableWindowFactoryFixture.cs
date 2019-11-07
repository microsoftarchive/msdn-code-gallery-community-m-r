using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RCE.Infrastructure.Tests.Windows
{
    using RCE.Infrastructure.Windows;

    [TestClass]
    public class FloatableWindowFactoryFixture
    {
        [TestMethod]
        public void ShouldCreateInstancesOfFloatableWindowAdapter()
        {
            IWindowFactory windowFactory = this.CreateFactory();

            IWindow window = windowFactory.CreateWindow();

            Assert.IsInstanceOfType(window, typeof(FloatableWindowAdapter));
        }

        private IWindowFactory CreateFactory()
        {
            return new FloatableWindowFactory();
        }
    }
}