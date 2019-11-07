# Getting Started

The basic usage is very simple

    var storage = SimpleStorage.EditGroup("group name of key/value store");
    storage.Put("myKey", "some value");
    var value = storage.Get("myKey");

it's also possible to use complex types via automatic binary serialization:

    storage.Put<DateTime>("some timestamp", DateTime.Now);
    var value = storage.Get<DateTime>("some timestamp");

When checking for a specific value you can use HasKey("some key") or make use of the "returning null if not found" feature:

    var value = storage.Get("some key") ?? "default value if key is not present";

When using Value Types, you need to make them nullable 

    var value = storage.Get<TimeSpan?>("some key") ?? new TimeSpan.FromSeconds(10);

or pass the default value as second parameter:

    var value = storage.Get<TimeSpan>("some key", new TimeSpan.FromSeconds(10));

## Async/Await

We all love async/await and hence these are provided in corresponding method names: PutAsync, GetAsync, HasKeyAsync and DeleteAsync.

## How it Works

There are specialized implementations for Android and iOS which make use of the native Prefreneces APIs to store the values. Thanks to a static initialization and the impleStorage.EditGroup creator delegate you can use the above code also in files shared on both platforms. 

## Android Setup

On Android, we need the App context to use the shared preferences. Before you use SimpleStorage anywhere in your App, make sure you set the context with SimpleStorage.SetContext(). For example:

    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);
        
        SimpleStorage.SetContext(ApplicationContext);
        
        SetContentView(Resource.Layout.Main);
         
        // other code
    }