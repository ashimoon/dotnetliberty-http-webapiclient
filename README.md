# Description

A simple client for interacting with a RESTful APIs using strongly typed models on .NET Core.

Supports synchronous and asynchronous `GET`/`POST`/`PUT`/`DELETE`.

# Usage

## Setup
Declare a model:
```csharp
public class Thing
{
    public int Id { get; set; }
    public string SomeProperty { get; set; }
}
```

Create client:
```csharp
// Second type parameter is the ID type
var client = new WebApiClient<Thing, int>(
    new Uri("http://localhost:5000/api/things/"));
```

## GET /api/things/

```csharp
// Async
IEnumerable<Thing> things = await _client.GetAsync();
// Sync
IEnumerable<Thing> things = _client.Get();
```

## GET /api/things/{id}

```csharp
var id = 1;
// Async
Thing thing = await _client.GetAsync(id);
// Sync
Thing thing = _client.Get(id);
```

## POST /api/things/

```csharp
// Async
Thing created = await _client.PostAsync(new Thing
{
    Id = 2,
    SomeProperty = "Some value"
});

// Sync
Thing created = _client.Post(new Thing
{
    Id = 2,
    SomeProperty = "Some value"
});
```

## PUT /api/things/{id}

```csharp
var id = 3;
// Async
Thing updated = await _client.PutAsync(id, new Thing
{
    Id = id,
    SomeProperty = "Some value"
});

// Sync
Thing updated = _client.Put(id, new Thing
{
    Id = id,
    SomeProperty = "Some value"
});
```

## DELETE /api/things/{id}

```csharp
var id = 4;
// Async
await _client.DeleteAsync(id);

// Sync
_client.Delete(id);
```


# More Samples

Take a look at the `Samples/` folder.