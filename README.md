# ZipFileEncoder
A simple program which accepts and sends an encoded Zip file to a backend Web Service. Done to practice my skills on .NET core and Docker

**How to start the application:**  
1.Go to the ZipFileEncorder folder  
2.Run docker-compose up  
3.Navigate to http://localhost:8999

**Credentials:**  
Please use the following credentials at the control panel to send a successful request  
Username: admin  
Password: rcFq{bj5b&}#UK7M

**Limitations:**  
The file submitted to the Data Management System is stored as a string in the database. Therefore a JSON structure exceeding the maximum string size will be truncated.

**Postman Documentation:**  
Please find the Postman generated API documentation for the Data Management System API.  
https://documenter.getpostman.com/view/8763376/SVmvSdqF?version=latest

**Improvements to make:**  
Following are a list of features that could not implement due to time constraints , but will be good to have those.  
-Better UX on the control panel  
-Unit testing  
-Logging ( errors etc )  
-A way to view the stored file data on the Data Management System  
