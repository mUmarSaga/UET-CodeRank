using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    public static class CurrentSession
    {
        public static Student Student { get; private set; }
        public static Admin Admin { get; private set; }
        public static LeetCodeStat leetCodeStat { get; private set; }
        public static bool IsAdmin => Admin != null;
        public static bool IsLoggedIn => Student != null || Admin != null;
        public static string SectionName { get; private set; } = "Not Enrolled";
        public static string SessionName { get; private set; } = "-";

        public static void SetStudent(Student student)
        {
            Student = student;
            Admin = null;
        }
        public static void SetSectionAndSession(string sectionName, string sessionName)
        {
            SectionName = sectionName;
            SessionName = sessionName;
        }
        public static void SetLeetCodeStat(LeetCodeStat stat)
        {
            leetCodeStat = stat;
        }
        public static void SetAdmin(Admin admin)
        {
            Admin = admin;
            Student = null;
        }

        public static void Clear()
        {
            Student = null;
            Admin = null;
        }
        public static event Action ProfileUpdated;

        public static void NotifyProfileUpdated()
        {
            ProfileUpdated?.Invoke();
        }
        public static event Action<Type> NavigationRequested;

        public static void RequestNavigation(Type pageType)
        {
            NavigationRequested?.Invoke(pageType);
        }
    }
}
