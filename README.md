# Finance Manager repository

!Work in progress!

## Table of contents

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">About the project</a></li>
    <li><a href="#technologies">Technologies</a></li>
    <li><a href="#getting-started">Getting Started</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

## About The Project

Finance manager is a project that allows user to:

1.  Manage monthly budget
2.  Manage expenses tracking
3.  Manage couples or families finance with splitting the cost based on the chosen preference
4.  Keep track of users properties and investments, should they add their values
5.  Keep track of users income
6.  Set up financial goals

The is to have a software provided through several stores for free and that will be available for following platforms:

- Windows
- Android
- MacOS?
- iOS?
- Web?

## Technologies

- [.NET](https://dotnet.microsoft.com/en-us/) for microservices
- [MAUI](https://dotnet.microsoft.com/en-us/apps/maui) for desktop layer
- [MimeKit](https://mimekit.net/) as a mail client
- [MSSQL](https://www.mssql.cz/) as a database layer
- [XUnit](https://xunit.net/) for C# unit testing
- [NSubstitute](https://nsubstitute.github.io/) for unit test mockin purposes

## Getting Started

- First you will need account made for you for JIRA and you will also need access to DB in case you will be handeling it as well.
- To be invited to JIRA, contant one of the app administrator -> <a href="#contributing">Contributing</a>
- Most of what we worked with is managed by microsoft so I strongly recomend using [Visual Studio IDE](https://visualstudio.microsoft.com/) as it offers great support, though ofcourse, any IDE will work just fine. This documentation is altought written with VS in mind.
- You need to clone the project from github with `git clone`
- depending on which part of the project you will be working on, you will need to install several things:
  <strong>Visual Studio</strong>

  1.  <strong>Web API development</strong> - You will need at least VS version 17.8 or higher as we user .NET 8.0 - Essentially all you have to do is run Visual Studio Installer - Check ASP.NET and web development tile and install whatever is set by default.

      ![web api tab](assets/images/web-api-development.png)

  You will also need a secret in order to be able to work with our DB provider, in order to do so, you will have to add secrets in each of the projects. For further details, contact me or setup your own db.

  2.  <strong>Desktop/Wep app development</strong> (iOS, Android, MacOS)

      - For Devs that wants to work on support for mobile or MaxOS version
      - Similarly to above but check the .NET Multiplatform App UI Development

        ![multiplatform app tab](assets/images/multiplatform-tab.png)

## Roadmap
- [ ] Add .NET Web API with Microservices architecture
- [ ] Add Frontend
  - [ ] Desktop version of application
    - [ ] Windows
    - [ ] MacOS
  - [ ] Mobile version of application
    - [ ] Android
    - [ ] iOS
  - [ ] Add Web version of application
  - [ ] Multilanguage support
    - [ ] Czech
    - [ ] English
    - [ ] Portuguese
    - [ ] Korean
  - [ ] Calendar Support
    - [ ] Gmail
    - [ ] Outlook
  - [ ] User Role support
    - [ ] Admins can add categories as they wish
