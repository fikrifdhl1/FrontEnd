My application is a simple e-commerce platform that is now only available to administrators. The administrator has the ability to create carts, change products, and more.

The application using JWT tokens for authentication in order to provide secure access, and I've put in place middleware to handle error globally.

The application's architecture following the SOLID principles:
1.	The Single Responsibility Principle (SRP) states that each application class or module is responsible for a single, distinct task. For example in my case, adding products or changing quantities are exclusively the responsibility of the CartService.
2.	Open/Closed Principle (OCP): I can add new features (like additional product kinds) to the code without having to change it since it is written in such a way. For instance, I can add additional product categories to the product system's functionality without altering the main product class.
3.	I ensure that the interfaces are tailored to the requirements of the classes in accordance with the Interface Segregation Principle (ISP). The IProductRepository interface, for instance, solely addresses product activities, saving the service classes from having to deal with pointless methods they never use.
4.	The Dependency Inversion Principle (DIP) states that abstractions, not specific implementations, are what higher-level modules rely on. Dependency injection, for example, allows swapping out or mocking of dependencies for services such as IAuthService and IProductRepository in the application.

![Login](https://github.com/user-attachments/assets/6c10d79b-4c29-440b-a5ca-a31c0dc94a6d)
