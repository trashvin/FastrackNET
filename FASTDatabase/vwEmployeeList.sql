<<<<<<< HEAD
﻿CREATE VIEW [dbo].[vwEmployeeList]
	AS select e.EmployeeID,e.FirstName,e.MiddleName,e.LastName, (ISNUll(e.FirstName,'') + ' ' + ISNULL(e.MiddleName,'') + ISNULL(e.LastName,'')) as 'FullName' , 
e.ManagerID, e.Gender, e.PhoneNumber, e.EmailAddress, e.DepartmentID, d.DepartmentName, d.GroupName, e.PositionID, p.Description, 
e.IsCompany, e.CompanyName, e.Status 
from Employee e
left join Department d
on e.DepartmentID = d.DepartmentID
left join Position p
=======
﻿CREATE VIEW [dbo].[vwEmployeeList]
	AS select e.EmployeeID,e.FirstName,e.MiddleName,e.LastName, (ISNUll(e.FirstName,'') + ' ' + ISNULL(e.MiddleName,'') + ISNULL(e.LastName,'')) as 'FullName' , 
e.ManagerID, e.Gender, e.PhoneNumber, e.EmailAddress, e.DepartmentID, d.DepartmentName, d.GroupName, e.PositionID, p.Description, 
e.IsCompany, e.CompanyName, e.Status 
from Employee e
left join Department d
on e.DepartmentID = d.DepartmentID
left join Position p
>>>>>>> origin/master
on e.PositionID = p.PositionID