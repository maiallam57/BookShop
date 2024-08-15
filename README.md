
**1. Explain two types of deployment for .NET Core applications.**
   - **Framework-Dependent Deployment (FDD):** The application relies on the presence of the .NET Core runtime on the target system. Only the application files are deployed, resulting in a smaller deployment size.
   - **Self-Contained Deployment (SCD):** The application is deployed with its own version of the .NET Core runtime. This eliminates dependency on the target system's runtime, making the application larger but more portable.

**2. Explain what is included in .NET Core.**
   - **CoreCLR:** The runtime that executes applications and handles garbage collection, JIT compilation, and more.
   - **CoreFX:** The framework libraries that provide common functions like collections, file I/O, and networking.
   - **SDK & CLI:** Tools for developing, building, testing, and publishing .NET Core applications.

**3. Is there a way to catch multiple exceptions at once and without code duplication?**
   - Yes, you can catch multiple exceptions using a single `catch` block by specifying multiple exception types in a comma-separated list (C# 6.0 and later), or by using pattern matching in C# 7.0 and later.

**4. What is Kestrel?**
   - Kestrel is a cross-platform web server for ASP.NET Core. It is the default web server included with ASP.NET Core projects, known for its high performance and efficiency.

**5. What is the difference between .NET Core and .NET Framework?**
   - **.NET Core:** Cross-platform, modern, lightweight, and modular, suited for cloud, microservices, and container-based deployments.
   - **.NET Framework:** Windows-only, mature, and full-featured, with broad library support and legacy system compatibility.

**6. What's the difference between .NET Core, .NET Framework, and Xamarin?**
   - **.NET Core:** A cross-platform framework for building modern applications.
   - **.NET Framework:** A Windows-specific framework with comprehensive libraries for enterprise applications.
   - **Xamarin:** A framework for building cross-platform mobile applications using C# and .NET.

**7. What is the use of the IDisposable interface?**
   - `IDisposable` provides a mechanism for releasing unmanaged resources (e.g., file handles, database connections) through its `Dispose` method, typically used in a `using` statement.

**8. What does Common Language Specification (CLS) mean?**
   - CLS is a set of rules that define a subset of common features and behaviors across all .NET languages, ensuring interoperability among them.

**9. Explain the difference between Task and Thread in .NET?**
   - **Thread:** Represents a physical thread in the OS; it’s more resource-intensive and used for low-level threading.
   - **Task:** Represents an asynchronous operation and abstracts threading. It’s part of the Task Parallel Library, enabling more efficient and simpler concurrent programming.

**10. Explain the difference between Managed and Unmanaged code in .NET?**
   - **Managed code:** Runs under the control of the .NET runtime (CLR), benefiting from features like garbage collection and type safety.
   - **Unmanaged code:** Runs outside the CLR, directly executed by the OS, often involving resources not managed by the runtime, like pointers.

**11. What is CoreCLR?**
   - CoreCLR is the runtime for .NET Core, responsible for executing managed code, performing JIT compilation, and managing memory through garbage collection.

**12. What is a JIT compiler?**
   - The JIT (Just-In-Time) compiler translates Intermediate Language (IL) code into machine code at runtime, allowing .NET applications to run on different hardware architectures.

**13. What is the difference between .NET Standard and PCL (Portable Class Libraries)?**
   - **.NET Standard:** A formal specification of .NET APIs that all .NET implementations must support, offering greater compatibility and future-proofing.
   - **PCL:** A deprecated approach to targeting multiple .NET platforms with limited API surface area.

**14. What is Explicit Compilation?**
   - Explicit Compilation (AOT) refers to compiling code ahead of time into native code, rather than relying on JIT at runtime.

**15. What are the benefits of Explicit Compilation (AOT)?**
   - **Performance:** Faster startup time since code is precompiled.
   - **Reduced Memory Usage:** Lower runtime overhead.
   - **Platform Independence:** Ensures compatibility with environments where JIT compilation is not allowed or possible.

**16. What is the difference between Class Library (.NET Standard) and Class Library (.NET Core)?**
   - **.NET Standard:** Targets multiple .NET platforms (e.g., .NET Core, .NET Framework, Xamarin) and offers broad API compatibility.
   - **.NET Core:** Targets only .NET Core, allowing access to more APIs specific to .NET Core, but is less portable.

**17. What is BCL?**
   - The Base Class Library (BCL) is a set of core libraries that provide essential APIs for .NET applications, including collections, I/O, and basic data types.

**18. When should we use .NET Core and .NET Standard Class Library project types?**
   - **.NET Standard:** When building libraries intended to be used across different .NET implementations.
   - **.NET Core:** When building libraries specifically for .NET Core applications, where platform-specific features are required.

**19. What is FCL?**
   - The Framework Class Library (FCL) is a comprehensive collection of reusable classes, interfaces, and value types that provide a wide range of functionality for .NET applications.

**20. What about MVC in .NET Core?**
   - ASP.NET Core MVC is a web application framework that follows the Model-View-Controller pattern, enabling separation of concerns, testability, and flexible architecture for web applications.

**21. Explain the IoC (DI) Container service lifetimes?**
   - **Transient:** A new instance is created each time it is requested.
   - **Scoped:** A single instance is created per request and shared across the lifetime of the request.
   - **Singleton:** A single instance is created the first time it is requested and is shared across all requests.

**22. What is Unit Of Work?**
   - Unit Of Work is a design pattern that maintains a list of changes to be made (like inserts, updates, deletions) and coordinates the writing out of these changes into the database as a single transaction.

**23. Mention what is the difference between ADO.NET and classic ADO?**
   - **ADO.NET:** Designed for disconnected data access, using datasets and data readers, with strong integration in .NET.
   - **Classic ADO:** Older technology relying on a connected model, using recordsets.

**24. What is the difference between DataView, DataTable, and DataSet?**
   - **DataView:** Represents a customizable view of a DataTable.
   - **DataTable:** Represents an in-memory table of data.
   - **DataSet:** Represents an in-memory collection of DataTables.

**25. What are the differences between using SqlDataAdapter vs SqlDataReader for getting data from a DB?**
   - **SqlDataAdapter:** Fills a DataSet or DataTable and works in a disconnected mode, suitable for operations that require data manipulation after retrieval.
   - **SqlDataReader:** Provides a fast, forward-only, and connected method to read data, ideal for quick retrieval and processing.

**26. What is the difference between ExecuteScalar, ExecuteReader, and ExecuteNonQuery?**
   - **ExecuteScalar:** Retrieves a single value (e.g., the result of an aggregate function).
   - **ExecuteReader:** Returns a SqlDataReader to iterate over a result set.
   - **ExecuteNonQuery:** Executes a command that changes data (e.g., INSERT, UPDATE, DELETE) and returns the number of rows affected.

**27. What is the difference between Integrated Security = True and Integrated Security = SSPI?**
   - Both specify Windows Authentication in SQL Server. **SSPI** (Security Support Provider Interface) is the underlying technology, while **True** is a more readable alias.

**28. Could you explain to me some of the main differences between Connection-oriented access and Connectionless access in ADO.NET?**
   - **Connection-oriented:** Maintains a persistent connection to the database (e.g., SqlDataReader).
   - **Connectionless:** Fetches data into a disconnected object like a DataSet, and the connection is closed afterward (e.g., SqlDataAdapter).

**29. How could you control connection pooling behavior?**
   - Connection pooling can be controlled via connection string parameters like `Max Pool Size`, `Min Pool Size`, and `Pooling=True/False`, adjusting how connections are managed and reused.

**30. Is it necessary to manually close and dispose of SqlDataReader?**
   - Yes, it is recommended to close and dispose of SqlDataReader explicitly to free up the database connection and resources promptly.

**31. Could you explain some benefits of Repository Pattern?**
   - **Abstraction:** Hides data access details from business logic.
   - **Testability:** Facilitates unit testing by allowing mocking of data sources.
   - **Consistency:** Provides a consistent data access interface across different data sources.
