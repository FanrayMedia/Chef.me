# Chef.me: [AngularJS](https://www.angularjs.org/) [ASP.NET MVC](http://www.asp.net/mvc) Starter Kit

A starter kit for AngularJS 1.x and ASP.NET MVC 5.  This app simulates the basic functionality of [About.me](https://about.me) for chefs.

<img src="http://i.imgur.com/YUL7Sy6.png" title="Chef.me Screen Shot" />

## Stack

* [AngularJS](https://www.angularjs.org/) v1.4.8
* [Bootstrap](http://getbootstrap.com/) v3.3.6
* [jQuery](http://jquery.com/) 2.1.4
* [jQuery UI](http://jqueryui.com/) 1.11.4 (Customized download just for draggable and droppable)
* [ASP.NET MVC](http://www.asp.net/mvc) 5 (.NET Framework 4.6.1)
* [Entity Framework](http://www.asp.net/entity-framework) 6
* [SQL Server](https://www.microsoft.com/en-us/server-cloud/products/sql-server-editions/sql-server-express.aspx)

## Installation

* Download or clone repo to your local
* Open SQL Server and create a database ChefMe and run Sql/Chef.sql
* Open sln and go to web.config and you may update the following:
  * Connection string and make sure it points to the right database
  * Google+ client id and secret if you want to login with google+, 
  you can set it up at [Google Developer Console](https://console.developers.google.com/)
  * SendGrid username and password if you want to be able to email your connections,
  you can set it up at [SendGrid](https://sendgrid.com/) or through [Windows Azure Portal](https://azure.microsoft.com/) with SendGrid app
* Ctrl + F5

## Known Issues

* Some of the plugins used on the profile designer don't work on IE

## vNext

* AngularJS 2
* Bootstrap 4
* ASP.NET 5 / MVC 6 / EF 7

## Follow Me

[@FanrayMeida](https://twitter.com/FanrayMedia)