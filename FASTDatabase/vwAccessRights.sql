CREATE VIEW [dbo].[vwAccessRights]
	AS SELECT        dbo.AccessRight.AccessID, dbo.AccessRight.EmployeeID, dbo.Employee.LastName + ', ' + dbo.Employee.FirstName AS FullName, dbo.AccessLevel.AccessMode, dbo.AccessLevel.Description, 
                         dbo.Department.GroupName
FROM            dbo.AccessRight INNER JOIN
                         dbo.AccessLevel ON dbo.AccessRight.AccessLevel = dbo.AccessLevel.AccessLevelID INNER JOIN
                         dbo.Employee ON dbo.AccessRight.EmployeeID = dbo.Employee.EmployeeID LEFT OUTER JOIN
                         dbo.Department ON dbo.AccessRight.DepartmentID = dbo.Department.DepartmentID