using System;
using System.Collections.Generic;
using System.Text;
using USSEScoreboard.Interfaces;
using USSEScoreboard.Models;
using Xunit;

namespace USSEScoreboard.Tests
{
    public class ProgressCalculator_Tests
    {
        //TODO: Add dependency injection for wigsettings from db

        [Fact]
        public void ResultsValue1()
        {
            //setup
            var StartDate = new DateTime(2016, 7, 1);
            var EndDate = new DateTime(2017, 7, 1);
            var AWinGoal = 20;
            var CWinGoal = 100;
            var TotalAscendWins = 10;
            var TotalPresentations = 20;

            ProgressCalculator pg = new ProgressCalculator();
            ProgressCalculator.Result result = pg.CalculateProgress(
                StartDate, EndDate, AWinGoal,
                CWinGoal, TotalAscendWins, TotalPresentations);
            Assert.True(result.AscendOverallPct > 1,"Percentage should be greater than 0");
        }
    }
}
