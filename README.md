# TimeItUp

TimeItUp is an application for monitoring the time spent by the user on certain activities. The application provides the ability to count time, set alarms and store information about user activities and their duration.

The TimeItUp application consists of an API based on .NET 5 and a client application built using the Angular.js framework. The application's data warehouse service is a relational MS SQL Server database. Access to the stored data is performed using Entity Framewok Core in the lazy loading approach. Authentication and authorization of users is based on JWT token and ASP.NET Identity Core.

The following functionalities from the presented subject areas have been designed for the users of the application:

1. Users and their accounts:
+ logging in
+ registration of new account
+ password reset
+ editing personal data

2. Timers:
+ creating a timer
+ editing timer data
+ deleting timer
+ timer control start/pause/split/finish/reinstate
+ listing of active timers
+ listing of archived timers
+ displaying timer details
+ displaying timer summaries - total duration time/total countdown time/total paused time
+ listing timer alarms
+ listing timer related pauses
+ listing timer related splits

3. Alarms:
+ creating an alarm
+ editing alarm data
+ deleting an alarm
+ displaying the active alarm in a modal with sound effect

Login form:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_1.PNG "Login form")

Register form:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_3.PNG "Register form")

Reset password form:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_2.PNG "Reset password form")

Active timers list:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_4.PNG "Active timers")

Past timers table:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_5.PNG "Past timers")

Create timer form:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_6.PNG "Create timer form")

Change user data form:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_7.PNG "Change user data form")

Logout user modal:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_8.PNG "Logout user modal")

Timer details (control and info sections):

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_9.PNG "Timer details - control, info sections")

Timer details (timings and alarms sections):

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_10.PNG "Timer details - timings, alarms sections")

Timer details (splits section):

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_11.PNG "Timer details - splits section")

Timer details (pauses section):

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_12.PNG "Timer details - pauses section")

Create alarm modal:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_13.PNG "Create alarm modal")

Create alarm modal (datetime picker):

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_14.PNG "Create alarm modal - datetime picker")

Active alarm modal:

![alt text](https://github.com/Korag/DocumentationImages/blob/master/TimeItUp/TimeItUp_15.PNG "Active alarm modal")
