using TwistingNether.DataAccess.Configuration;

namespace TwistingNether.Core
{
    public class Common
    {

        public DateTime GetLastReset()
        {
            DateTime lastTuesday = DateTime.Now.AddDays(-1);
            while (lastTuesday.DayOfWeek != DayOfWeek.Tuesday)
            {
                lastTuesday = lastTuesday.AddDays(-1);
            }
            return lastTuesday;
        }

        public async Task<string> GetClassColor(string char_class)
        {
            
            var color = AppConstants.CharacterClasses
                                     .Where(c => c.ClassName == char_class)
                                     .Select(c => c.ColorCode).FirstOrDefault();

            return color ?? "black"; // Return "black" if no match found
        }
    }

}
