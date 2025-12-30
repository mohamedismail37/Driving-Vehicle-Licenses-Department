<div align="center">
<a href="https://github.com/mohamedismail37/Driving-Vehicle-Licenses-Department" rel="Title">
  
![DVLD-Title](assets/Title.png)
</a>
</div>

<h3 align="center">Driving Vehicle Licenses Department</h3>

### About The Project
> **DVLD** Developed a **C# .NET** Desktop application simulating real-world driver’s license issuance workflows

---
## Table of Contents

- [Project Description](#project-description)
  - [Overview](#overview)
  - [Objectives](#objectives)
  - [Built With](#built-with)
- [Main Features](#main-features)
- [Screenshots / UI Preview](#-screenshots--ui-preview)
- [Architecture & Database Design](#-architecture--database-design)
- [File Structure](#-file-structure)
- [Future Enhancements](#-future-enhancements)

---
## Project Description

### Overview
The Driving & Vehicle Licensing Department (DVLD) is a full-stack desktop application developed using C# and .NET Framework.  
The system simulates core workflows of a real-world licensing department, including people management, user management, applications, tests, etc.  
It is built with a clear focus on clean architecture, data validation, and maintainable code structure.

### Objectives
- Apply the **3-Tier Architecture** (Presentation, Business Logic, Data Access)
- Build a real-world–like system with **complete CRUD operations**
- Practice **WinForms UI development** with proper validation and user experience
- Design a **normalized SQL Server database** with relationships and constraints

### Built With
- **C# (.NET Framework)**
- **WinForms** 
- **ADO.NET**
- **SQL Server**
- **3-Tier Architecture**
- **Git & GitHub**

---
## Main Features

### Authentication & Account Management
- Secure login system with username & password validation
- Active user check before allowing access
- “Remember Me” functionality
- Change password with validation
- View current user information (without sensitive data)
- Logout functionality

| Login | Main Screen with account setting menu |
|-------|---------------------------------------|
| ![](assets/Login.png) | ![](assets/Users%20Management/MainAccountSettingsMenu.png) | 

---
###  People Management
- View & List all people in a DataGridView
- Advanced filtering (Person ID, Name, Email, etc.)
- Input validation (letters-only for names, numbers-only for IDs)
- Context menu actions:
  - View person details
  - Update person information
  - Delete person (with safety checks)

| Manage People List | Context Menu |
|--------------------|--------------|
| ![](assets/People%20Management/ManagePeopleList.png) | ![](assets/People%20Management/ManagePeopleContextMenu.png) |

| All Filters | Filter applied |
|-------------|----------------|
| ![](assets/People%20Management/ManagePeopleFilters.png) | ![](assets/People%20Management/ManagePeopleFilter.png) |

| Show Person Card | Update Person Info |
|------------------|--------------------|
| ![](assets/People%20Management/PersonDetails.png) | ![](assets/People%20Management/UpdatePerson.png) |

#### Add New Person:
| Validation for entities | Live validatoin with DB | 
|-------------------------|-------------------------|
| ![](assets/People%20Management/AddNewPersonValidation.png) | ![](assets/People%20Management/LiveValidation.png) |

---
### Users Management
- View all system users with filtering options
- Filter by User ID, Person ID, or Active status
- Add new users by linking them to existing people
- Prevent duplicate usernames (live database validation)
- Update user information
- Change user passwords
- Delete users only if not linked to critical data

#### Add New User:
| Find Person with filters | Fill User Info | Save Data |
|--------------------------|----------------|-----------|
| ![](assets/Users%20Management/AddNewUser4.png) | ![](assets/Users%20Management/AddNewUser2.png) | ![](assets/Users%20Management/AddNewUser3.png) 

#### Manage Users:
| Manage Users with Filters | Context Menu | 
|---------------------------|--------------|
| ![](assets/Users%20Management/ManageUsersFilter1.png) | ![](assets/Users%20Management/ManageUsersContextMenu.png) |

#### Update User:
| Show person Info | Update User Info | 
|------------------|------------------|
| ![](assets/Users%20Management/UpdateUser1.png) | ![](assets/Users%20Management/UpdateUser2.png) |

#### Change Password:
| Validation on previous password | Validation for new password | 
|---------------------------------|-----------------------------|
| ![](assets/Users%20Management/ChangePassword1.png) | ![](assets/Users%20Management/ChangePassword2.png) |

#### Show User Card:
![Show User Card](assets/Users%20Management/UserCard.png)

---

### 🗂 Applications & Test Types
- List and manage application types
- Update application settings
- List and update test types

#### Manage Test Types:
| List all Test Types | Update Test Type | 
|---------------------|--------------------|
| ![](assets/Test%20Types/ManageTestTypes.png) | ![](assets/Test%20Types/UpdateTestType.png) |

#### Manage Application Types:
| Application Menu in Dashboard | List Application Types | 
|-------------------------------|------------------------|
| ![](assets/Applications/MainApplicationsMenu.png) | ![](assets/Applications/Application%20Types/ManageApplicationTypes.png) |

| Context Menu | Update Application Type | 
|--------------|-------------------------|
| ![](assets/Applications/Application%20Types/ManageApplicationTypesContextMenu.png) | ![](assets/Applications/Application%20Types/UpdateApplicationType.png) |


---
## Architecture

The project follows the **3-Tier Architecture**:

1. **Presentation Layer (UI):**
   - Built with Windows Forms.
   - Handles all user interactions, such as buttons, forms, validation on the UI tier and DataGridViews.

2. **Business Logic Layer (BLL):**
   - Contains the core application logic and rules.
   - Validates data and ensures system consistency.
   - Acts as a bridge between the UI and the Data Access Layer.

3. **Data Access Layer (DAL):**
   - Uses ADO.NET to interact with SQL Server.
   - Responsible for all CRUD operations (Create, Read, Update, Delete).

---

## Technical Highlights

- 3-Tier Architecture (UI, BLL, DAL)
- Full CRUD Operations (Create, Read, Update, Delete)
- Data Validation and Error Handling
- Live Database Validation for Unique Fields
- Role-Based Access Control
- Image Handling and Storage
- Dynamic Search and Filtering
- Structured and Maintainable Codebase

---

## Technologies Used

- **Language:** C# (.NET Framework, WinForms)
- **Database:** Microsoft SQL Server
- **Architecture:** 3-Tier (Presentation, Business Logic, Data Access)
- **Libraries:** ADO.NET, System.Data, System.IO

---

## Screenshots

You can place all screenshots inside a `/assets` folder:

