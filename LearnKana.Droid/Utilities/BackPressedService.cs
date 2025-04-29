using System;
using System.Collections.Generic;

namespace LearnKana.Droid.Utilities
{
    public static class BackPressedService
    {
        private static Dictionary<string, DateTimeOffset> LastBackPress { get; } = [];

        public static bool OnBackPressed(string key, TimeSpan delay)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset lastPress = LastBackPress.GetValueOrDefault(key, DateTimeOffset.UnixEpoch);
            bool pressedAfterDelay = lastPress.Add(delay) <= now;
            if (pressedAfterDelay)
            {
                LastBackPress[key] = DateTimeOffset.UtcNow;
                return false;
            }
            else
                return true;
        }
    }
}