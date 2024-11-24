# BpmnEngine

External BPMN processing engine based on Camunda diagrams.

Please note: This project is a proof of concept on how to use Camunda API and BPMN diagrams and external tasks. It was created for the sole purpose of demonstrating a way of integrating Camunda 7 with .NET. This code makes little attempt to mitigate security risks (it does have a simple login panel though), and care should be taken before using the source code in this repo in a live environment. I advise to learn how I did it and then to develop your own project. This would also be the best way to learn.

## Features developed in BpmnEngine.Application

- <TargetFramework>**net6.0**</TargetFramework>
- Support for **External Service Tasks** with variables
- Support for external process start-up procedures
- Support for message handling via the API to broadcast events
- Two HTML forms in Razor which start a Camunda process via the API
- One HTML form in Razor used for request rejection and approval
- Automated e-mail notifications with a link to go to the decision page
- Process completion notification is also sent via e-mail
- The web app has a database layer to log filled-in forms
- The web app has user logins and hashed passwords stored in the local database for ease of use
- Support for user authentication has been added to separate different job positions of the mock employees involved

![diagram.png](/wiki/diagram.png)
