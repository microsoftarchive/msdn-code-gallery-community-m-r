// <copyright file="TimeCodeFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
// Project: SMPTE 12M Timecode
// FILES: TimeCodeFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace SMPTETimecode.Tests
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="TimeCode"/>.
    /// </summary>
    [TestClass]
    public class TimeCodeFixture
    {
        /// <summary>
        /// Creates the time code_2398 from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2398FromString()
        {
            var timecode = new TimeCode("01:24:12:13", SmpteFrameRate.Smpte2398);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the time code_2398 from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2398FromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte2398);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 24fps from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_24FromString()
        {
            var timecode = new TimeCode("01:24:12:13", SmpteFrameRate.Smpte24);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 24fps from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_24FromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte24);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 25fps from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_25FromString()
        {
            var timecode = new TimeCode("01:24:12:13", SmpteFrameRate.Smpte25);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 25fps from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_25FromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte25);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode  2997 non drop from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2997NonDropFromString()
        {
            var timecode = new TimeCode("01:42:12:20", SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("01:42:12:20", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 non drop from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2997NonDropFromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 drop from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2997DropFromString()
        {
            TimeCode timecode = new TimeCode("01:42:12;22", SmpteFrameRate.Smpte2997Drop);

            Console.WriteLine("TimeCode = {0}", timecode);
            Assert.AreEqual("01:42:12;22", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 drop from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_2997DropFromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte2997Drop);

            Console.WriteLine("TimeCode = {0}", timecode);

            Assert.AreEqual("01:24:12;13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 30 from string.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_30FromString()
        {
            var timecode = new TimeCode("01:42:12:22", SmpteFrameRate.Smpte30);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("01:42:12:22", timecode.ToString());
        }

        /// <summary>
        /// Creates the time code_30 from integers.
        /// </summary>
        [TestMethod]
        public void CreateTimeCode_30FromIntegers()
        {
            var timecode = new TimeCode(1, 24, 12, 13, SmpteFrameRate.Smpte30);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Adds two  timecodes @ 30fps.
        /// </summary>
        [TestMethod]
        public void Add2TimeCodes30Fps()
        {
            var t1 = new TimeCode(0, 01, 00, 00, SmpteFrameRate.Smpte30);
            var t2 = new TimeCode(0, 01, 00, 22, SmpteFrameRate.Smpte30);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:02:00:22", t3.ToString());
        }

        /// <summary>
        /// Adds two timecodes @29 nondrop.
        /// </summary>
        [TestMethod]
        public void Add2TimeCodes_29Nondrop()
        {
            TimeCode t1 = new TimeCode(0, 01, 00, 29, SmpteFrameRate.Smpte2997NonDrop);
            TimeCode t2 = new TimeCode(0, 00, 00, 02, SmpteFrameRate.Smpte2997NonDrop);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:01:01:01", t3.ToString());
        }

        [TestMethod]
        public void Add2TimeCodes_29Drop1()
        {
            TimeCode t1 = new TimeCode(0, 01, 00, 29, SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode(0, 01, 00, 02, SmpteFrameRate.Smpte2997Drop);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:02:01;01", t3.ToString());
        }

        [TestMethod]
        public void Add2TimeCodes_29Drop2()
        {
            TimeCode t1 = new TimeCode(0, 01, 00, 29, SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode(0, 01, 00, 02, SmpteFrameRate.Smpte2997Drop);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:02:01;01", t3.ToString());
        }

        /// <summary>
        /// Adds two timecodes @ 25 fps PAL/EBU
        /// </summary>
        [TestMethod]
        public void Add2TimeCodes25Fps()
        {
            TimeCode t1 = new TimeCode(0, 00, 10, 13, SmpteFrameRate.Smpte25);
            TimeCode t2 = new TimeCode(0, 00, 12, 22, SmpteFrameRate.Smpte25);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:00:23:10", t3.ToString());
        }

        /// <summary>
        /// Adds two time codes @ 23.98fps.
        /// </summary>
        [TestMethod]
        public void Add2TimeCodes2398Fps()
        {
            TimeCode t1 = new TimeCode(0, 00, 10, 13, SmpteFrameRate.Smpte2398);
            TimeCode t2 = new TimeCode(0, 00, 12, 22, SmpteFrameRate.Smpte2398);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("00:00:23:11", t3.ToString());
        }

        /// <summary>
        /// Adds two time codes @ 23.98fps.
        /// </summary>
        [TestMethod]
        public void Add2TimeCodes2398Fps2()
        {
            TimeCode t1 = new TimeCode(15, 54, 25, 12, SmpteFrameRate.Smpte2398);
            TimeCode t2 = new TimeCode(1, 42, 35, 15, SmpteFrameRate.Smpte2398);

            TimeCode t3 = t1 + t2;

            Assert.AreEqual("17:37:01:03", t3.ToString());
        }

        /// <summary>
        /// Adds two time codes @ 23.98fps that are both equal.
        /// </summary>
        [TestMethod]
        public void TimeCodesAreEqualBoth2398()
        {
            TimeCode t1 = new TimeCode(0, 12, 10, 13, SmpteFrameRate.Smpte2398);
            TimeCode t2 = new TimeCode(0, 12, 10, 13, SmpteFrameRate.Smpte2398);

            Assert.AreEqual(t1, t2);
        }

        [TestMethod]
        public void Add2TimeCodes_PastMaxTimeCode()
        {
            try
            {
                TimeCode t1 = new TimeCode(12, 01, 00, 00, SmpteFrameRate.Smpte30);
                TimeCode t2 = new TimeCode(12, 01, 00, 22, SmpteFrameRate.Smpte30);

                TimeCode t3 = t1 + t2;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(System.OverflowException));
            }
        }

        [TestMethod]
        public void TimeCodes_AreEqual_DifferentFrameRates_DropAndNon()
        {
            TimeCode t1 = new TimeCode("00:12:33:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:04", SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine(t1.ToString(SmpteFrameRate.Smpte2997NonDrop));

            Assert.AreEqual(t1, t2);
        }

        /// <summary>
        /// Checks to see which is less using different rates drop and PAL.
        /// </summary>
        [TestMethod]
        public void TimeCodesLessThanOrEqualDifferentFrameRatesDropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:33:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:23", SmpteFrameRate.Smpte25);

            Assert.IsTrue(t1 <= t2);
        }

        [TestMethod]
        public void TimeCodesLessThanOrEqual2DifferentFrameRatesDropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:31:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:22", SmpteFrameRate.Smpte25);

            Assert.IsTrue(t1 <= t2);
        }

        [TestMethod]
        public void TimeCodesLessThanOrEqual3DifferentFrameRates_DropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:35:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:22", SmpteFrameRate.Smpte25);

            Assert.IsFalse(t1 <= t2);
        }

        [TestMethod]
        public void TimeCodesGreaterThanOrEqualDifferentFrameRatesDropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:33:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:22", SmpteFrameRate.Smpte25);

            Assert.IsTrue(t1 >= t2);
        }

        [TestMethod]
        public void TimeCodesGreaterThanOrEqual2DifferentFrameRatesDropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:35:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:22", SmpteFrameRate.Smpte25);

            Assert.IsTrue(t1 >= t2);
        }

        [TestMethod]
        public void TimeCodesGreaterThanOrEqual3DifferentFrameRatesDropAndPAL()
        {
            TimeCode t1 = new TimeCode("00:12:31:26", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:12:33:22", SmpteFrameRate.Smpte25);

            Assert.IsFalse(t1 >= t2);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTEDrop()
        {
            const long Ticks27 = 156523374000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("01:36:37;05", timecode);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTENonDrop()
        {
            const long Ticks27 = 156523374000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("01:36:31:11", timecode);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTE2398()
        {
            const long Ticks27 = 156523374000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("01:36:31:08", timecode);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTE24()
        {
            const long Ticks27 = 156523374000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte24);

            Assert.AreEqual("01:36:37:03", timecode);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTE25()
        {
            const long Ticks27 = 156523374000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:36:37:04", timecode);
        }

        [TestMethod]
        public void Ticks27MhzToSMPTE30()
        {
            const long Ticks27 = 156522600000;
            string timecode = TimeCode.Ticks27MhzToSmpte12M(Ticks27, SmpteFrameRate.Smpte30);

            Assert.AreEqual("01:36:37:04", timecode);
        }

        [TestMethod]
        public void SMPTE30ToTicks27Mhz()
        {
            const string Timecode = "01:36:37:04";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte30);

            Assert.AreEqual(156522600000, (long)ticks27Mhz);
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString()
        {
            const string Timecode = "01:36:37:04";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte30);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte30);

            Assert.AreEqual("01:36:37:04", t.ToString());
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString2398()
        {
            const string Timecode = "01:36:31:08";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte2398);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("01:36:31:08", t.ToString());
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString24()
        {
            const string Timecode = "01:36:37:05";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte24);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte24);

            Assert.AreEqual("01:36:37:05", t.ToString());
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString25()
        {
            const string Timecode = "01:36:37:05";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte25);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:36:37:05", t.ToString());
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString2997Drop()
        {
            const string Timecode = "01:36:37;05";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte2997Drop);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("01:36:37;05", t.ToString());
        }

        [TestMethod]
        public void TimecodeFromTicks27BackToString2997NonDrop()
        {
            const string Timecode = "01:36:37:05";
            long ticks27Mhz = TimeCode.Smpte12MToTicks27Mhz(Timecode, SmpteFrameRate.Smpte2997NonDrop);

            TimeCode t = TimeCode.FromTicks27Mhz(ticks27Mhz, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("01:36:37:05", t.ToString());
        }

        [TestMethod]
        public void ValidateBadTimecode1()
        {
            bool valid = TimeCode.ValidateSmpte12MTimecode("24:00:00:12");

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidateBadTimecode2()
        {
            bool valid = TimeCode.ValidateSmpte12MTimecode("01:60:10:10");

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ValidateGoodTimecode()
        {
            bool valid = TimeCode.ValidateSmpte12MTimecode("23:38:10:10");

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void ValidateAreEqualDropFrame()
        {
            TimeCode t1 = new TimeCode("01:01:43;02", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("01:01:43;02", SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual(t1, t2);
        }

        [TestMethod]
        public void ValidateAreNotEqualDropFrame()
        {
            TimeCode t1 = new TimeCode("01:01:43;01", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("01:01:43;02", SmpteFrameRate.Smpte2997Drop);

            Assert.AreNotEqual(t1, t2);
        }

        [TestMethod]
        public void ValidateAreEqualAddOneFrame()
        {
            TimeCode t1 = new TimeCode("01:29:45:15", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("01:29:45:16", SmpteFrameRate.Smpte2997Drop);

            TimeCode expected = t1 + new TimeCode(0, 0, 0, 1, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual(expected, t2);
        }

        [TestMethod]
        public void AddAndSubtractTimecodesDrop()
        {
            TimeCode t1 = new TimeCode("00:58:12:15", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:02:00:00", SmpteFrameRate.Smpte2997Drop);
            TimeCode t3 = new TimeCode("01:22:12:15", SmpteFrameRate.Smpte2997Drop);

            TimeCode t4 = t1 + t3 - t2;

            Assert.AreEqual("02:18:25;00", t4.ToString());
        }

        [TestMethod]
        public void Add3TimecodesDrop()
        {
            TimeCode t1 = new TimeCode("00:01:00:02", SmpteFrameRate.Smpte2997Drop);
            TimeCode t2 = new TimeCode("00:10:00:00", SmpteFrameRate.Smpte2997Drop);
            TimeCode t3 = new TimeCode("01:00:00:00", SmpteFrameRate.Smpte2997Drop);

            TimeCode t4 = t1 + t2 + t3;

            Assert.AreEqual("01:11:00;02", t4.ToString());
        }

        [TestMethod]
        public void AbsoluteTimeToDropFrame()
        {
            const double AbsoluteTime = 8304.963333333335;

            TimeCode t1 = new TimeCode(AbsoluteTime, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("02:18:25;00", t1.ToString());
        }

        [TestMethod]
        public void GetTotalHours()
        {
            TimeCode t1 = new TimeCode("01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(1.5D, t1.TotalHours);
        }

        [TestMethod]
        public void GetTotalMinutes()
        {
            TimeCode t1 = new TimeCode("01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(90D, t1.TotalMinutes);
        }

        [TestMethod]
        public void FromHours()
        {
            TimeCode t1 = TimeCode.FromHours(1.5, SmpteFrameRate.Smpte30);

            Assert.AreEqual(1.5D, t1.TotalHours);
        }

        [TestMethod]
        public void AbsoluteToSmpte2997Drop()
        {
            const double AbsoluteTime = 37.837800000000000000000D;

            TimeCode timecode = new TimeCode(AbsoluteTime, SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("00:00:37:24", timecode.ToString());
        }

        [TestMethod]
        public void SubtractToMinValue()
        {
            TimeCode timecode1 = new TimeCode(0, 0, 0, 1, SmpteFrameRate.Smpte2997NonDrop);
            TimeCode timecode2 = new TimeCode(0, 0, 0, 1, SmpteFrameRate.Smpte2997NonDrop);

            TimeCode time3 = timecode1 - timecode2;

            Assert.AreEqual("00:00:00:00", time3.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        [Ignore]
        public void SubtractPastMinValue()
        {
            TimeCode timecode1 = new TimeCode(0, 0, 0, 1, SmpteFrameRate.Smpte2997NonDrop);
            TimeCode timecode2 = new TimeCode(0, 0, 0, 4, SmpteFrameRate.Smpte2997NonDrop);

            TimeCode time3 = timecode1 - timecode2;
        }

        /// <summary>
        /// Checks to see that 1000/1001 is coming back as expected. This is the slowdown rate of 29.97 video.
        /// </summary>
        [TestMethod]
        public void CheckDotNetMathDivide1000By1001()
        {
            Assert.AreEqual((1000 / (decimal)1001), decimal.Parse("0.999000999000999000999000999", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Checks to see that 30 / (1000/1001) is coming back as expected
        /// </summary>
        [TestMethod]
        public void CheckDotNetMathDivide30By1000By1001()
        {
            Assert.AreEqual(30 / (1000 / (decimal)1001), decimal.Parse("30.03", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Checks to see that 29 frames is converting to absolute time as expected
        /// </summary>
        [TestMethod]
        public void CheckDotNetMathConvert29Frames2997ToAbsoluteTime()
        {
            const int Frames = 29;
            const decimal AbsoluteTime = Frames / (decimal)30 / (1000 / (decimal)1001);

            Assert.AreEqual(AbsoluteTime, decimal.Parse("0.9676333333333333333333333334", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Checks to see that 31 frames is converting to absolute time as expected
        /// </summary>
        [TestMethod]
        public void CheckDotNetMathConvert31Frames2997ToAbsoluteTime()
        {
            const int Frames = 31;
            const decimal AbsoluteTime = Frames / (decimal)30 / (1000 / (decimal)1001);

            Assert.AreEqual(AbsoluteTime, decimal.Parse("1.0343666666666666666666666666", CultureInfo.InvariantCulture));
            Assert.AreEqual(Convert.ToDouble(AbsoluteTime), double.Parse("1.0343666666666666666666666666", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Checks to see that 29 frames is actually 00:00:00:29 in Smpte 29.97 timecode.
        /// </summary>
        [TestMethod]
        public void CheckFromFrames29IsEqualToString()
        {
            const long Frames = 29;
            TimeCode t = TimeCode.FromFrames(Frames, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:00:00:29", t.ToString());
        }

        /// <summary>
        /// Checks to see that 30 frames is actually 00:00:01:00 in Smpte 29.97 timecode.
        /// </summary>
        [TestMethod]
        public void CheckFromFrames30IsEqualToString()
        {
            const long Frames = 30;
            TimeCode t = TimeCode.FromFrames(Frames, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:00:01:00", t.ToString());
        }

        /// <summary>
        /// Checks to see that 31 frames is actually 00:00:01:01 in Smpte 29.97 timecode.
        /// </summary>
        [TestMethod]
        public void CheckFromFrames31IsEqualToString()
        {
            const long Frames = 31;
            TimeCode t = TimeCode.FromFrames(Frames, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:00:01:01", t.ToString());
        }

        /// <summary>
        /// This test adds frames to an existing timecode and makes sure it is correct.
        /// </summary>
        [TestMethod]
        public void AddFramesToExistingTimeCodeEnsureCorrectTime()
        {
            var timeSpanInitial = TimeSpan.FromSeconds(1);

            var startingTimeCode = TimeCode.FromTimeSpan(timeSpanInitial, SmpteFrameRate.Smpte2997NonDrop);
            Console.WriteLine("startingTimeCode: " + startingTimeCode);

            Assert.AreEqual(29, (int)startingTimeCode.TotalFrames);

            float totalFrameCount = startingTimeCode.TotalFrames;
            Console.WriteLine("totalFrameCount: " + totalFrameCount);
            Assert.AreEqual(29, (int)totalFrameCount);

            int newFrameCount = (int)totalFrameCount + 1;
            Assert.AreEqual(30, newFrameCount);
            Console.WriteLine("newFrameCount: " + newFrameCount);

            var frameAddedTimeCode = TimeCode.FromFrames((long)newFrameCount, SmpteFrameRate.Smpte2997NonDrop);
            Console.WriteLine("frameAddedTimeCode: " + frameAddedTimeCode);
            Assert.AreEqual(30, (int)frameAddedTimeCode.TotalFrames);

            var newTime = TimeSpan.FromSeconds(frameAddedTimeCode.TotalSeconds);
            Console.WriteLine("newTime: " + newTime);

            var newTimeCode = TimeCode.FromSeconds(newTime.TotalSeconds, SmpteFrameRate.Smpte2997NonDrop);
            Console.WriteLine("newTimeCode: " + newTimeCode);

            Console.WriteLine("startingTimeCode.TotalFrames:" + startingTimeCode.TotalFrames);
            Console.WriteLine("newTimeCode.TotalFrames:" + newTimeCode.TotalFrames);
            Assert.AreNotEqual(startingTimeCode.TotalFrames, newTimeCode.TotalFrames);
            Assert.AreEqual(30, (int)newTimeCode.TotalFrames);
        }

        /// <summary>
        /// This test adds frames to an existing 2398 format timecode and makes sure it is correct.
        /// </summary>
        [TestMethod]
        public void Add_Frames_To_Existing_TimeCode_2398_Ensure_Correct_Time()
        {
            var currentTimeCode = TimeCode.FromSeconds(1000D, SmpteFrameRate.Smpte2398);

            float expectedFrameCount = currentTimeCode.TotalFrames;

            for (int i = 1; i < 30000; i++)
            {
                expectedFrameCount++;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Iteration # {0} ExpectedFrames: {1}", i, expectedFrameCount);

                currentTimeCode = currentTimeCode.Add(TimeCode.FromFrames(1, SmpteFrameRate.Smpte2398));
                Console.WriteLine("currentTimeCode: " + currentTimeCode);
                Console.WriteLine("currentTimeCode.TotalFrames: " + currentTimeCode.TotalFrames);
                Console.WriteLine("currentTimeCode.TotalSeconds: " + currentTimeCode.TotalSeconds);
                Console.WriteLine("currentTimeCode.TotalSecondsPrecision: " + currentTimeCode.TotalSecondsPrecision);
                Console.WriteLine("currentTimeCode.ToTicks27Mhz: " + currentTimeCode.ToTicks27Mhz());
                Assert.AreEqual(expectedFrameCount, currentTimeCode.TotalFrames, "Expected frames {0} did not match TotalFrames {1}", expectedFrameCount, currentTimeCode.TotalFrames);
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// This test adds frames to an existing 2997 dropframe format timecode and makes sure it is correct.
        /// </summary>
        [TestMethod]
        public void AddFramesToExistingTimeCode2997DfEnsureCorrectTime()
        {
            var currentTimeCode = TimeCode.FromSeconds(1000D, SmpteFrameRate.Smpte2997Drop);

            float expectedFrameCount = currentTimeCode.TotalFrames;

            for (int i = 1; i < 30000; i++)
            {
                expectedFrameCount++;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Iteration # {0} ExpectedFrames: {1}", i, expectedFrameCount);

                currentTimeCode = currentTimeCode.Add(TimeCode.FromFrames(1, SmpteFrameRate.Smpte2997Drop));
                Console.WriteLine("currentTimeCode: " + currentTimeCode);
                Console.WriteLine("currentTimeCode.TotalFrames: " + currentTimeCode.TotalFrames);
                Console.WriteLine("currentTimeCode.TotalSeconds: " + currentTimeCode.TotalSeconds);
                Console.WriteLine("currentTimeCode.TotalSecondsPrecision: " + currentTimeCode.TotalSecondsPrecision);
                Console.WriteLine("currentTimeCode.ToTicks27Mhz: " + currentTimeCode.ToTicks27Mhz());
                Assert.AreEqual(expectedFrameCount, currentTimeCode.TotalFrames, "Expected frames {0} did not match TotalFrames {1}", expectedFrameCount, currentTimeCode.TotalFrames);
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// This test adds frames to an existing 2997 Non Drop format timecode and makes sure it is correct.
        /// </summary>
        [TestMethod]
        public void AddFramesToExistingTimeCode2997NdEnsureCorrectTime()
        {
            var currentTimeCode = TimeCode.FromSeconds(1000D, SmpteFrameRate.Smpte2997NonDrop);

            float expectedFrameCount = currentTimeCode.TotalFrames;

            for (int i = 1; i < 30000; i++)
            {
                expectedFrameCount++;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Iteration # {0} ExpectedFrames: {1}", i, expectedFrameCount);

                currentTimeCode = currentTimeCode.Add(TimeCode.FromFrames(1, SmpteFrameRate.Smpte2997NonDrop));
                Console.WriteLine("currentTimeCode: " + currentTimeCode);
                Console.WriteLine("currentTimeCode.TotalFrames: " + currentTimeCode.TotalFrames);
                Console.WriteLine("currentTimeCode.TotalSeconds: " + currentTimeCode.TotalSeconds);
                Console.WriteLine("currentTimeCode.TotalSecondsPrecision: " + currentTimeCode.TotalSecondsPrecision);
                Console.WriteLine("currentTimeCode.ToTicks27Mhz: " + currentTimeCode.ToTicks27Mhz());
                Console.WriteLine("currentTimeCode.PcrTB: " + currentTimeCode.ToTicksPcrTb());
                Assert.AreEqual(expectedFrameCount, currentTimeCode.TotalFrames, "Expected frames {0} did not match TotalFrames {1}", expectedFrameCount, currentTimeCode.TotalFrames);
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// This test adds frames to an existing 25 PAL format timecode and makes sure it is correct.
        /// </summary>
        [TestMethod]
        public void Add_Frames_To_Existing_TimeCode_25PAL_Ensure_Correct_Time()
        {
            var currentTimeCode = TimeCode.FromSeconds(1000D, SmpteFrameRate.Smpte25);

            long expectedFrameCount = currentTimeCode.TotalFrames;

            for (int i = 1; i < 30000; i++)
            {
                expectedFrameCount++;
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Iteration # {0} ExpectedFrames: {1}", i, expectedFrameCount);
                var oneFrame = TimeCode.FromFrames(1, SmpteFrameRate.Smpte25);
                Assert.AreEqual(1, (int)oneFrame.TotalFrames);
                Console.WriteLine("oneFrame.TotalFrames: " + oneFrame.TotalFrames);
                currentTimeCode = currentTimeCode.Add(oneFrame);

                Console.WriteLine("After AddOne: currentTimeCode: " + currentTimeCode);
                Console.WriteLine("currentTimeCode.TotalFrames: " + currentTimeCode.TotalFrames);
                Console.WriteLine("currentTimeCode.TotalSeconds: " + currentTimeCode.TotalSeconds);
                Console.WriteLine("currentTimeCode.TotalSecondsPrecision: " + currentTimeCode.TotalSecondsPrecision);
                Console.WriteLine("currentTimeCode.ToTicks27Mhz: " + currentTimeCode.ToTicks27Mhz());
                Console.WriteLine("currentTimeCode.PcrTB: " + currentTimeCode.ToTicksPcrTb());
                Assert.AreEqual((int)expectedFrameCount, (int)currentTimeCode.TotalFrames, "Expected frames {0} did not match TotalFrames {1}", expectedFrameCount, currentTimeCode.TotalFrames);
                Console.WriteLine("----------------------------------------");
            }
        }

        /// <summary>
        /// Determines whether this value equals one frame.
        /// </summary>
        [TestMethod]
        public void IsThisValueEqualToOneFrame()
        {
            const double OneFrame = 0.033366666666667;
            var oneSecTimeCode = new TimeCode(0, 0, 0, 1, SmpteFrameRate.Smpte2997NonDrop);

            var newTimeCode = new TimeCode(OneFrame, SmpteFrameRate.Smpte2997NonDrop);
            Assert.AreEqual(oneSecTimeCode.TotalFrames, newTimeCode.TotalFrames);
        }

        /// <summary>
        /// Determines if two timecode values with slightly differing absoluteTimes are still equal.
        /// </summary>
        [TestMethod]
        public void TwoTimecodesAreEqualDurationInSeconds()
        {
            var t1 = new TimeCode(2096.111665, SmpteFrameRate.Smpte2997Drop);
            var t2 = new TimeCode(2096.11166497009, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual(t1, t2, "The two timecodes are not evaluating to Equal as expected.");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_1()
        {
            const double Known = 1300.073D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("00:21:40:01", t.ToString(), "Not valid timecode for 1300.073");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_2()
        {
            const double Known = 355.04D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:05:54:20", t.ToString(), "Not valid timecode for 355.04D");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_3()
        {
            const double Known = 1315.736D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:21:54:12", t.ToString(), "Not valid timecode for 1315.736");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_4()
        {
            const double Known = 1655.112D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:27:33:13", t.ToString(), "Not valid timecode for 1655.112");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_6()
        {
            const double Known = 1754.315D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("00:29:12:16", t.ToString(), "Not valid timecode for 1754.315");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_7()
        {
            const double Known = 1926.613D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("00:32:06;18", t.ToString(), "Not valid timecode for 1926.613");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_8()
        {
            const double Known = 4965.337D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("01:22:45;09", t.ToString(), "Not valid timecode for 4965.337");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_9()
        {
            const double Known = 12342.52342D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("03:25:42;15", t.ToString(), "Not valid timecode for 12342.52342");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_10()
        {
            const double Known = 4885.23489D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:21:25:05", t.ToString(), "Not valid timecode for 4885.23489");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_11()
        {
            const double Known = 8948.233667D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte24);

            Assert.AreEqual("02:29:08:05", t.ToString(), "Not valid timecode for 8948.233667");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_12()
        {
            const double Known = 5797.133333D;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("01:36:37:04", t.ToString(), "Not valid timecode for 5797.133333");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_13()
        {
            const double Known = 56197.4333333333F;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("15:36:37:13", t.ToString(), "Not valid timecode for 56197.4333333333");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownAbsoluteTimeIsValid_14()
        {
            const long Known27Mhz = 43443649200;

            var t = new TimeCode(Known27Mhz, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("00:26:47:09", t.ToString(), "Not valid timecode for 43443649200 (27Mhz)");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_15()
        {
            const string Known2398Time = "00:26:47:09";

            var t = new TimeCode(Known2398Time, SmpteFrameRate.Smpte2398);

            Assert.AreEqual(38577, (int)t.TotalFrames, "Should be 38578 frames");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownAbsoluteTimeIsValid_16()
        {
            const string Known2398Time1 = "00:24:50:01";
            var t1 = new TimeCode(Known2398Time1, SmpteFrameRate.Smpte2398);
            Assert.AreEqual(35761, (int)t1.TotalFrames, "Should be 35761 frames");

            const string Known2398Time2 = "00:24:50:02";
            var t2 = new TimeCode(Known2398Time2, SmpteFrameRate.Smpte2398);
            Assert.AreEqual(35762, (int)t2.TotalFrames, "Should be 35762 frames");
        }

        [TestMethod]
        public void KnownTimecode_Smpte24_MatchesString1()
        {
            var t1 = new TimeCode(10, 10, 20, 5, SmpteFrameRate.Smpte24);
            string result = t1.ToString();
            const string Expected = "10:10:20:05";

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void KnownTimecode_Smpte24_MatchesString2()
        {
            var t1 = new TimeCode(0, 23, 20, 5, SmpteFrameRate.Smpte24);
            string result = t1.ToString();
            const string Expected = "00:23:20:05";

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void KnownTimecode_Smpte24_MatchesString3()
        {
            var t1 = new TimeCode(7, 01, 20, 5, SmpteFrameRate.Smpte24);
            string result = t1.ToString();
            const string Expected = "07:01:20:05";

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        [Ignore]
        public void KnownTimecode_Smpte24_MatchesString4_Days()
        {
            var t1 = new TimeCode(12, 22, 01, 20, 5, SmpteFrameRate.Smpte24);
            string result = t1.ToString();
            const string Expected = "12:22:01:20:05";

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        [Ignore]
        public void KnownTimecode_Smpte2997DF_MatchesString1_Days()
        {
            var t1 = new TimeCode(12, 22, 01, 20, 5, SmpteFrameRate.Smpte2997Drop);
            string result = t1.ToString();
            const string Expected = "12:22:01:20;05";

            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        [Ignore]
        public void KnownTimecode_Smpte299ND_MatchesString1_Days()
        {
            var t1 = new TimeCode(12, 22, 01, 20, 5, SmpteFrameRate.Smpte2997NonDrop);
            string result = t1.ToString();
            const string Expected = "12:22:01:20:05";

            Assert.AreEqual(Expected, result);
        }

        /// <summary>
        /// Checks the absolute time to frames algorithm for 2398.
        /// </summary>
        [TestMethod]
        public void CheckSomeAbsoluteTimeToFramesAlgorithmFor2398()
        {
            const decimal Epsilon = 0.00000000000000000000000001M;

            decimal absoluteTime = 23.481791666666666666666666648M;
            long frames = Convert.ToInt64(decimal.Floor(decimal.Round(24 * (1000 / 1001M) * (absoluteTime + Epsilon), 26)));
            Assert.AreEqual(562, frames, "wrong # of frames for this absolute Time.");

            absoluteTime = 23.48179166667M;
            frames = Convert.ToInt64(decimal.Floor(decimal.Round(24 * (1000 / 1001M) * (absoluteTime + Epsilon), 26)));
            Assert.AreEqual(563, frames, "wrong # of frames for this absolute Time");
        }

        [TestMethod]
        public void ShouldHandleMax2398Value()
        {
            const double Known = 86486.36;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("23:59:59:23", t.ToString(), "Not valid timecode for 86486.358291666700000");
        }

        [TestMethod]
        public void ShouldHandleMax24Value()
        {
            const double Known = 86399.958333333300000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte24);

            Assert.AreEqual("23:59:59:23", t.ToString(), "Not valid timecode for 86399.958333333300000");
        }

        [TestMethod]
        public void ShouldHandleMax25Value()
        {
            const double Known = 86399.960000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("23:59:59:24", t.ToString(), "Not valid timecode for 86399.960000000000000");
        }

        [TestMethod]
        public void ShouldHandleMax2997DFValue()
        {
            const double Known = 86399.880233333300000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("23:59:59;29", t.ToString(), "Not valid timecode for 86399.880233333300000");
        }

        [TestMethod]
        public void ShouldHandleMax2997NDValue()
        {
            const double Known = 86486.366633333300000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("23:59:59:29", t.ToString(), "Not valid timecode for 86486.366633333300000");
        }

        [TestMethod]
        public void ShouldHandleMax30Value()
        {
            const double Known = 86399.966666666700000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("23:59:59:29", t.ToString(), "Not valid timecode for 86399.966666666700000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn2398_1()
        {
            const double Known = 86486.400000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("01:00:00:00:00", t.ToString(), "Not valid timecode for 86486.400000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn2398_2()
        {
            const double Known = 86486.441708333300000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("01:00:00:00:01", t.ToString(), "Not valid timecode for 86486.441708333300000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownLongRunningAbsoluteTimeIsValidIn2398_3()
        {
            const int FrameCount = 4924825;

            var t = TimeCode.FromFrames(FrameCount, SmpteFrameRate.Smpte2398);

            Assert.AreEqual("02:09:00:01:01", t.ToString(), "Not valid timecode for 205406.242708333");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn24_1()
        {
            const double Known = 86400.000000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte24);

            Assert.AreEqual("01:00:00:00:00", t.ToString(), "Not valid timecode for 86400.000000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn24_2()
        {
            const double Known = 86400.041666666700000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte24);

            Assert.AreEqual("01:00:00:00:01", t.ToString(), "Not valid timecode for 86400.041666666700000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownLongRunningAbsoluteTimeIsValidIn24_3()
        {
            const long KnownTicks27 = 759108344625000;

            var t = new TimeCode(KnownTicks27, SmpteFrameRate.Smpte24);

            Assert.AreEqual("325:09:45:23:21", t.ToString(), "759108344625000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn25_1()
        {
            const double Known = 86400.000000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:00:00:00:00", t.ToString(), "Not valid timecode for 86400.000000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn25_2()
        {
            const double Known = 86400.040000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:00:00:00:01", t.ToString(), "Not valid timecode for 86400.040000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn25_3()
        {
            const double Known = 167462.200000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte25);

            Assert.AreEqual("01:22:31:02:05", t.ToString(), "Not valid timecode for 167462.200000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997ND_1()
        {
            const double Known = 86486.400000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("01:00:00:00:00", t.ToString(), "Not valid timecode for 86486.400000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997ND_2()
        {
            const double Known = 86486.433366666700000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("01:00:00:00:01", t.ToString(), "Not valid timecode for 86486.433366666700000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997ND_3()
        {
            const double Known = 309753.877766667000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual("03:13:57:24:13", t.ToString(), "Not valid timecode for 309753.877766667000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997DF_1()
        {
            const double Known = 86399.913600000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("01:00:00:00;00", t.ToString(), "Not valid timecode for 86399.913600000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997DF_2()
        {
            const double Known = 86399.946966666700000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("01:00:00:00;01", t.ToString(), "Not valid timecode for 86399.946966666700000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void KnownLongRunningAbsoluteTimeIsValidIn2997DF_3()
        {
            const double Known = 441921.313166667000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte2997Drop);

            Assert.AreEqual("05:02:45:21;23", t.ToString(), "Not valid timecode for 441921.313166667000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn30_1()
        {
            const double Known = 86400.000000000000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("01:00:00:00:00", t.ToString(), "Not valid timecode for 86400.000000000000000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn30_2()
        {
            const double Known = 86400.033333333300000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("01:00:00:00:01", t.ToString(), "Not valid timecode for 86400.033333333300000");
        }

        /// <summary>
        /// Knows the absolute time is valid.
        /// </summary>
        [TestMethod]
        public void KnownLongRunningAbsoluteTimeIsValidIn30_3()
        {
            const double Known = 334769.833333333000000;

            var t = new TimeCode(Known, SmpteFrameRate.Smpte30);

            Assert.AreEqual("03:20:59:29:25", t.ToString(), "Not valid timecode for 334769.833333333000000");
        }

        /// <summary>
        /// Creates the time code_2398 from string.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_2398FromString()
        {
            var timecode = new TimeCode("2:01:24:12:13", SmpteFrameRate.Smpte2398);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the time code_2398 from integers.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_2398FromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte2398);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 24fps from string.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_24FromString()
        {
            var timecode = new TimeCode("2:01:24:12:13", SmpteFrameRate.Smpte24);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 24fps from integers.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_24FromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte24);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 25fps from string.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_25FromString()
        {
            var timecode = new TimeCode("2:01:24:12:13", SmpteFrameRate.Smpte25);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 25fps from integers.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_25FromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte25);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode  2997 non drop from string.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_2997NonDropFromString()
        {
            var timecode = new TimeCode("2:01:42:12:20", SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("02:01:42:12:20", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 non drop from integers.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_2997NonDropFromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte2997NonDrop);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 drop from string.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CreateLongRunningTimeCode_2997DropFromString()
        {
            TimeCode timecode = new TimeCode("2:01:42:12;22", SmpteFrameRate.Smpte2997Drop);

            Console.WriteLine("TimeCode = {0}", timecode);
            Assert.AreEqual("02:01:42:12;22", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 2997 drop from integers.
        /// </summary>
        [TestMethod]
        [Ignore]
        public void CreateLongRunningTimeCode_2997DropFromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte2997Drop);

            Console.WriteLine("TimeCode = {0}", timecode);

            Assert.AreEqual("02:01:24:12;13", timecode.ToString());
        }

        /// <summary>
        /// Creates the timecode 30 from string.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_30FromString()
        {
            var timecode = new TimeCode("2:01:42:12:22", SmpteFrameRate.Smpte30);

            Console.WriteLine("TimeCode = " + timecode);
            Assert.AreEqual("02:01:42:12:22", timecode.ToString());
        }

        /// <summary>
        /// Creates the time code_30 from integers.
        /// </summary>
        [TestMethod]
        public void CreateLongRunningTimeCode_30FromIntegers()
        {
            var timecode = new TimeCode(2, 1, 24, 12, 13, SmpteFrameRate.Smpte30);

            Console.WriteLine("TimeCode = " + timecode);

            Assert.AreEqual("02:01:24:12:13", timecode.ToString());
        }

        [TestMethod]
        public void ValidateBadLongRunningTimecode()
        {
            bool valid = TimeCode.ValidateSmpte12MTimecode("-1:01:00:00:12");

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void GetTotalDays()
        {
            TimeCode t1 = new TimeCode("01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(0.0625, t1.TotalDays);
        }

        [TestMethod]
        public void GetLongRunningTotalDays()
        {
            TimeCode t1 = new TimeCode("01:01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(1.0625, t1.TotalDays);
        }

        [TestMethod]
        public void GetLongRunningTotalHours()
        {
            TimeCode t1 = new TimeCode("2:01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(49.5D, t1.TotalHours);
        }

        [TestMethod]
        public void GetLongRunningTotalMinutes()
        {
            TimeCode t1 = new TimeCode("2:01:30:00:00", SmpteFrameRate.Smpte2997NonDrop);

            Assert.AreEqual(2970D, t1.TotalMinutes);
        }

        [TestMethod]
        public void FromHours_LongRunning()
        {
            TimeCode t1 = TimeCode.FromHours(25.5, SmpteFrameRate.Smpte30);

            Assert.AreEqual(25.5D, t1.TotalHours);
        }

        [TestMethod]
        public void FromDays()
        {
            TimeCode t1 = TimeCode.FromDays(10.5, SmpteFrameRate.Smpte30);

            Assert.AreEqual(10.5D, t1.TotalDays);
        }
    }
}