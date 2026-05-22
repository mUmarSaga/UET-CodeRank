using System.Collections.Generic;
using UET_CODERANK.DL;
using UET_CODERANK.Model;

namespace UET_CODERANK.BL
{
    public static class EnrollmentBL
    {
        public static string SendRequest(int studentId, int sectionId)
        {
            if (CurrentSession.Student.IsApproved)
                return "You are already enrolled in a section";

            if (EnrollmentDL.HasPendingRequest(studentId))
                return "You already have a pending request. Please wait for admin approval";

            bool success = EnrollmentDL.SendRequest(studentId, sectionId);
            if (success) return null;
            return "Failed to send request. Please try again";
        }

        public static string ApproveRequest(int requestId, int studentId, int sectionId)
        {
            bool success = EnrollmentDL.ApproveRequest(requestId, studentId, sectionId);
            if (success) return null;
            return "Failed to approve request";
        }

        public static string RejectRequest(int requestId)
        {
            bool success = EnrollmentDL.RejectRequest(requestId);
            if (success) return null;
            return "Failed to reject request";
        }

        public static List<EnrollementRequest> GetPendingRequests()
        {
            return EnrollmentDL.GetAllPending();
        }
    }
}