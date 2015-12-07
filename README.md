# Description

A simple client for interacting with a RESTful APIs (including Web API) using strongly typed models on .NET and .NET Core.

Supports synchronous and asynchronous `GET`/`POST`/`PUT`/`DELETE`.

## Website

.NET Liberty - http://dotnetliberty.com

# Usage

## Setup
Extract an interface from your Web API Controller. I like to put this in a shared contract library (along with data transfer object models).
```csharp
// DataContract and DataMember are optional
[DataContract]
public class Thing
{
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string SomeProperty { get; set; }
}

// This is the same interface on the Web API Controller
public interface IThingsApi
{
    void Delete(int id);
    IEnumerable<Thing> Get();
    Thing Get(int id);
    Thing Post(Thing value);
    Thing Put(int id, Thing value);
}
```

Extend `WebApiClient`:
```csharp
public class ThingsApiClient : WebApiClient<Thing, int>, IThingsApi
{
    public ThingsApiClient(Uri uri)
        : base(uri)
    { }
}
```

Use:
```csharp
// Only Synchronous
IThingsApi api = new ThingsApiClient(
    new Uri("http://localhost:5000/api/things/"));

// Async + Synchronous
HttpClient client = new ThingsApiClient(
    new Uri("http://localhost:5000/api/things/"));
```

## GET /api/things/

### Synchronous
```csharp
IEnumerable<Thing> things = api.Get();
// or
IEnumerable<Thing> things = api.GetMany();
```
### Async
```csharp
IEnumerable<Thing> things = await client.GetAsync();
// or
IEnumerable<Thing> things = await client.GetManyAsync();
```

## GET /api/things/{id}

### Synchronous
```csharp
Thing thing = api.Get(1);
// or 
Thing thing = api.GetSingle(1);
```
### Async
```csharp
Thing thing = await client.GetAsync(1);
// or
Thing thing = await client.GetSingleAsync(1);
```

## POST /api/things/

### Synchronous
```csharp
Thing created = api.Post(new Thing
{
    Id = 2,
    SomeProperty = "Some value"
});
```
### Async
```csharp
Thing created = await client.PostAsync(new Thing
{
    Id = 2,
    SomeProperty = "Some value"
});
```

## PUT /api/things/{id}

### Synchronous
```csharp
var id = 3;
Thing updated = api.Put(id, new Thing
{
    Id = id,
    SomeProperty = "Some value"
});
```

### Async
```csharp
var id = 3;
Thing updated = await client.PutAsync(id, new Thing
{
    Id = id,
    SomeProperty = "Some value"
});
```

## DELETE /api/things/{id}

### Synchronous
```csharp
api.Delete(4);
```

### Async
```csharp
await client.DeleteAsync(4);
```


# More Samples

Take a look at the `Samples/` folder.