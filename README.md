# Introduction
"Extended IPO" is a way of structuring CRUD-style applications to align user flow. It promotes a shallow call stack and understandable scoping. It builds on common practices such as [CQRS](https://www.martinfowler.com/bliki/CQRS.html) and [MediatR](https://github.com/jbogard/MediatR), but identifies there is an extra step between Query and Command. There are rigid definitions in Extended IPO so that we can avoid asking philosophical questions of our code and focus on concrete, easy-to-follow structures.

# Understanding the mental model
## Scoping
Extended IPO is scoped to user-flow-sized chunks. This means it covers everything from the entry point, to returning data to the user, but not in a fine-grained way. Using Extended IPO as a cascading pattern is generally not recommended. Instead, Extended IPO groups code into cohesive buckets. You can still use external libraries if you need to centralize portions of work, but Extended IPO goes against horizontally-layered architecture by saying that each user flow needs to maintain its own layers and business logic.

## IPO
Input, Process, Output is still the basis for any function in any language. The "Extended" portion of Extended IPO lies in how we define Input, Process, and Output.

### Definitions:
#### Input
> "Any information that lives outside of the function that the function requires to run."

This includes information from databases, caches, etc.

#### Process
> "Any operation which must be completed inside the function before the outside world can be changed." 

There are generally two types of processes in CRUD programs, validation and transformation. While not part of Extended IPO, it's recommended that you create one large Process() method for each type of work (validtion, transformation, etc) and then create private methods which the Process() calls for each business rule. Examples of this are covered in the tests section of the library.

#### Output
> "Any unit of work which is both expected to succeed and changes something in the outside universe."

There is no limit to what could be changed. Perhaps it's a record in a database or a notification sent to a customer. "Expected to work" signifies that you shouldn't return a status code. If something breaks, it's an exceptional case and should be handled by an exception.

### Rules gathered from these definitions
Because of how IPO is defined, functions are placed in a specific order. An example of what this looks like in an API lies below.

    1. User sends us data.
    2. We go and get extra data from the database
    3. We make sure that the user submitted data is valid and doesn't conflict with any business rules.
    4. If there are no error messages, transform the data based on any transformational business rules we might have, and submit it to the database.
    5. If there are error messages, return those to the user.

Now that we have an understanding in plain english, let's write it in sudo-C#.

```C#
IActionResult PerformUserFlow(userInput) 
{
    var allData = Query(userInput);
    var errors = Validate(allData);

    if (!errors.Any()) {
        var transformModel = Transform(allData);
        Submit(transformModel);
        return Ok();
    }
    else 
    {
        return BadRequest(errors);
    }
}
```

### Types of functions
Generalizing the code above, we end up with five different types of functions:
1. Entry Points: Where the user flow begins (PerformUserFlow(userInput)).
2. Queries: Where extra information is gathered from the universe (Query(userInput)).
3. Validators: Where data validation is performed (Validate(allData)).
4. Transformers: Where information is transformed into something usable (Transform(allData)).
5. Commands: Where data is saved to the database (Submit(transformModel)).

# FAQ

## Q: Why don't you put database queries inside validations in Extended IPO?
    A: When you intermingle queries with processes, it's really hard to never duplicate a database query. It's easier to grab all of the information you'll need exactly once. Often the reduction in duplication more than offsets the number of queries executed if the function exits early.

## Q: Why can't you return something from a Command?
    A: The validation should catch any case where we expect not to be able to be able to complete a user flow. Therefore if a command fails, we don't want to return a result code because that situation is a failure we don't expect. It's an exceptional case, which requires an exception. There is a situation where you need to return an id to a user after something has been created. You could get around this by finding that id within a Query at the top of the user flow and submitting it as part of the payload... But that sometimes feels awkward. I'm open to ideas for how to allow ids to be returned but not allowing result codes.

    Sidenote: if you *really* want to return a result code, look at [MediatR](https://github.com/jbogard/MediatR). MediatR is another way designate sepparate Queries / Commands, and promotes "Vertical Slice Architecture" which is also at play in Extended IPO. 

# Summary
Our goals as developers should not be to promote any framework or pattern, but to write intention-revealing code. Extended IPO is one way to write intention revealing code, but not the only way. Also, if you don't prize the "mundane" aspects of programming, such as naming your variables, no amount of new patterns or new language features will help increase your readability.

What we've just learned is enough to successfully implement Extended IPO. If you're really *hoping* for a package to put in your solution, read on. (But really you should just leave and go write good code without using a library for Extended IPO.)

# Library

The library associated with Extended IPO lines up seamlessly with the descriptions above. There are four interfaces and a class. Each designed to allow you to incorporate Extended IPO into your entry points for user flow.

## Interfaces:
- IQueryer is designed for a situation where you receive user input and relay back all the data that function will need to run. As such, it is designed to take in type T and return type U.
- IValidator is designed to take in either an IEnumerable\<T\> or one model of type T and return a List of ValidationErrorModels. If you don't want to pass back ValidationErrorModels, you are free to provide a type of U, which will pass back a List of type U. There is a default implementation when you take in a list which simply passes each model of type T to the function which validates a single model of type T. This should help reduce the amount of boilerplate you write without restricting you to a default implementation.
- ITransformer is designed to take in a model of type T and return a model of type U. Any business logic should reside within an ITransformer.
- ICommander is designed to take in a model of type T and perform an operation. There is nothing returned from an ICommander because any operation done is expected to succeed.

## Models:
- ValidationErrorModel contains properties which will be useful in writing error messages to a user. There are multiple designs which could be used, but I opted for a flat design so that implementing the validation function is easier. The default implementation for an IValidator returns a List of ValidationErrorModels.
 
## Installation 

This is in an **UNSTABLE** state. Please don't use the package in production yet as there may be huge changes coming. Currently you have to download the project and add it to your solution. Once a 1.0.0 release has been identified, this will be shared as part of a NuGet package.
