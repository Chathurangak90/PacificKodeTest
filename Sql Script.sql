USE [Test_DB]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 5/22/2025 2:02:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [nvarchar](10) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Age] [int] NOT NULL,
	[Salary] [decimal](18, 2) NOT NULL,
	[DepartmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteDepartment]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Chathuranga Karunarathne
-- Create date: 2025-05-21
-- Description:	Delete Department
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteDepartment]
    @DepartmentId INT,
    @Id INT = NULL OUTPUT,
    @Message NVARCHAR(255) = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if department exists
    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentId = @DepartmentId)
    BEGIN
        -- Check if department is used by any employee
        IF EXISTS (SELECT 1 FROM Employees WHERE DepartmentId = @DepartmentId)
        BEGIN
            SET @Id = -2;
            SET @Message = 'Cannot delete. Department is assigned to one or more employees.';
        END
        ELSE
        BEGIN
            DELETE FROM Departments WHERE DepartmentId = @DepartmentId;

            SET @Id = 1;
            SET @Message = 'Department deleted successfully.';
        END
    END
    ELSE
    BEGIN
        SET @Id = -1;
        SET @Message = 'Department not found.';
    END

    SELECT @Id AS StatusId, @Message AS Message;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteEmployee]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Chathuranga Karunarathne
-- Create date: 2025-05-21
-- Description:	Delete Employee
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteEmployee]
    @EmployeeId INT,
    @Id INT = NULL OUTPUT,
    @Message NVARCHAR(255) = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeId = @EmployeeId)
    BEGIN
        DELETE FROM Employees WHERE EmployeeId = @EmployeeId;

        SET @Id = 1;
        SET @Message = 'Employee deleted successfully.';
    END
    ELSE
    BEGIN
        SET @Id = -1;
        SET @Message = 'Employee not found.';
    END

    SELECT @Id AS StatusId, @Message AS Message;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllDepartments]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,Chathuranga Karunarathne>
-- Create date: <Create Date,2025-05-21>
-- Description:	<Description,Get all departments>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllDepartments]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        DepartmentId,
        DepartmentCode,
        DepartmentName
    FROM Departments
    ORDER BY DepartmentName;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllEmployees]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,Chathuranga Karunarathne>
-- Create date: <Create Date,2025-05-21>
-- Description:	<Description,Get All Employees>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllEmployees]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.EmployeeId,
        e.FirstName,
        e.LastName,
        e.Email,
        e.DateOfBirth,
        e.Age,
        e.Salary,
        e.DepartmentId,
        d.DepartmentName
    FROM 
        Employees e
    INNER JOIN 
        Departments d ON e.DepartmentId = d.DepartmentId
    ORDER BY 
        e.EmployeeId;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoadDepEmpCount]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,Chathuranga Karunarthne>
-- Create date: <Create Date,2025-05-21>
-- Description:	<Description,Get count>
-- =============================================
CREATE   PROCEDURE [dbo].[sp_LoadDepEmpCount]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        (SELECT COUNT(*) FROM Departments) AS DepartmentCount,
        (SELECT COUNT(*) FROM Employees) AS EmployeeCount;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUpdateDepartment]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Chathuranga Karunarathne>
-- Create date: <Create Date,2025-05-21>
-- Description:	<Description,Save Update Department>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveUpdateDepartment]
    @DepartmentId INT,
    @DepartmentCode NVARCHAR(50),
    @DepartmentName NVARCHAR(100),
    @Id INT = NULL OUTPUT,
    @Message NVARCHAR(255) = NULL OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Departments WHERE DepartmentId = @DepartmentId)
    BEGIN
        UPDATE Departments
        SET DepartmentCode = @DepartmentCode,
            DepartmentName = @DepartmentName
        WHERE DepartmentId = @DepartmentId;

        SET @Id = 1;
        SET @Message = 'Department updated successfully.';
    END
    ELSE
    BEGIN
        INSERT INTO Departments (DepartmentCode, DepartmentName)
        VALUES (@DepartmentCode, @DepartmentName);

        SET @Id = 1;
        SET @Message = 'Department added successfully.';
    END

    SELECT @Id AS StatusId, @Message AS Message;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SaveUpdateEmployee]    Script Date: 5/22/2025 2:02:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,Chathuranga Karunarathne>
-- Create date: <Create Date,2025-05-21>
-- Description:	<Description,Save Update Employee>
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveUpdateEmployee]
    @EmployeeId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(255),
    @DateOfBirth DATE,
    @Age INT,
    @Salary DECIMAL(18,2),
    @DepartmentId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeId = @EmployeeId)
        BEGIN
            -- Update if EmployeeId exists
            UPDATE Employees
            SET 
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                DateOfBirth = @DateOfBirth,
                Age = @Age,
                Salary = @Salary,
                DepartmentId = @DepartmentId
            WHERE EmployeeId = @EmployeeId;

            SELECT StatusId = 1, Message = 'Employee updated successfully.';
        END
        ELSE
        BEGIN
            -- Insert if EmployeeId doesn't exist
            INSERT INTO Employees (
                FirstName,
                LastName,
                Email,
                DateOfBirth,
                Age,
                Salary,
                DepartmentId
            )
            VALUES (
                @FirstName,
                @LastName,
                @Email,
                @DateOfBirth,
                @Age,
                @Salary,
                @DepartmentId
            );

            SELECT StatusId = 1, Message = 'Employee inserted successfully.';
        END
    END TRY
    BEGIN CATCH
        SELECT StatusId = -1, Message = ERROR_MESSAGE();
    END CATCH
END
GO
