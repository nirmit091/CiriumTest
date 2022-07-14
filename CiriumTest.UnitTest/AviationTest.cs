using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CiriumTest.UnitTest
{
    [TestClass]
    public class AviationTest
    {
        [TestMethod]
        public void TestInputValue()
        {
            string input = "airbus 60";
            string result = AviationBase.SetAndCalculate(input);
            Assert.AreEqual("Input saved in variable for calculation.", result);
        }

        [TestMethod]
        public void EmptyInput()
        {
            string result = AviationBase.SetAndCalculate("");
            Assert.AreEqual("Input value can not be empty.", result);
        }

        [TestMethod]
        public void InvalidAircraftValue()
        {
            string result = AviationBase.SetAndCalculate("airbuses 60");
            Assert.AreEqual("Please enter valid input value.", result);
        }

        [TestMethod]
        public void InvalidMinutes()
        {
            string result = AviationBase.SetAndCalculate("airbus 60A");
            Assert.AreEqual("Please enter valid minutes value.", result);
        }

        [TestMethod]
        public void TestCalculationValue()
        {
            AviationBase.SetAndCalculate("airbus 60");
            AviationBase.SetAndCalculate("airbus 100");
            string result = AviationBase.SetAndCalculate("calculate");
            Assert.AreEqual("Airbus 2:40", result);
        }
    }
}
