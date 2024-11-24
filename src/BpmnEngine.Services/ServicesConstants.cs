namespace BpmnEngine.Services;

public sealed class ServicesConstants
{
    public const int DefaultLockDuration = 10_000;

    public static class Messages
    {
        public const string ManagerApproved = "MGRAPPROVED";
        public const string ManagerRejected = "MGRREJECTED";
        public const string DirectorApproved = "DIRAPPROVED";
        public const string DirectorRejected = "DIRREJECTED";
        public const string VerificationDone = "VERIFICATIONDONE";
        public const string BouDirectorApproved = "BOUDIRAPPROVEDVERIFIED";
        public const string BouDirectorRejected = "BOUDIRREJECTEDVERIFIED";
    }

    public static class Topics
    {
        public const string ManagerChecks = "manager-checks";
        public const string BouDirectorChecks = "bou-director-checks";
        public const string Verification = "bou-verification";
        public const string DirectorChecks = "director-checks";
        public const string Accepted = "inform-sender-accepted";
        public const string Rejected = "inform-sender-rejected";
    }

    public static class FormHandlingVariables
    {
        public const string Start = "start";
        public const string LastStep = "last_step";
        public const string Position = "position";
        public const string UserName = "user_name";

        public const string PhoneNumber = "phone_number";
        public const string Destination = "destination";
        public const string NumberOfPeople = "no_of_people";
        public const string Room = "room";
    }

    public static class ProcessBpmnDiagrams
    {
        public const string FormHandling = "FormHandlingExtended";
        public const string Test = "TestProcess";
    }
}