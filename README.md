# Modulix

[![CI](https://github.com/PampaLabs/Modulix/actions/workflows/ci.yml/badge.svg)](https://github.com/PampaLabs/Modulix/actions/workflows/ci.yml)
[![Downloads](https://img.shields.io/nuget/dt/Modulix)](https://www.nuget.org/stats/packages/Modulix?groupby=Version)
[![NuGet](https://img.shields.io/nuget/v/Modulix)](https://www.nuget.org/packages/Modulix/)

Modulix is a dependency injection extension built on top of .NET abstractions for creating isolated, composable service modules. Each module can define its own services, allowing you to organize your application into self-contained, independent modules.

## Installation

To install Modulix, use the following command in your project directory:

```bash
dotnet add package Modulix
```

## Usage

### Defining a Module

Use the extension method `AddModule` to register a module.

```dotnet
services.AddModule(builder =>
{
    builder
        .ConfigureServices(services =>
        {
            services.AddSingleton<MyService>();
        })
        .Imports(imports =>
        {
            imports.AddBinding<OtherService>();
        })
        .Exports(exports =>
        {
            exports.AddBinding<MyService>();
        });
});
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.
