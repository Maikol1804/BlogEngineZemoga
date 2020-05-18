# Blog Engine App

This is a Blog Engine using C# .Net Core 3.1 to Zemoga

## Problem

Build a .Net web app that allows to create, edit and publish text-based blog posts, with an approval flow where two different user types may interact.

## General comments:

This application was implemented using different clean code principles including the SOLID principles.

## Prerequisities

These are the prerequisites for the application to run:

* Visual Studio 2019
* .Net Core 3.1

An in-memory database was used so it is not necessary to have a database engine (`EntityFrameworkCore.InMemory` was used). Additionally the application can be run on `IIS Express` or `Docker`

## Step List to Run

1. Clone the github repository
2. Open `BlogEngine.sln` file
3. Run using `IIS Express`

Additionally, the app is deployed in azure at the following [link](https://blogenginezemoga.azurewebsites.net/) 

## Credentials

Those users are the ones that are created by default:

Rol | Username | Password |
------| ------ | ------ |
Writer| mbonilla | Z3moga.852 |
Writer| jmendez  | Z3moga.852 |
Editor| atovar  | Z3moga.963 |
Editor| ozapata  | Z3moga.963 |

## Endpoints API

* Get all post that are pending for approval:

	`GET` {domain}/api/approvalposts
	
	Response Example:
	```
    [
       {
          "title":"Test",
          "body":"This is just a test",
          "createdDate":"Sunday, 17 May 2020 21:42",
          "creatorFullName":"Maikol Bonilla",
          "id":1
       },
       {
          "title":"Test 2",
          "body":"This is another test",
          "createdDate":"Sunday, 17 May 2020 21:43",
          "creatorFullName":"Maikol Bonilla",
          "id":2
       }
    ]
	```

* Approve a post:
    
    `PUT` {domain}/api/approvalposts/{postId}/approve
	
* Reject a post:

    `PUT` {domain}/api/approvalposts/{postId}/reject

##### Estimated time to complete the test: `55 hours`

That's it :+1:
