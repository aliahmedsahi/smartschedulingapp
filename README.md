# Smart Scheduling App for Healthcare Organizations
Allow me to walk you through my ABP.io application the technical assement. The application is centered around a smart-scheduling software that fulfills several essential features. Throughout the development process, I used various principles of object-oriented programming (OOP), .NET core principles, and Domain-Driven Design (DDD).

The main features are amdin can login and naviagte to User menu to add user as Doctors, Staff and Mananger. By overriding the ABP.io default user creation module I am creating a user and also saving the person information in the third tab. After that admin can assign schedule to for the doctore and staff and it will automatically create a notication for staff and doctor. Both staff and doctor can login and see the dashboard for details. 

## Application of Inheritance
The application wisely appropriates inheritance, where the Person class serves as the super or "parent" class for three subordinate or "child" classes: Doctor, Staff, and Manager. The child classes inherit the characteristics of the Person class and incorporate additional attributes to align with their specific roles.

### Entity & Aggregate Roots:
Particular classes like Person, Schedule, Notification inherit from Entity<Guid> or FullAuditedAggregateRoot<Guid>, the base classes provided by the framework that encompass an Id property for the unique identification of instances.

### App Services & Domain Services:
To handle domain logic, classes PersonManager, DoctorManager, and StaffManager inherit from DomainService. On the same note, for service layer functionalities, IPersonAppService, IStaffAppService, and IDoctorAppService inherit from IApplicationService.

### Repositories:
The class EfCorePersonRepository inherits from EfCoreRepository<SmartSchedulingAppDbContext, Person, Guid>. This allows the reuse of methods for database operations and alignment with the IPersonRepository interface for custom repository methods.

## Leveraging Polymorphism
Abstract Person class and its derived classes (Doctor, Staff, Manager):
The Person class acts as an abstract base and has three derived classes - Doctor, Staff, and Manager. Each of these derived classes extends the Person base class with their unique data, providing a typical case of polymorphism. For instance, the Doctor class includes a property public string Specialization { get; set; }, not included in the base Person class, as it is unique to doctors only.

### Method Polymorphism:
Polymorphism is evident in the CreateAsync method present in the PersonManager, DoctorManager and StaffManager classes. While the function names remain the same, their behavior varies based on the parameters and the concrete class implementation.

### Polymorphism in DomainService:
DomainService class is extended by PersonManager, DoctorManager, and StaffManager. By inheriting from the DomainService the classes can utilize its methods and properties, essentially demonstrating polymorphism.

### Utilizing Discriminator in Entity Framework Core:
Another scenario where polymorphism comes into play is in the Entity Framework Core's polymorphic queries and relationships. In my application, a .HasDiscriminator<string>("PersonType") is included in the DbContext configuration. This method is crucial in determining the type of Person (Doctor, Staff, Manager).

The discriminator column in Entity Framework Core implements polymorphism by allowing EF Core to recognize the specific subclass of Person it should instantiate when retrieving the data from the database. Hence, different Persons can be stored within one table, and EF Core will correctly handle the creation of instances of the appropriate type.

## Overloading
### Constructor Overloading:
In the Person, Doctor, Staff, and Manager classes, constructor overloading is implemented with varying parameters for each class, providing varying initiations per each specific role.

### Method Overloading of AddSchedule:
Although not currently present in the code, method overloading for AddSchedule could potentially be applied. The method could either accept a Timeslot object or separate start and end times, thereby offering more flexibility.

## Generics
### Class Definitions:
The use of generics is apparent in classes like Person, Schedule, Notification, which inherit from base classes using a generic type Guid for the data type of the primary key.

Collections:
Generic collections such as List<T> and IReadOnlyCollection<T> are applied in the Person class with _schedules and _notifications fields.

## Dependency Injection

### Repository Injection in Application Services:
Dependency Injection has been handled in application services like DoctorAppService and StaffAppService through the IPersonRepository instances.

## Domain Service vs Application Service
### Domain Services:
Domain services such as PersonManager, DoctorManager, StaffManager are utilized for handling complex business logic, as seen in the operation StaffManager.AddSchedule().

### Application Services:
Application services like IPersonAppService, IDoctorAppService, IStaffAppService handles operations involving task coordination, security, user interactions, and more application-specific tasks rather than business logic.

## Aggregates, Entities and Value Objects
### Entity:
Entities such as Person,Schedule, Notification, Doctor, Manager, Staff are objects each having a unique identifier (Id).

### Value Object:
The Timeslot class in my code is a value object, being defined only by its StartTime and EndTime.

### Aggregate Root:
Person class is the aggregate root for Schedule and Notification, forming a unit, managing these associated objects for changes and consistency.

### Repository Pattern:
Repository patterns have been implemented in my code via the IPersonRepository and EfCorePersonRepository classes, abstracting the complexities of data access.

## Modularity
In the context of the project, each module can be thought of as a team in a big office. Each module, whether it be Identity, Security, Scheduling, or Notifications, is responsible for a specific task within the software.

Sometimes these modules need to share the same information. To simplify this, we create a "Shared Domain Project", like Domain.Shared project, a common space for all modules to get or share the information they need.

While these modules help us to work more efficiently and organize our work better, managing many modules can sometimes cause confusion, with dependencies making things complex.

## Questions to Validate Modularity

1. Do you want to run these features independently from other modules?
2. Do you plan on selling these features separately to clients?
3. Are you planning to assign a dedicated team to these features?
4. Is there a possibility of reusing these features in other applications?
5. Are you considering a dedicated database for this feature set?
6. Is this feature set meant to be handled by a dedicated team, separate from the other features?

## Other Questions
1. What tools and technologies are involved in the project (Backend, frontend, database etc), and the reasoning behind their choices?
2. Who is the target market for this project?
3. Is there a cadence for new feature requests, and are any features currently in the pipeline?
4. How much of the project has already been completed and what's remaining?
5. What is the coverage of the application? Is it only Web-based, or are there Desktop and Mobile versions available too?
6. Is there room for modifications in the existing application features, keeping in mind the open-closed principle?


