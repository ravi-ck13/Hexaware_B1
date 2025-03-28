create database HexaDB

use HexaDB

create table Clients 
(Client_ID numeric(4) primary key,
Cname varchar(40) not null,
Client_Address varchar(30),
Email varchar(30) unique,
Phone numeric(10),
Business varchar(20) not null )

create table Departments
(Deptno numeric(2) primary key,
Dname varchar(15) not null,
Loc varchar(20))

create table Employees
(Empno numeric(4) primary key,
Ename varchar(20) not null,
Job varchar(15),
Salary numeric(7) check(Salary>0),
Deptno numeric(2) foreign key(Deptno) references Departments(Deptno))

create table Projects
(Project_ID numeric(3) primary key,
Descr varchar(30) not null,
StartDate date,
Planned_End_Date date,
Actual_End_Date date ,
Budget numeric(10) check(Budget>0),
Client_ID numeric(4) foreign key(Client_ID) references Clients(Client_ID),
check(Actual_End_Date > Planned_End_Date))

create table EmpProjectTasks
(Project_ID numeric(3) foreign key(Project_ID) references Projects(Project_ID),
Empno numeric(4) foreign key(Empno) references Employees(Empno),
StartDate date,
End_Date date,
Task varchar(25) not null,
ProjectStatus varchar(15) not null,
primary key (Project_ID,Empno))

sp_help EmpProjectTasks 

drop table EmpProjectTasks

Insert into Clients values(1001, 'ACME Utilities', 'Noida', 'contact@acmeutil.com', 9567880032, 'Manufaturing'),
(1002, 'Trackon Consultants', 'Mumbai', 'consult@trackon.com', 8734210090, 'Consultant'),
(1003, 'MoneySaver Distributors', 'Kolkata', 'save@moneysaver.com', 7799886655, 'Reseller'),
(1004, 'Lawful Corp', 'Chennai', 'justice@lawful.com', 9210342219, 'Professional')

insert into Departments values(10, 'Design', 'Pune'),
(20, 'Development', 'Pune'),
(30, 'Testing', 'Mumbai'),
(40, 'Document', 'Mumbai')

insert into Employees values(7001, 'Sandeep', 'Analyst', 25000, 10),
(7002, 'Rajesh', 'Designer', 30000, 10),
(7003, 'Madhav', 'Developer', 40000, 20),
(7004, 'Manoj', 'Developer', 40000, 20),
(7005, 'Abhay', 'Designer', 35000, 10),
(7006, 'Uma', 'Tester', 30000, 30),
(7007, 'Gita', 'Tech. Writer', 30000, 40),
(7008, 'Priya', 'Tester', 35000, 30),
(7009, 'Nutan', 'Developer', 45000, 20),
(7010, 'Smita', 'Analyst', 20000, 10),
(7011, 'Anand', 'Project Mgr', 65000, 10)

insert into Projects values(401, 'Inventory', '01-Apr-11','01-Oct-11', '31-Oct-11', 150000, 1001)
insert into Projects values(402, 'Accounting', '01-Aug-11','01-Jan-12', null, 500000, 1002),
(403, 'Payroll', '01-Oct-11','31-Dec-11', null, 75000, 1003),
(404, 'Contact Mgmt', '01-Nov-11','31-Dec-11', null, 5000, 1004)

insert into EmpProjectTasks values(401, 7001, '01-Apr-11', '20-Apr-11',	'System Analysis', 'Completed'),
(401, 7002, '21-Apr-11', '30-May-11', 'System Design', 'Completed'),
(401, 7003,	'01-Jun-11', '15-Jul-11', 'Coding', 'Completed'),
(401, 7004,	'18-Jul-11', '01-Sep-11', 'Coding', 'Completed'),
(401, 7006,	'03-Sep-11', '15-Sep-11', 'Testing', 'Completed'),
(401, 7009,	'18-Sep-11', '05-Oct-11', 'Code Change', 'Completed'),
(401, 7008,	'06-Oct-11', '16-Oct-11', 'Testing', 'Completed'),
(401, 7007,	'06-Oct-11', '22-Oct-11', 'Documentation', 'Completed'),
(401, 7011,	'22-Oct-11', '31-Oct-11', 'Sign off', 'Completed'),
(402, 7010,	'01-Aug-11', '20-Aug-11', 'System Analysis', 'Completed'),
(402, 7002,	'22-Aug-11', '30-Sep-11', 'System Design', 'Completed'),
(402, 7004,	'01-Oct-11', null, 'Coding', 'In Progress')


select * from EmpProj

use HexaDB

create table DEPT 
(deptno int primary key,
dname varchar(15),
loc varchar(10))

create table EMP
(empno int primary key,
ename varchar(10),
job varchar(10),
mgr_id int, 
hiredate date,
sal numeric(7), 
comm int ,
deptno int foreign key(deptno) references DEPT(deptno))

insert into DEPT values(10, 'ACCOUNTING',    'NEW YORK'), 
(20 ,    'RESEARCH',      'DALLAS' ),
(30 ,    'SALES'    ,     'CHICAGO'), 
(40 ,    'OPERATIONS',    'BOSTON' )

insert into  EMP values
(7369, 'SMITH',   'CLERK',	  7902,	  '17-DEC-80', 	 800,   null,  20),
(7499, 'ALLEN',   'SALESMAN', 7698,   '20-FEB-81',   1600,  300,   30),
(7521, 'WARD',	  'SALESMAN', 7698,   '22-FEB-81',   1250,  500,   30),
(7566, 'JONES',   'MANAGER',  7839,   '02-APR-81',   2975,  null,  20),
(7654, 'MARTIN',  'SALESMAN', 7698,   '28-SEP-81',   1250,  1400,  30),
(7698, 'BLAKE',   'MANAGER',  7839,   '01-MAY-81',   2850,  null,  30),
(7782, 'CLARK',   'MANAGER',  7839,   '09-JUN-81',   2450,  null,  10),
(7788, 'SCOTT',   'ANALYST',  7566,   '19-APR-87',   3000,  null,  20),
(7839, 'KING',    'PRESIDENT',null,   '17-NOV-81',   5000,  null,  10),
(7844, 'TURNER',  'SALESMAN', 7698,   '08-SEP-81',   1500,   0,    30),
(7876, 'ADAMS',   'CLERK',    7788,   '23-MAY-87',   1100,  null,  20),
(7900, 'JAMES',  'CLERK',    7698,   '03-DEC-81',   950,   null,   30),
(7902, 'FORD',    'ANALYST',  7566,   '03-DEC-81',   3000,  null,  20),
(7934, 'MILLER',  'CLERK',    7782,   '23-JAN-82',	 1300,  null,  10)

select * from EMP

---set 1---

--1--List all employees whose name begins with 'A'. 
select * from EMP where ename like 'A%'
--2--Select all those employees who don't have a manager. 
select * from EMP where mgr_id is null
--3--List employee name, number and salary for those employees who earn in the range 1200 to 1400. 
select ename,empno, sal from EMP where sal between 1200 and 1400
--4--Give all the employees in the RESEARCH department a 10% pay rise. Verify that this has been done by listing all their details before and after the rise. 
select * from EMP e 
join DEPT d ON e.DEPTNO = d.DEPTNO 
where d.dname = 'RESEARCH'

select *, e.sal*1.10 as new_salary from EMP e 
join DEPT d ON e.DEPTNO = d.DEPTNO 
where d.dname = 'RESEARCH'
--5--Find the number of CLERKS employed. Give it a descriptive heading. 
select count(job) as NoOf_Clerks_Employed from EMP where job='clerk'
--6--Find the average salary for each job type and the number of people employed in each job.
select lower(job) as job, count(*) as emp_count, avg(sal) as avg_salary from emp group by job
--7--List the employees with the lowest and highest salary. 
select *  from EMP where sal = (select min(sal) from EMP) or sal = (select max(sal) from EMP)
--8--List full details of departments that don't have any employees. 
select d.* from dept d left join emp e on d.deptno = e.deptno  
where e.deptno is null
--9--Get the names and salaries of all the analysts earning more than 1200 who are based in department 20. Sort the answer by ascending order of name. 
select ename, sal from EMP where job='Analyst' and sal>1200 and deptno=20 order by ename asc
--10--For each department, list its name and number together with the total salary paid to employees in that department. 
select d.dname, d.deptno, sum(e.sal) as total_salary from dept d  
join emp e on d.deptno = e.deptno  
group by d.dname, d.deptno
--11--Find out salary of both MILLER and SMITH.
select ename, sal from emp where ename in ('miller', 'smith')
--12--Find out the names of the employees whose name begin with ‘A’ or ‘M’. 
select ename from emp where ename like 'A%' or ename like 'M%'
--13--Compute yearly salary of SMITH. 
select ename, sal*12 as annual_salary from emp where ename='smith'
--14--List the name and salary for all employees whose salary is not in the range of 1500 and 2850. 
select ename, sal from emp where sal not between 1500 and 2850
--15--Find all managers who have more than 2 employees reporting to them
--selfjoin
select e.mgr_id as manager_id, m.ename as manager_name, count(e.empno) as num_of_reports  
from emp e  
join emp m on e.mgr_id = m.empno  
group by e.mgr_id, m.ename  
having count(e.empno) > 2

---set 2---

--1--Retrieve a list of MANAGERS. 
select *from emp where job='Manager'
--2--Find out the names and salaries of all employees earning more than 1000 per month. 
select ename, sal from emp where sal>1000
--3--Display the names and salaries of all employees except JAMES. 
select ename, sal from emp where ename!= 'james'
--4--Find out the details of employees whose names begin with ‘S’. 
select *from emp where ename like 's%'
--5--Find out the names of all employees that have ‘A’ anywhere in their name. 
select * from emp where ename like '%A%'
--6--Find out the names of all employees that have ‘L’ as their third character in their name. 
select * from emp where ename like '___L%'
--7--Compute daily salary of JONES. 
select ename, sal / 30 as daily_salary from emp where ename = 'jones'
--8--Calculate the total monthly salary of all employees. 
select sum(sal) as total_salary from emp
--9--Print the average annual salary . 
select avg(sal * 12) as avg_annual_salary from emp
--10--Select the name, job, salary, department number of all employees except SALESMAN from department number 30. 
select ename, job, sal, deptno from emp where not(deptno = 30 and job = 'salesman')

--------------------------------------

---set 3---

--1--List unique departments of the EMP table.
select distinct deptno from emp

--2--List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
select ename as employee, sal as "monthly salary"  
from emp  
where sal > 1500 and deptno in (10, 30)

--3-- Display the name, job, and salary of all the employees whose job is MANAGER or 
--ANALYST and their salary is not equal to 1000, 3000, or 5000. 
select ename, job, sal  
from emp  
where job in ('manager', 'analyst')  
and sal not in (1000, 3000, 5000)

--4--Display the name, salary and commission for all employees whose commission 
--amount is greater than their salary increased by 10%. 
select ename, sal, comm  
from emp  
where comm > sal * 1.1

--5--Display the name of all employees who have two Ls in their name and are in 
--department 30 or their manager is 7782. 
select ename  
from emp  
where ename like '%l%l%'  
and (deptno = 30 or mgr_id = 7782)

--6--Display the names of employees with experience of over 30 years and under 40 yrs.
 --Count the total number of employees. 
select ename, datediff(year, hiredate, getdate()) as experience  
from emp  
where datediff(year, hiredate, getdate()) between 30 and 40

select count(*) as total_employees  
from emp  
where datediff(year, hiredate, getdate()) between 30 and 40

--7--Retrieve the names of departments in ascending order and their employees in descending order. 
select d.dname, e.ename  
from dept d  
left join emp e on d.deptno = e.deptno  
order by d.dname asc, e.ename desc

--8--Find out experience of MILLER. 
select ename, datediff(year, hiredate, getdate()) as experience  
from emp  
where ename = 'miller'

--9--Write a query to display all employee information where ename contains 5 or more characters
select *  
from emp  
where len(ename) >= 5

--10--Copy empno, ename of all employees from emp table who work for dept 10 into a new table called emp10
select empno, ename  
into emp10  
from emp  
where deptno = 10

---set 4---

--1--Write a SQL query to find those employees who receive a higher salary than the employee with ID 7566. Return their names
select ename  
from emp  
where sal > (select sal from emp where empno = 7566)

--2--Write a SQL query to find out which employees have the same designation as the employee whose ID is 7876. Return name, department no and job .
select ename, deptno, job  
from emp  
where job = (select job from emp where empno = 7876)

--3--Write a SQL query to find those employees who report to that manager whose name starts with a 'B' or 'C'. Return first name, employee ID and salary
select ename, empno, sal  
from emp  
where mgr_id in (select empno from emp where ename like 'b%' or ename like 'c%')
