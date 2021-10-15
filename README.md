## Oak

Oak is a series of dotnet libraries I pulled out from personal projects and organized into neat packages to be used elsewhere. The libraries are mostly focused around ASP.NET web projects and dependency injection. Feel free to use them in your own projects, contribute or ask questions.

### Oak.Events

Event library for building apps with event-driven architecture. It allows for event handlers to be incorporated into the standard dependency injection pipeline, be created dynamically, and executed either synchronously or asynchronously.

There is a more complete example in the [`examples/ContactMe`](https://github.com/weavc/Oak/tree/master/examples/ContactMe) project, but to give a quick run down:

Define your event:
```c#
public class OnDatabaseModelUpdate : IEvent
{
    public OnDatabaseModelUpdate(object sender, DatabaseModel entity, Context context)
    {
        this.Sender = sender;
        this. Entity = entity;
        this.Context = context;
    }
    
    // this must be implemented as per the IEvent interface
    public object Sender { get; set; }
    
     // The following properties can be anything to suit the situation
    public Context context { get; set; }
    
    public DatabaseModel Entity { get; set; }
}
```

Create EventHandler(s):
```c#
public class OnDatabaseModelUpdateEventHandler : IAsyncEventHandler<OnDatabaseModelUpdate>
{
    private readonly SomeOptions _someOptions;
    private readonly ISomeService _someService;

    // inject options/services into your event handler
    public OnDatabaseModelUpdateEventHandler(IOptions<SomeOptions> someOptions, ISomeService someService)
    {
        this._someOptions = someOptions.Value;
        this._someService = someService;
    }

    // execute code!
    public async Task HandleEventAsync(OnDatabaseModelUpdate args)
    {
         this._someService.Execute(args, this._someOptions.Type);
    }
}
```

Register Services in `Startup.cs`:
```c#
public void ConfigureServices(IServiceCollection services)
{
      // Add our event dispatcher service
      services.AddOakEventDispatcher();
      
      // Types are the EventHandler and the IEvent implementation.
      // Multiple EventHandlers can be added on the same IEvent, they will all be 
      // called when the event is emitted.
      services.AddAsyncEvent<OnDatabaseModelUpdateEventHandler, OnDatabaseModelUpdate>();
      
      // Multiple EventHandlers can be added on the same IEvent, they will all be 
      // called when the event is emitted.
      services.AddAsyncEvent<SomeOtherEventHandler, OnDatabaseModelUpdate>();
      
      ...
}
```

Emit the event with `IEventDispatcher` service:
```c#
public class UpdateDatabaseModel
{
    private readonly IEventDispatcher _eventDispatcher;

    // inject IEventDispatcher into your service
    public UpdateDatabaseModel(IEventDispatcher eventDispatcher)
    {
        this._eventDispatcher = eventDispatcher;
    }
    
    public async Task Update(Context context, DatabaseModel model)
    {
        // update ...
        
        // finally emit your event!
        // This will trigger all registered event handlers and call their 
        // HandleEventAsync(OnDatabaseModelUpdate args) method
        await this._eventDispatcher.EmitAsync(new OnDatabaseModelUpdate(this, model, context));
    }
}
```
