CREATE DATABASE HMBank
USE HMBank

create table Customers (
    customer_id int primary key,
    first_name varchar(50) not null,
    last_name varchar(50) not null,
    DOB date not null,
    email varchar(100) unique not null,
    phone_number varchar(15) unique not null,
    customer_Address text not null
)
create table Accounts (
    account_id int primary key,
    customer_id int not null,
    account_type varchar(20) not null check (account_type IN ('savings', 'current', 'zero_balance')),
    balance decimal(15,2) not null default 0.00,
    foreign key (customer_id) references Customers(customer_id) ON delete cascade
)
create table Transactions (
    transaction_id int primary key,
    account_id int not null,
    transaction_type varchar(20) not null check (transaction_type IN ('deposit', 'withdrawal', 'transfer')),
    amount decimal(15,2) not null,note
    transaction_date datetime default getdate(),
    foreign key (account_id) references Accounts(account_id) ON delete cascade
)

use HMBank

insert into Customers values
(1, 'John', 'Doe', '1990-05-12', 'john.doe@example.com', '1234567890', '123 Main St, NY'),
(2, 'Alice', 'Smith', '1985-09-23', 'alice.smith@example.com', '2345678901', '456 Oak St, CA'),
(3, 'Bob', 'Johnson', '1992-07-15', 'bob.johnson@example.com', '3456789012', '789 Pine St, TX'),
(4, 'Emma', 'Williams', '1988-11-30', 'emma.williams@example.com', '4567890123', '101 Maple St, FL'),
(5, 'David', 'Brown', '1995-03-17', 'david.brown@example.com', '5678901234', '202 Birch St, WA'),
(6, 'Sophia', 'Martinez', '1993-06-25', 'sophia.martinez@example.com', '6789012345', '303 Cedar St, IL'),
(7, 'James', 'Garcia', '1987-02-09', 'james.garcia@example.com', '7890123456', '404 Walnut St, OH'),
(8, 'Olivia', 'Miller', '1991-08-14', 'olivia.miller@example.com', '8901234567', '505 Cherry St, MI'),
(9, 'William', 'Davis', '1983-12-05', 'william.davis@example.com', '9012345678', '606 Aspen St, NV'),
(10, 'Mia', 'Rodriguez', '1994-04-19', 'mia.rodriguez@example.com', '0123456789', '707 Elm St, CO')

insert into Accounts values
(1, 1, 'savings', 5000.00),
(2, 2, 'current', 12000.50),
(3, 3, 'zero_balance', 0.00),
(4, 4, 'savings', 7500.75),
(5, 5, 'current', 22000.00),
(6, 6, 'savings', 4800.60),
(7, 7, 'zero_balance', 0.00),
(8, 8, 'current', 15000.25),
(9, 9, 'savings', 8300.40),
(10, 10, 'current', 19500.00)

insert into Transactions values
(1, 1, 'deposit', 1000.00, '2025-03-10 10:30:00'),
(2, 2, 'withdrawal', 500.00, '2025-03-11 12:45:00'),
(3, 3, 'deposit', 2000.00, '2025-03-12 15:20:00'),
(4, 4, 'transfer', 300.00, '2025-03-13 18:10:00'),
(5, 5, 'withdrawal', 2500.00, '2025-03-14 09:00:00'),
(6, 6, 'deposit', 350.75, '2025-03-15 11:15:00'),
(7, 7, 'transfer', 120.00, '2025-03-16 14:00:00'),
(8, 8, 'withdrawal', 800.00, '2025-03-17 16:30:00'),
(9, 9, 'deposit', 5000.00, '2025-03-18 19:45:00'),
(10, 10, 'transfer', 600.00, '2025-03-19 08:20:00')



--1--Write a SQL query to retrieve the name, account type and email of all customers. 
select first_name + ' ' + last_name as full_name, account_type, email  from customers  
join accounts on customers.customer_id = accounts.customer_id

--2--Write a SQL query to list all transaction corresponding customer.
select first_name + ' ' + last_name as full_name, transaction_type, amount, transaction_date  from customers  
join accounts on customers.customer_id = accounts.customer_id  
join transactions on accounts.account_id = transactions.account_id

--3--Write a SQL query to increase the balance of a specific account by a certain amount. 
update accounts  set balance = balance + 500 where account_id = 1

select * from Accounts

--4--Write a SQL query to Combine first and last names of customers as a full_name.
select first_name + ' ' + last_name as full_name from customers

--5--Write a SQL query to remove accounts with a balance of zero where the account type is savings.
delete from accounts where balance = 0 and account_type = 'savings'

--6--Write a SQL query to Find customers living in a specific city.
select * from customers where customer_address like '%123 Main St, NY%'

--7--Write a SQL query to Get the account balance for a specific account. 
select balance from accounts where account_id=2

--8--Write a SQL query to List all current accounts with a balance greater than $1,000. 
select *from accounts where account_type = 'current' and  balance>1000

--9--Write a SQL query to Retrieve all transactions for a specific account. 
select * from transactions where account_id=2

--10-- Write a SQL query to Calculate the interest accrued on savings accounts based on a given interest rate.
select account_id, balance, balance * 0.05 as interest_accrued from accounts  
where account_type = 'savings'

--11-- Write a SQL query to Identify accounts where the balance is less than a specified overdraft limit.
select * from accounts where balance<-300

--12--Write a SQL query to Find customers not living in a specific city.
select * from customers where customer_Address not like '%123 Main St, NY%'