CREATE VIEW [dbo].[vwEmployees]
	AS SELECT        e.EmployeeID, e.FirstName, e.MiddleName, e.LastName, ISNULL(e.FirstName, '') + ' ' + ISNULL(e.MiddleName, '') + ISNULL(e.LastName, '') AS FullName, e.ManagerID, e.Gender, e.PhoneNumber, 
                         e.EmailAddress, e.DepartmentID, d.DepartmentName, d.GroupName, e.PositionID, p.Description, e.IsCompany, e.CompanyName, e.Status
FROM            dbo.Employee AS e LEFT OUTER JOIN
                         dbo.Department AS d ON e.DepartmentID = d.DepartmentID LEFT OUTER JOIN
                         dbo.Position AS p ON e.PositionID = p.PositionID