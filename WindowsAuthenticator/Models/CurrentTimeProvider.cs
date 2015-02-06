using System;

namespace WindowsAuthenticator.Models
{
    internal static class CurrentTimeProvider
    {
        public const int Period = 30;

        public static readonly DateTime StartOfUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long GetCurrentCounter()
        {
            return GetCurrentCounter(DateTime.UtcNow, StartOfUnixEpoch, Period);
        }

        private static long GetCurrentCounter(DateTime now, DateTime baseTime, int periodOfSamePassword)
        {
            return (long)(now - baseTime).TotalSeconds / periodOfSamePassword;
        }

        public static double GetCurrentTime()
        {
            return GetCurrentTime(DateTime.UtcNow, StartOfUnixEpoch, Period);
        }

        private static double GetCurrentTime(DateTime now, DateTime baseTime, int periodOfSamePassword)
        {
            double secondsFromBaseTime = (now - baseTime).TotalSeconds;

            var numberOfFullPeriods = (long) secondsFromBaseTime/periodOfSamePassword;

            return secondsFromBaseTime - numberOfFullPeriods*periodOfSamePassword;
        }
    }
}