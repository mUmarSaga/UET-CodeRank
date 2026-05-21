using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UET_CODERANK.BL
{
    public class LeetCodeStatBL
    {
        public static bool GetLeetCodeStat(Model.Student student)
        {
            if(student.LeetcodeUsername == null)
            {
                return false;
            }
            Model.LeetCodeStat stats =DL.LeetCodeAPI.GetSolvedStats(student.LeetcodeUsername);
            if(stats == null)
            {
                return false;
            }
            stats.Student_id = student.Id;
            DL.LeetCodeStatDL.AddLeetCodeStat(stats);
            return true;
        }
        public static void UpdateLeetCodeStat(Model.Student student)
        {
            if (student.LeetcodeUsername == null)
            {
                return;
            }
            Model.LeetCodeStat stats = DL.LeetCodeAPI.GetSolvedStats(student.LeetcodeUsername);
            if (stats == null)
            {
                return;
            }
            stats.Student_id = student.Id;
            DL.LeetCodeStatDL.UpdateLeetCodeStat(stats);
        }


    }
}
