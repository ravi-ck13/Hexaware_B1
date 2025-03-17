CREATE DATABASE HMBank;
USE HMBank;

create table Customers (
    customer_id int primary key,
    first_name varchar(50) not null,
    last_name varchar(50) not null,
    DOB date not null,
    email varchar(100) unique not null,
    phone_number varchar(15) unique not null,
    customer_Address text not null
);
create table Accounts (
    account_id int primary key,
    customer_id int not null,
    account_type varchar(20) not null check (account_type IN ('savings', 'current', 'zero_balance')),
    balance decimal(15,2) not null default 0.00,
    foreign key (customer_id) references Customers(customer_id) ON delete cascade
);
create table Transactions (
    transaction_id int primary key,
    account_id int not null,
    transaction_type varchar(20) not null check (transaction_type IN ('deposit', 'withdrawal', 'transfer')),
    amount decimal(15,2) not null,note
    transaction_date datetime default getdate(),
    foreign key (account_id) references Accounts(account_id) ON delete cascade
);

