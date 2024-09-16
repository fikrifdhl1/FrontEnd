My application is a simple e-commerce platform that is now only available to administrators. The administrator has the ability to create carts, change products, and more.

The application using JWT tokens for authentication in order to provide secure access, and I've put in place middleware to handle error globally.

The application's architecture following the SOLID principles:
1.	The Single Responsibility Principle (SRP) states that each application class or module is responsible for a single, distinct task. For example in my case, adding products or changing quantities are exclusively the responsibility of the CartService.
2.	Open/Closed Principle (OCP): I can add new features (like additional product kinds) to the code without having to change it since it is written in such a way. For instance, I can add additional product categories to the product system's functionality without altering the main product class.
3.	I ensure that the interfaces are tailored to the requirements of the classes in accordance with the Interface Segregation Principle (ISP). The IProductRepository interface, for instance, solely addresses product activities, saving the service classes from having to deal with pointless methods they never use.
4.	The Dependency Inversion Principle (DIP) states that abstractions, not specific implementations, are what higher-level modules rely on. Dependency injection, for example, allows swapping out or mocking of dependencies for services such as IAuthService and IProductRepository in the application.
Login
![Login](https://github.com/user-attachments/assets/6c10d79b-4c29-440b-a5ca-a31c0dc94a6d)
Product
![Product](https://github.com/user-attachments/assets/e5f23423-6476-4143-9ea0-d84b43762cab)
Create Product
![Create Product](https://github.com/user-attachments/assets/ef704ea2-9114-4915-9d7d-3eebf06e40bf)
Detail Product
![Detail Product](https://github.com/user-attachments/assets/9b86d55c-471a-46dd-9ef1-e967d7429bd9)
Edit Product
![Edit Product](https://github.com/user-attachments/assets/f6c762bc-40ea-4c99-9743-ea40aa0f6e94)
Cart
![Cart](https://github.com/user-attachments/assets/595f03e7-1f23-49ea-9928-fd170befa6ea)
Cart Detail Active
![Cart Detail Active](https://github.com/user-attachments/assets/6f2d58ad-86c3-4bbf-be48-fe4efc9a5cb4)
Cart Detail Checkout
![Cart Detail CheckedOut](https://github.com/user-attachments/assets/ee357c3f-d9ef-4579-9839-d3a1ac384a72)
Creating Cart
![Create Cart](https://github.com/user-attachments/assets/ffffc547-65fe-46c4-a2e1-fc5407edb03b)
Transaction History
![Transaction History](https://github.com/user-attachments/assets/da44f9cf-0fe4-41f6-937e-26d69cc3fadc)
