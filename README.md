# BlazorDynamicList sample

> Note: the code for this example uses Blazor 3.0.100-preview6-012264

This demo application shows how Blazor's component model permits 
us to easily encapsulate code, UI and behaviours in reusable modules, 
and even load components dynamically in code.

A demo is hosted at https://blazordynamiclist.azurewebsites.net 

## Background

I currently have a large ASP.NET web application which has services
for a wide range of products and services it must support.

Each product has its own settings, properties and behavior. Although I can encapsulate the product behaviours in 
class libraries, it's been very hard to create UI in these class 
libraries as neither ASP.NET or JavaScript really lends themselves to this.

This results in a lot of supplier-specific and product-specific UI in 
the ASP.NET project, resulting in a monolithic web app that is 
only losely bound to the product code.

### Blazor

When Blazor came onto the scene in 2017, I was excited for two reasons. 

First, the ability to use C# in the client meant we no longer had to 
re-write the same C# code in JavaScript to get front-end behaviours. The 
server and the client can shared the models.

Secondly, and in my opinion more importantly, Blazor's excellent component 
model permits us to encapsulate the UI in these libraries as well.

### Proof of Concept

So I decided to write this project as a proof-of-concept. Could I encapsulate
behaviours and UI in libraries, and then handle generic lists of objects 
and display the correct UI for each one?

## Overview

The business case is a product list, where each product has some common 
features, e.g. `Name` and `Price` but also specific properties for each type.

**Product1** has a `HasFlange` property and **Product2** has a `Grommets` 
property. We want to have a custom view that shows each product according to 
its type and is contained in the same library that defines the ProductX class.

`Component1` displays a Product1 and `Component2` displays a Product2. 
In a further wrinkle I decided that any Product1 that has `HasFlange` true 
should use the `Component1b` component.

## Creation

The project uses the Blazor (ASP.NET Core hosted) template as a starting point.

### Product Libraries


I created a .NET Standard library **BaseClasses** 
to hold a common `ProductBase` abstract base class, which has `ID`, `Name`, 
`Price` and `Image` properties. It also defines an abstract method `GetViewComponent()` that returns the 
type of the Razor Component we want to use to view the product.

I then added new Blazor library projects **Library1** and **Library2** using 
the console command `dotnet new blazorlib -o Library1` etc. These represent 
our two product libraries. `Product1` and `Product2` inherit from `ProductBase`.

I also created a **RepositoryLib** .NET Standard library which represents 
our 'datasource' (which in a real-world example would be a database of 
some kind). The `ProductRepository` class just generates random products 
of either type using the `GetProducts()` method.

### Index

The `Index.razor` page on the client I used to test the basic component 
binding. `Component1` and `Component2` are placed in the HTML with binding 
to page properties `p1` and `p2` respectively. 

These are initially `null` but are populated using the button. You should 
see the products render when the button is clicked.

![Index.razor](https://github.com/conficient/BlazorDynamicList/blob/master/img/index.png)


### FetchData

I modified the `FetchData.razor` page to use a list of `ProductBase`. The
initalization code calls the WebAPI method on the server to fetch a random 
list of products.

#### Deserialization Issue

Although the FetchData list is `ProductBase`, we cannot use this code:
```cs
products = await Http.GetJsonAsync<ProductBase>("api/SampleData/Products?count=6");
```
The deserializer cannot create instances of the abstract class `ProductBase`, and 
anyway we want the specific `Product1` and `Product2` instances. The solution
is to use the [NewtonsoftTypeNameHandling](https://www.newtonsoft.com/json/help/html/SerializeTypeNameHandling.htm) 
option on the server API to generate JSON with embedded types, and then 
deserialize this on the client in the same way. I created a simple class
`TypedSerializer` in my **BaseClasses** library to achieve this. 

The FetchData page can then deserialize as follows:
```cs
    protected override async Task OnInitAsync()
    {
        // get sample data as JSON string
        var json = await Http.GetStringAsync("api/SampleData/Products?count=6");
        // deserialize the list using a typed-deserializer
        var data = BaseClasses.TypedSerializer.Deserialize(json);
        // set value
        products = (ProductBase[])data;
    }
```

### List Display

The page shows the list of products in three different ways.
![FetchData.razor](https://github.com/conficient/BlazorDynamicList/blob/master/img/fetchdata.png)


The first just shows a list of `ProductBase` values - only the common 
properties can be used when we do this.

#### If-then-else
The second list shows a manual `if-then-else` type of binding where we check 
the product type and show a bound component manually selecting either 
`<Component1>` or `<Component2>`.
```cs
@foreach (var product in products)
    {
        @* here we manually bind - simple with two, but quickly becomes untenable with say a hundred product types! *@
        @if (product is Library1.Product1 p1)
        {
            <Library1.Component1 Product=@p1></Library1.Component1>
        }
        @if (product is Library2.Product2 p2)
        {
            <Library2.Component2 Product=@p2></Library2.Component2>
        }
    }
```
While this works for two products, if we had hundreds or even thousands
of products this becomes a nightmare. Worse, it's putting product specific
logic into the web application and making maintaining it much harder.

#### Dynamic Component

The third list is the cool one! We display each product using a `DynamicComponent`:
```cs
 @foreach (var product in products)
    {
        <DynamicComponent Product=@product />
    }
```
This dynamic component selects and binds the correct component for each product. 
Also notice it uses the correct `<Component1b>` if the `HasFlange` property is set.

The code for this class is in the root of the Blazor client (although really it 
should be in the BaseClasses library). This is a manually coded Razor Component
that determines which component to use by calling the `GetComponentType()` method.

It then manually builds the render tree, binding the property thus:
```cs
 protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        Type componentType = Product.GetViewComponent();
        builder.OpenComponent(0, componentType);
        builder.AddAttribute(1, "Product", Product);
        builder.CloseComponent();
    }
```
I figured out the syntax by simply looking at the generated C# code for the `Index.razor`
page in the file `BlazorDynamicList.Client\obj\Debug\netstandard2.0\Razor\Pages\Index.razor.g.s`

### Other interesting points

You may notice both product libraries come with `styles.css` and 
`background.png` files. These are loaded by Blazor for us as virtual
libraries e.g. `/_content/Library1/styles.css`


### Acknowledgements

Thanks to all those fellow Blazorians on [Gitter](https://gitter.im/aspnet/Blazor) and
especially to [Chris Sainty](https://chrissainty.com) for his excellent Blog articles on 
how to use Github and Azure pipelines to publish the site.
