<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwMISList]
	AS SELECT        dbo.Employee.EmployeeID, dbo.Employee.FirstName, dbo.Employee.MiddleName, dbo.Employee.LastName, dbo.Employee.EmailAddress, dbo.Employee.PhoneNumber, dbo.Department.DepartmentID, 
                         dbo.Department.DepartmentName
FROM            dbo.AccessRight INNER JOIN
                         dbo.Employee ON dbo.AccessRight.EmployeeID = dbo.Employee.EmployeeID INNER JOIN
                         dbo.Department ON dbo.AccessRight.DepartmentID = dbo.Department.DepartmentID
=======
﻿CREATE VIEW [dbo].[vwMISList]
	AS SELECT        dbo.Employee.EmployeeID, dbo.Employee.FirstName, dbo.Employee.MiddleName, dbo.Employee.LastName, dbo.Employee.EmailAddress, dbo.Employee.PhoneNumber, dbo.Department.DepartmentID, 
                         dbo.Department.DepartmentName
FROM            dbo.AccessRight INNER JOIN
                         dbo.Employee ON dbo.AccessRight.EmployeeID = dbo.Employee.EmployeeID INNER JOIN
                         dbo.Department ON dbo.AccessRight.DepartmentID = dbo.Department.DepartmentID
>>>>>>> origin/master
WHERE         (dbo.AccessRight.AccessLevel = 3)