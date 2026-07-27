# Navigation Guide

Complete navigation structure for the Brawndo MVC application.

## Entry Point: Home Page

Route: `/[language]/Home/Index`

Three main sections:
1. **People Management** - Students, Instructors, Office Assignments
2. **Academic Management** - Courses, Departments, Course Instructors
3. **Enrollment Management** - Enrollments

## Navigation Paths

### Students
- Home → `/Person/Students` (list)
- List → `/Person/Details/{id}` (detail)
- Detail → Back to Home

### Instructors
- Home → `/Person/Instructors` (list)
- List → `/Person/Details/{id}` (detail)
- Detail → Back to Home

### Courses
- Home → `/Course/Index` (list)
- List → `/Course/Create` (create form)
- List → `/Course/Details/{id}` (detail)
- Detail → `/Course/Edit/{id}` (edit form)
- Detail → Back to Home
- List has "View by Department" link

### Departments
- Home → `/Department/Index` (list)
- List → `/Department/Create` (create form)
- List → `/Department/Details/{id}` (detail)
- Detail → `/Department/Edit/{id}` (edit form)
- Detail → Back to Home

### Course Instructors
- Home → `/CourseInstructor/Index` (list)
- List → `/CourseInstructor/Create` (create form)
- List → `/CourseInstructor/ByCourse/{courseId}` (by course)
- List → `/CourseInstructor/ByInstructor/{instructorId}` (by instructor)
- Detail → Back to Home

### Office Assignments
- Home → `/OfficeAssignment/Index` (list)
- List → `/OfficeAssignment/Create` (create form)
- List → `/OfficeAssignment/Details/{id}` (detail)
- Detail → `/OfficeAssignment/Edit/{id}` (edit form)
- Detail → Back to Home

### Enrollments
- Home → `/StudentGrade/Index` (list)
- List → `/StudentGrade/Create` (create form)
- List → `/StudentGrade/ByStudent/{studentId}` (by student)
- List → `/StudentGrade/ByCourse/{courseId}` (by course)
- Detail → `/StudentGrade/Edit/{enrollmentId}` (edit form)
- Detail → Back to Home

## Key Features

✅ All views reachable from Home Index
✅ Language parameter preserved on all links
✅ Create buttons on all Index pages
✅ Logical back navigation to parent views
✅ Keyboard accessible
✅ Screen reader friendly with ARIA labels
✅ Skip-to-content link for keyboard users
