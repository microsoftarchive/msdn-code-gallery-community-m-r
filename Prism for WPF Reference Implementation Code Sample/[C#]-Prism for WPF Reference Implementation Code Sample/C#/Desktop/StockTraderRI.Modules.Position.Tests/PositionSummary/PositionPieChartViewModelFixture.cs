// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.Position.PositionSummary;

namespace StockTraderRI.Modules.Position.Tests.PositionSummary
{
    [TestClass]
    public class PositionPieChartViewModelFixture
    {
        [TestMethod]
        public void ShouldBuildCorrectly()
        {
            var observablePosition = new MockObservablePosition();

            PositionPieChartViewModel model = new PositionPieChartViewModel(observablePosition);

            Assert.AreSame(observablePosition, model.Position);
        }
    }
}
