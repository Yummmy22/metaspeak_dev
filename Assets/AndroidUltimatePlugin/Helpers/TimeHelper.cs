using System;

namespace Gigadrillgames.AUP.Helpers
{
    public class TimeHelper
    {
        public static double GetMilliSecondSinceEpoch()
        {
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long ms = ((long) (DateTime.UtcNow - epochStart).TotalMilliseconds);
            return ms;
        }
    }
}