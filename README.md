## Oak

Oak is a series of dotnet libraries I pulled out from personal projects and organized into neat packages to be used elsewhere. The libraries are mostly focused around ASP.NET web projects and dependency injection. Feel free to use them in your own projects, contribute or ask questions.

### Oak.Events

Event library for building apps with event-driven architecture. It allows for event handlers to be incorporated into the standard dependency injection pipeline, be created dynamically, and executed either synchronously or asynchronously.

View the project and more details over in [`src/Oak.Events/`](https://github.com/weavc/Oak/tree/master/src/Oak.Events) or the working example in [`examples/ContactMe`](https://github.com/weavc/Oak/tree/master/examples/ContactMe).

### Oak.Email

### Oak.Webhooks

### Oak.TaskScheduler

Services and helpers for building a simple scheduled tasks. The library is built with concurrency & dependency injection in mind, it will track and run tasks based on the implementation of `IOccurrence`