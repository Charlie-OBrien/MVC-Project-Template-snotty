-- Create InsertOfficeAssignment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertOfficeAssignment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertOfficeAssignment]
@InstructorID int,
@Location nvarchar(50)
AS
INSERT INTO dbo.OfficeAssignment (InstructorID, Location)
VALUES (@InstructorID, @Location);
IF @@ROWCOUNT > 0
BEGIN
SELECT [Timestamp] FROM OfficeAssignment
WHERE InstructorID=@InstructorID;
END
'
END
GO

--Create the UpdateOfficeAssignment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdateOfficeAssignment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateOfficeAssignment]
@InstructorID int,
@Location nvarchar(50),
@OrigTimestamp timestamp
AS
UPDATE OfficeAssignment SET Location=@Location
WHERE InstructorID=@InstructorID AND [Timestamp]=@OrigTimestamp;
IF @@ROWCOUNT > 0
BEGIN
SELECT [Timestamp] FROM OfficeAssignment
WHERE InstructorID=@InstructorID;
END
'
END
GO

-- Create the DeleteOfficeAssignment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeleteOfficeAssignment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteOfficeAssignment]
@InstructorID int
AS
DELETE FROM OfficeAssignment
WHERE InstructorID=@InstructorID;
'
END
GO

-- Create the DeletePerson stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeletePerson]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeletePerson]
@PersonID int
AS
DELETE FROM Person WHERE PersonID = @PersonID;
'
END
GO

-- Create the UpdatePerson stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePerson]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdatePerson]
@PersonID int,
@LastName nvarchar(50),
@FirstName nvarchar(50),
@HireDate datetime,
@EnrollmentDate datetime,
@Discriminator nvarchar(50)
AS
UPDATE Person SET LastName=@LastName,
FirstName=@FirstName,
HireDate=@HireDate,
EnrollmentDate=@EnrollmentDate,
Discriminator=@Discriminator
WHERE PersonID=@PersonID;
'
END
GO

-- Create the InsertPerson stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertPerson]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertPerson]
@LastName nvarchar(50),
@FirstName nvarchar(50),
@HireDate datetime,
@EnrollmentDate datetime,
@Discriminator nvarchar(50)
AS
INSERT INTO dbo.Person (LastName,
FirstName,
HireDate,
EnrollmentDate,
Discriminator)
VALUES (@LastName,
@FirstName,
@HireDate,
@EnrollmentDate,
@Discriminator);
SELECT SCOPE_IDENTITY() as NewPersonID;
'
END
GO

-- Create GetStudentGrades stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetStudentGrades]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetStudentGrades]
@StudentID int
AS
SELECT EnrollmentID, Grade, CourseID, StudentID FROM dbo.StudentGrade
WHERE StudentID = @StudentID
'
END
GO

-- Create GetDepartmentName stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetDepartmentName]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetDepartmentName]
@ID int,
@Name nvarchar(50) OUTPUT
AS
SELECT @Name = Name FROM Department
WHERE DepartmentID = @ID
'
END
GO

-- ==========================================================================
-- Delete stored procedures
-- ==========================================================================

-- Create the DeleteStudentGrade stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeleteStudentGrade]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteStudentGrade]
@EnrollmentID int
AS
DELETE FROM dbo.StudentGrade
WHERE EnrollmentID = @EnrollmentID;
'
END
GO

-- Create the DeleteStudentGradesByStudent stored procedure.
-- Clears the FK_StudentGrade_Student dependency before a Person is deleted.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeleteStudentGradesByStudent]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteStudentGradesByStudent]
@StudentID int
AS
DELETE FROM dbo.StudentGrade
WHERE StudentID = @StudentID;
'
END
GO

-- Create the DeleteCourseInstructor stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCourseInstructor]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteCourseInstructor]
@CourseID int,
@PersonID int
AS
DELETE FROM dbo.CourseInstructor
WHERE CourseID = @CourseID AND PersonID = @PersonID;
'
END
GO

-- Create the DeleteCourseInstructorsByPerson stored procedure.
-- Clears the FK_CourseInstructor_Person dependency before a Person is deleted.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCourseInstructorsByPerson]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteCourseInstructorsByPerson]
@PersonID int
AS
DELETE FROM dbo.CourseInstructor
WHERE PersonID = @PersonID;
'
END
GO

-- Create the DeletePersonAndDependents stored procedure.
-- Removes a person and every row that references them, in one transaction so a
-- mid-way failure cannot leave the person half-deleted.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[DeletePersonAndDependents]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeletePersonAndDependents]
@PersonID int
AS
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION;
DELETE FROM dbo.StudentGrade WHERE StudentID = @PersonID;
DELETE FROM dbo.CourseInstructor WHERE PersonID = @PersonID;
DELETE FROM dbo.OfficeAssignment WHERE InstructorID = @PersonID;
DELETE FROM dbo.Person WHERE PersonID = @PersonID;
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
THROW;
END CATCH
'
END
GO

-- ==========================================================================
-- Get stored procedures
-- ==========================================================================

-- Create the GetPerson stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetPerson]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetPerson]
@PersonID int
AS
SELECT PersonID, LastName, FirstName, HireDate, EnrollmentDate, Discriminator
FROM dbo.Person
WHERE PersonID = @PersonID;
'
END
GO

-- Create the GetPeople stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetPeople]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetPeople]
AS
SELECT PersonID, LastName, FirstName, HireDate, EnrollmentDate, Discriminator
FROM dbo.Person
ORDER BY LastName, FirstName;
'
END
GO

-- Create the GetPeopleByDiscriminator stored procedure.
-- Pass Student or Instructor to filter the Person table by subtype.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetPeopleByDiscriminator]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetPeopleByDiscriminator]
@Discriminator nvarchar(50)
AS
SELECT PersonID, LastName, FirstName, HireDate, EnrollmentDate, Discriminator
FROM dbo.Person
WHERE Discriminator = @Discriminator
ORDER BY LastName, FirstName;
'
END
GO

-- Create the GetDepartment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetDepartment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetDepartment]
@DepartmentID int
AS
SELECT DepartmentID, [Name], Budget, StartDate, Administrator
FROM dbo.Department
WHERE DepartmentID = @DepartmentID;
'
END
GO

-- Create the GetDepartments stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetDepartments]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetDepartments]
AS
SELECT DepartmentID, [Name], Budget, StartDate, Administrator
FROM dbo.Department
ORDER BY [Name];
'
END
GO

-- Create the GetCourse stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCourse]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCourse]
@CourseID int
AS
SELECT CourseID, Title, Credits, DepartmentID
FROM dbo.Course
WHERE CourseID = @CourseID;
'
END
GO

-- Create the GetCourses stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCourses]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCourses]
AS
SELECT CourseID, Title, Credits, DepartmentID
FROM dbo.Course
ORDER BY Title;
'
END
GO

-- Create the GetCoursesByDepartment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCoursesByDepartment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCoursesByDepartment]
@DepartmentID int
AS
SELECT CourseID, Title, Credits, DepartmentID
FROM dbo.Course
WHERE DepartmentID = @DepartmentID
ORDER BY Title;
'
END
GO

-- Create the GetOfficeAssignment stored procedure.
-- Callers need the row version from here before calling UpdateOfficeAssignment.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetOfficeAssignment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetOfficeAssignment]
@InstructorID int
AS
SELECT InstructorID, Location, [Timestamp]
FROM dbo.OfficeAssignment
WHERE InstructorID = @InstructorID;
'
END
GO

-- Create the GetOfficeAssignments stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetOfficeAssignments]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetOfficeAssignments]
AS
SELECT InstructorID, Location, [Timestamp]
FROM dbo.OfficeAssignment
ORDER BY Location;
'
END
GO

-- Create the GetStudentGrade stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetStudentGrade]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetStudentGrade]
@EnrollmentID int
AS
SELECT EnrollmentID, CourseID, StudentID, Grade
FROM dbo.StudentGrade
WHERE EnrollmentID = @EnrollmentID;
'
END
GO

-- Create the GetCourseGrades stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCourseGrades]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCourseGrades]
@CourseID int
AS
SELECT EnrollmentID, CourseID, StudentID, Grade
FROM dbo.StudentGrade
WHERE CourseID = @CourseID;
'
END
GO

-- ==========================================================================
-- Create/Insert stored procedures
-- ==========================================================================

-- Create the InsertCourse stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertCourse]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertCourse]
@Title nvarchar(50),
@Credits int,
@DepartmentID int
AS
INSERT INTO dbo.Course (Title, Credits, DepartmentID)
VALUES (@Title, @Credits, @DepartmentID);
SELECT SCOPE_IDENTITY() as NewCourseID;
'
END
GO

-- Create the UpdateCourse stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdateCourse]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateCourse]
@CourseID int,
@Title nvarchar(50),
@Credits int,
@DepartmentID int
AS
UPDATE dbo.Course SET Title=@Title, Credits=@Credits, DepartmentID=@DepartmentID
WHERE CourseID=@CourseID;
'
END
GO

-- Create the InsertDepartment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertDepartment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertDepartment]
@Name nvarchar(50),
@Budget money,
@StartDate datetime,
@Administrator int
AS
INSERT INTO dbo.Department (Name, Budget, StartDate, Administrator)
VALUES (@Name, @Budget, @StartDate, @Administrator);
SELECT SCOPE_IDENTITY() as NewDepartmentID;
'
END
GO

-- Create the UpdateDepartment stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdateDepartment]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateDepartment]
@DepartmentID int,
@Name nvarchar(50),
@Budget money,
@StartDate datetime,
@Administrator int
AS
UPDATE dbo.Department SET Name=@Name, Budget=@Budget, StartDate=@StartDate, Administrator=@Administrator
WHERE DepartmentID=@DepartmentID;
'
END
GO

-- Create the InsertStudentGrade stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertStudentGrade]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertStudentGrade]
@CourseID int,
@StudentID int,
@Grade nvarchar(1)
AS
INSERT INTO dbo.StudentGrade (CourseID, StudentID, Grade)
VALUES (@CourseID, @StudentID, @Grade);
SELECT SCOPE_IDENTITY() as NewEnrollmentID;
'
END
GO

-- Create the UpdateStudentGrade stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdateStudentGrade]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateStudentGrade]
@EnrollmentID int,
@Grade nvarchar(1)
AS
UPDATE dbo.StudentGrade SET Grade=@Grade
WHERE EnrollmentID=@EnrollmentID;
'
END
GO

-- Create the InsertCourseInstructor stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[InsertCourseInstructor]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[InsertCourseInstructor]
@CourseID int,
@PersonID int
AS
INSERT INTO dbo.CourseInstructor (CourseID, PersonID)
VALUES (@CourseID, @PersonID);
'
END
GO

-- Create the UpdateCourseInstructor stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[UpdateCourseInstructor]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateCourseInstructor]
@CourseID int,
@PersonID int
AS
DELETE FROM dbo.CourseInstructor WHERE CourseID=@CourseID AND PersonID=@PersonID;
INSERT INTO dbo.CourseInstructor (CourseID, PersonID)
VALUES (@CourseID, @PersonID);
'
END
GO

-- Create the GetCourseInstructor stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCourseInstructor]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCourseInstructor]
@CourseID int,
@PersonID int
AS
SELECT CourseID, PersonID
FROM dbo.CourseInstructor
WHERE CourseID=@CourseID AND PersonID=@PersonID;
'
END
GO

-- Create the GetCourseInstructors stored procedure.
IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'[dbo].[GetCourseInstructors]')
AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetCourseInstructors]
@CourseID int
AS
SELECT CourseID, PersonID
FROM dbo.CourseInstructor
WHERE CourseID=@CourseID;
'
END
GO

