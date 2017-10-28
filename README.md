# _Hair Salon Management Site_

#### _Code review project for Epicodus C# week 3_

#### By _**Rane Fields**_

## Description

_This app allows an administrator to manage employees and clients at a hair salon._

## Setup/Installation Requirements

_To download and use the source code of this project:_

* _Clone the project using this link: `https://gitlab.com/lydianlights-epicodus/csharp/hair-salon.git`_
* _Install `.NET Core 1.1`. You can get it  [here](https://github.com/dotnet/core/blob/master/release-notes/download-archives/1.1.4-download.md)._
* _An SQL server is required for this project. If you have no SQL server environment on your computer, you can get MAMP [here](https://www.mamp.info/en/downloads/)_
* _Configure your server to listen on port 8889 and start it_
* _Once logged into your server, run the following SQL commands:_
* `CREATE DATABASE rane_fields`
* `CREATE TABLE rane_fields.stylists (id INT NOT NULL AUTO_INCREMENT, name VARCHAR(255) NOT NULL, phone VARCHAR(255) NOT NULL, email VARCHAR(255) NOT NULL, PRIMARY KEY (id));`
* `CREATE TABLE rane_fields.clients (id INT NOT NULL AUTO_INCREMENT, name VARCHAR(255) NOT NULL, phone VARCHAR(255) NOT NULL, stylist_id INT NOT NULL, PRIMARY KEY (id));`
* _Open the project directory `HairSalon.Solution/HairSalon` using terminal or powershell_
* _From the directory `HairSalon.Solution/HairSalon` run the command `dotnet restore` to fetch the project dependencies._
* _The application can now compiled and started by using the command `dotnet run`. It will be hosted at `localhost:5000`_

## Project Specs

Project BDD specs can be found [here](/SPECS.md).

## Technologies Used

_This project is powered by the [ASP .NET](https://docs.microsoft.com/en-us/aspnet/core/) framework and uses the [C# MySQL Connector Library](https://dev.mysql.com/downloads/connector/net/) for MySQL integration._

## Known Bugs

* _Currently the 'client update' functionality requires unchanged fields to be reentered._

### License

*This page is hereby released as public domain. No permission necessary for modification and distribution.*

Copyright (c) 2017 **_Rane Fields_**
