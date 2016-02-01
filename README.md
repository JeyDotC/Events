# Events

This is a small library which purpose is to give a very simple implementation of the observer pattern.

## Rationale

This library was created for the need of a simple way to say "hay, this happened" without worrying about "who is listening" or "how is this executing", just 

```csharp
emitter.Emit(myEvent);
```

And let the gossip classes do their job. 

That's how I came to the idea that our dispatcher (the EventEmitter class) needed a way to get rid of those affairs, then, the `IObserverStorage` and the `IObserversInvoker` came into play. One finds the neighbours and the other spreads the gossip.

## Usage

First, Define your event:

```csharp
// Just a Plain Old C# Class
public class HelloWorldEvent {
	public string Data {get; set; }
}
```

Second, create your observers:

```csharp
public class January : IEventObserver<HelloWorldEvent>{
	void On(HelloWorldEvent evnt){
		evnt.Data += "Traveling time with my fedora hat. ";
	}
}

public class February : IEventObserver<HelloWorldEvent>{
	void On(HelloWorldEvent evnt){
		evnt.Data += "Catching bullets with my bare hands. ";
	}
}
```

Third, create someone to find your observers:

```csharp
IObserverStorage observerStorage = new MemoryObserverStorage(
	new January(),
	new February()
	//, and the other...
);
```

Fourth, contract someone spread your message:

```csharp
IObserversInvoker invoker = new SequentialObserverInvoker(); //<-- This is the default guy.
```

And finally instantiate the emitter and emit the event:

```csharp
var emitter = new EventEmitter(observerStorage, invoker); // or just new EventEmitter(observerStorage);
var myEvent = new MyEvent{ Data = "Hello observers, what's your status? " };
emitter.Emit(myEvent);
```

Wrapping up, this is what you do:

```csharp
// Create the storage
IObserverStorage observerStorage = new MemoryObserverStorage(
	new January(),
	new February()
	//, and the other...
);

// Create the invoker
IObserversInvoker invoker = new SequentialObserverInvoker(); 

//Configure the emitter
var emitter = new EventEmitter(observerStorage, invoker); // or just new EventEmitter(observerStorage);

// Tell the world how cool are you
var myEvent = new MyEvent{ Data = "Hello observers, what's your status? " };

emitter.Emit(myEvent);
```

## But man, I still in need of instantiating all my observers!

Don't worry, there is an `AssemblyScanningOberverStorage` that goes inside your assemblies and finds all those gossip observers for you. Just make sure you instantiate that class only once.

```csharp
// Create the storage
IObserverStorage observerStorage = new AssemblyScanningOberverStorage(
	// optionally add assembly names: "My.Cool.Assembly", "My.Cool.Assembly2" ...
);

// Create the invoker
IObserversInvoker invoker = new SequentialObserverInvoker(); 

//Configure the emitter
var emitter = new EventEmitter(observerStorage, invoker); // or just new EventEmitter(observerStorage);

// Tell the world how cool are you
var myEvent = new MyEvent{ Data = "Hello observers, what's your status? " };

emitter.Emit(myEvent);
```

## Hey, my observers are kind of... fat

Then all you need is another invoker, the `ParallelsObserverInvoker` which calls your observers with the Parallels static class.


```csharp
// Create the invoker
IObserversInvoker invoker = new ParallelsObserverInvoker(new ParallelOptions
	{
		MaxDegreeOfParallelism = 4
	});

//Configure the emitter
var emitter = new EventEmitter(observerStorage, invoker);

// Tell the world how cool are you
var myEvent = new MyEvent{ Data = "Hello observers, what's your status? " };

emitter.Emit(myEvent);
```

> In the future I'm planning to create a HangFire invoker for really heavy observers.

## My boss says "cache al the things!"

To satisfy your boss, we got an adapter class: `CachedObserverStorage` which caches the results of getting the observers for each event:

```csharp
// Create the storage
IObserverStorage heavyStorage = new AssemblyScanningOberverStorage(
	// optionally add assembly names: "My.Cool.Assembly", "My.Cool.Assembly2" ...
);
// create your cache
ObjectCache cache = new MemoryCache("MyObservers");

IObserverStorage cachedStorage = new CachedObserverStorage(cache, heavyStorage);

//Configure the emitter
var emitter = new EventEmitter(cachedStorage);

// Tell the world how cool are you
var myEvent = new MyEvent{ Data = "Hello observers, what's your status? " };

emitter.Emit(myEvent);
```

## Get creative

This library lets you be creative in how to implement the aspects of the emitter, as it depends on a couple of interfaces, for example, you could create an observer storage that finds its observers from a Database or from CsScript files. Or an observer invoker that decides if an observer is to be executed now or scheduled for later (Quartz, hangfire, NServicebus)?

## Next things I'll do

* ~~Publish to nuget.~~
* HangFire invoker (I need it for my job, that's why it is first).
* Some handy extension methods. (myStorage.Cached(); to name one).
* Ninject integration.
