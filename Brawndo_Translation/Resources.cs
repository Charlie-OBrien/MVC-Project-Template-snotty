using System.Globalization;
using System.Resources;

namespace Brawndo_Translation
{
    public static class Resources
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("Brawndo_Translation.Resources.Strings", typeof(Resources).Assembly);

        public static string GetString(string key) =>
            _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;

        public static string HomePageTitle =>
            _resourceManager.GetString("HomePageTitle", CultureInfo.CurrentUICulture) ?? "Welcome to Brawndo";

        public static string HomePageDescription =>
            _resourceManager.GetString("HomePageDescription", CultureInfo.CurrentUICulture) ?? "Learn about building Web apps with ASP.NET Core";

        public static string PrivacyPageTitle =>
            _resourceManager.GetString("PrivacyPageTitle", CultureInfo.CurrentUICulture) ?? "Privacy Policy";

        public static string PrivacyPageContent =>
            _resourceManager.GetString("PrivacyPageContent", CultureInfo.CurrentUICulture) ?? "Use this page to detail your site's privacy policy.";

        public static string NavHome =>
            _resourceManager.GetString("NavHome", CultureInfo.CurrentUICulture) ?? "Home";

        public static string NavPrivacy =>
            _resourceManager.GetString("NavPrivacy", CultureInfo.CurrentUICulture) ?? "Privacy";

        public static string StudentsPageTitle =>
            _resourceManager.GetString("StudentsPageTitle", CultureInfo.CurrentUICulture) ?? "Students";

        public static string InstructorsPageTitle =>
            _resourceManager.GetString("InstructorsPageTitle", CultureInfo.CurrentUICulture) ?? "Instructors";

        public static string CoursesPageTitle =>
            _resourceManager.GetString("CoursesPageTitle", CultureInfo.CurrentUICulture) ?? "All Courses";

        public static string CoursesByDepartmentPageTitle =>
            _resourceManager.GetString("CoursesByDepartmentPageTitle", CultureInfo.CurrentUICulture) ?? "Courses for Department";

        public static string DepartmentsPageTitle =>
            _resourceManager.GetString("DepartmentsPageTitle", CultureInfo.CurrentUICulture) ?? "Departments";

        public static string PersonDetailsPageTitle =>
            _resourceManager.GetString("PersonDetailsPageTitle", CultureInfo.CurrentUICulture) ?? "Person Details";

        public static string CourseDetailsPageTitle =>
            _resourceManager.GetString("CourseDetailsPageTitle", CultureInfo.CurrentUICulture) ?? "Course Details";

        public static string DepartmentDetailsPageTitle =>
            _resourceManager.GetString("DepartmentDetailsPageTitle", CultureInfo.CurrentUICulture) ?? "Department Details";

        public static string BackToStudents =>
            _resourceManager.GetString("BackToStudents", CultureInfo.CurrentUICulture) ?? "Back to Students";

        public static string BackToInstructors =>
            _resourceManager.GetString("BackToInstructors", CultureInfo.CurrentUICulture) ?? "Back to Instructors";

        public static string BackToCourses =>
            _resourceManager.GetString("BackToCourses", CultureInfo.CurrentUICulture) ?? "Back to Courses";

        public static string BackToDepartments =>
            _resourceManager.GetString("BackToDepartments", CultureInfo.CurrentUICulture) ?? "Back to Departments";

        public static string BackToHome =>
            _resourceManager.GetString("BackToHome", CultureInfo.CurrentUICulture) ?? "Back to Home";

        public static string ViewByDepartment =>
            _resourceManager.GetString("ViewByDepartment", CultureInfo.CurrentUICulture) ?? "View by Department";

        public static string ViewAllCourses =>
            _resourceManager.GetString("ViewAllCourses", CultureInfo.CurrentUICulture) ?? "View All Courses";

        public static string TableHeaderID =>
            _resourceManager.GetString("TableHeaderID", CultureInfo.CurrentUICulture) ?? "ID";

        public static string TableHeaderName =>
            _resourceManager.GetString("TableHeaderName", CultureInfo.CurrentUICulture) ?? "Name";

        public static string TableHeaderTitle =>
            _resourceManager.GetString("TableHeaderTitle", CultureInfo.CurrentUICulture) ?? "Title";

        public static string TableHeaderCredits =>
            _resourceManager.GetString("TableHeaderCredits", CultureInfo.CurrentUICulture) ?? "Credits";

        public static string TableHeaderDepartment =>
            _resourceManager.GetString("TableHeaderDepartment", CultureInfo.CurrentUICulture) ?? "Department";

        public static string TableHeaderEnrollmentDate =>
            _resourceManager.GetString("TableHeaderEnrollmentDate", CultureInfo.CurrentUICulture) ?? "Enrollment Date";

        public static string TableHeaderHireDate =>
            _resourceManager.GetString("TableHeaderHireDate", CultureInfo.CurrentUICulture) ?? "Hire Date";

        public static string TableHeaderActions =>
            _resourceManager.GetString("TableHeaderActions", CultureInfo.CurrentUICulture) ?? "Actions";

        public static string ViewButton =>
            _resourceManager.GetString("ViewButton", CultureInfo.CurrentUICulture) ?? "View";

        public static string NoRecordsFound =>
            _resourceManager.GetString("NoRecordsFound", CultureInfo.CurrentUICulture) ?? "No records found.";

        public static string PersonNotFound =>
            _resourceManager.GetString("PersonNotFound", CultureInfo.CurrentUICulture) ?? "Person not found.";

        public static string NoCoursesFound =>
            _resourceManager.GetString("NoCoursesFound", CultureInfo.CurrentUICulture) ?? "No courses found.";

        public static string NoDepartmentsFound =>
            _resourceManager.GetString("NoDepartmentsFound", CultureInfo.CurrentUICulture) ?? "No departments found.";

        public static string FieldID =>
            _resourceManager.GetString("FieldID", CultureInfo.CurrentUICulture) ?? "ID";

        public static string FieldLastName =>
            _resourceManager.GetString("FieldLastName", CultureInfo.CurrentUICulture) ?? "Last Name";

        public static string FieldFirstName =>
            _resourceManager.GetString("FieldFirstName", CultureInfo.CurrentUICulture) ?? "First Name";

        public static string FieldType =>
            _resourceManager.GetString("FieldType", CultureInfo.CurrentUICulture) ?? "Type";

        public static string FieldEnrollmentDate =>
            _resourceManager.GetString("FieldEnrollmentDate", CultureInfo.CurrentUICulture) ?? "Enrollment Date";

        public static string FieldHireDate =>
            _resourceManager.GetString("FieldHireDate", CultureInfo.CurrentUICulture) ?? "Hire Date";

        public static string FieldBudget =>
            _resourceManager.GetString("FieldBudget", CultureInfo.CurrentUICulture) ?? "Budget";

        public static string FieldStartDate =>
            _resourceManager.GetString("FieldStartDate", CultureInfo.CurrentUICulture) ?? "Start Date";

        public static string FieldAdministrator =>
            _resourceManager.GetString("FieldAdministrator", CultureInfo.CurrentUICulture) ?? "Administrator";

        public static string CoursesForDepartment =>
            _resourceManager.GetString("CoursesForDepartment", CultureInfo.CurrentUICulture) ?? "Courses for Department";

        public static string SectionsTitle =>
            _resourceManager.GetString("SectionsTitle", CultureInfo.CurrentUICulture) ?? "Explore";

        public static string StudentsSection =>
            _resourceManager.GetString("StudentsSection", CultureInfo.CurrentUICulture) ?? "Students";

        public static string StudentsDescription =>
            _resourceManager.GetString("StudentsDescription", CultureInfo.CurrentUICulture) ?? "View all enrolled students";

        public static string InstructorsSection =>
            _resourceManager.GetString("InstructorsSection", CultureInfo.CurrentUICulture) ?? "Instructors";

        public static string InstructorsDescription =>
            _resourceManager.GetString("InstructorsDescription", CultureInfo.CurrentUICulture) ?? "View all instructors";

        public static string CoursesSection =>
            _resourceManager.GetString("CoursesSection", CultureInfo.CurrentUICulture) ?? "Courses";

        public static string CoursesDescription =>
            _resourceManager.GetString("CoursesDescription", CultureInfo.CurrentUICulture) ?? "View all courses";

        public static string DepartmentsSection =>
            _resourceManager.GetString("DepartmentsSection", CultureInfo.CurrentUICulture) ?? "Departments";

        public static string DepartmentsDescription =>
            _resourceManager.GetString("DepartmentsDescription", CultureInfo.CurrentUICulture) ?? "View all departments";

        public static string CreateCourseTitle =>
            _resourceManager.GetString("CreateCourseTitle", CultureInfo.CurrentUICulture) ?? "Create Course";

        public static string EditCourseTitle =>
            _resourceManager.GetString("EditCourseTitle", CultureInfo.CurrentUICulture) ?? "Edit Course";

        public static string BackToCourseDetails =>
            _resourceManager.GetString("BackToCourseDetails", CultureInfo.CurrentUICulture) ?? "Back to Course Details";

        public static string CourseNotFound =>
            _resourceManager.GetString("CourseNotFound", CultureInfo.CurrentUICulture) ?? "Course not found.";

        public static string SelectDepartment =>
            _resourceManager.GetString("SelectDepartment", CultureInfo.CurrentUICulture) ?? "Select a Department";

        public static string CreateButton =>
            _resourceManager.GetString("CreateButton", CultureInfo.CurrentUICulture) ?? "Create";

        public static string SaveButton =>
            _resourceManager.GetString("SaveButton", CultureInfo.CurrentUICulture) ?? "Save";

        public static string CreateDepartmentTitle =>
            _resourceManager.GetString("CreateDepartmentTitle", CultureInfo.CurrentUICulture) ?? "Create Department";

        public static string EditDepartmentTitle =>
            _resourceManager.GetString("EditDepartmentTitle", CultureInfo.CurrentUICulture) ?? "Edit Department";

        public static string BackToDepartmentDetails =>
            _resourceManager.GetString("BackToDepartmentDetails", CultureInfo.CurrentUICulture) ?? "Back to Department Details";

        public static string DepartmentNotFound =>
            _resourceManager.GetString("DepartmentNotFound", CultureInfo.CurrentUICulture) ?? "Department not found.";

        public static string EnrollmentsPageTitle =>
            _resourceManager.GetString("EnrollmentsPageTitle", CultureInfo.CurrentUICulture) ?? "Enrollments";

        public static string StudentGradePageTitle =>
            _resourceManager.GetString("StudentGradePageTitle", CultureInfo.CurrentUICulture) ?? "Student Grades";

        public static string EnrollmentsForStudent =>
            _resourceManager.GetString("EnrollmentsForStudent", CultureInfo.CurrentUICulture) ?? "Enrollments for Student";

        public static string EnrollmentsForCourse =>
            _resourceManager.GetString("EnrollmentsForCourse", CultureInfo.CurrentUICulture) ?? "Enrollments for Course";

        public static string FieldGrade =>
            _resourceManager.GetString("FieldGrade", CultureInfo.CurrentUICulture) ?? "Grade";

        public static string FieldGPA =>
            _resourceManager.GetString("FieldGPA", CultureInfo.CurrentUICulture) ?? "Grade Point Average";

        public static string CreateEnrollmentTitle =>
            _resourceManager.GetString("CreateEnrollmentTitle", CultureInfo.CurrentUICulture) ?? "Create Enrollment";

        public static string EditEnrollmentTitle =>
            _resourceManager.GetString("EditEnrollmentTitle", CultureInfo.CurrentUICulture) ?? "Edit Grade";

        public static string BackToEnrollments =>
            _resourceManager.GetString("BackToEnrollments", CultureInfo.CurrentUICulture) ?? "Back to Enrollments";

        public static string EnrollmentNotFound =>
            _resourceManager.GetString("EnrollmentNotFound", CultureInfo.CurrentUICulture) ?? "Enrollment not found.";

        public static string CourseInstructorsPageTitle =>
            _resourceManager.GetString("CourseInstructorsPageTitle", CultureInfo.CurrentUICulture) ?? "Course Instructors";

        public static string InstructorsForCourse =>
            _resourceManager.GetString("InstructorsForCourse", CultureInfo.CurrentUICulture) ?? "Instructors for Course";

        public static string CoursesForInstructor =>
            _resourceManager.GetString("CoursesForInstructor", CultureInfo.CurrentUICulture) ?? "Courses for Instructor";

        public static string AssignInstructorTitle =>
            _resourceManager.GetString("AssignInstructorTitle", CultureInfo.CurrentUICulture) ?? "Assign Instructor to Course";

        public static string SelectInstructor =>
            _resourceManager.GetString("SelectInstructor", CultureInfo.CurrentUICulture) ?? "Select an Instructor";

        public static string OfficeAssignmentsPageTitle =>
            _resourceManager.GetString("OfficeAssignmentsPageTitle", CultureInfo.CurrentUICulture) ?? "Office Assignments";

        public static string AssignOfficeTitle =>
            _resourceManager.GetString("AssignOfficeTitle", CultureInfo.CurrentUICulture) ?? "Assign Office";

        public static string EditOfficeTitle =>
            _resourceManager.GetString("EditOfficeTitle", CultureInfo.CurrentUICulture) ?? "Edit Office Assignment";

        public static string FieldLocation =>
            _resourceManager.GetString("FieldLocation", CultureInfo.CurrentUICulture) ?? "Location";

        public static string BackToOfficeAssignments =>
            _resourceManager.GetString("BackToOfficeAssignments", CultureInfo.CurrentUICulture) ?? "Back to Office Assignments";

        public static string DeleteButton =>
            _resourceManager.GetString("DeleteButton", CultureInfo.CurrentUICulture) ?? "Delete";

        public static string OfficeAssignmentNotFound =>
            _resourceManager.GetString("OfficeAssignmentNotFound", CultureInfo.CurrentUICulture) ?? "Office assignment not found.";

        public static string RemoveInstructor =>
            _resourceManager.GetString("RemoveInstructor", CultureInfo.CurrentUICulture) ?? "Remove Instructor";

        public static string EditButton =>
            _resourceManager.GetString("EditButton", CultureInfo.CurrentUICulture) ?? "Edit";
    }
}
