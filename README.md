# FX Exchange Console Application

## Overview

The FX Exchange Console Application is a simple tool for converting currency amounts based on current exchange rates. 
The application is built using clean code principles to ensure maintainability and scalability ;)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:

`git clone https://github.com/pLewandowskiEncke/FXExchange.git`

2. Open the solution in Visual Studio: `FXExchange.sln`
3. Restore the dependencies: Visual Studio should automatically restore the required NuGet packages when the solution is opened. If not, they can manually restore the packages by right-clicking on the solution in Solution Explorer and selecting "Restore NuGet Packages" or by running the following command in the terminal:


`dotnet restore`

### Running the Application

1. Navigate to the `FXExchange.ConsoleApp` project:
2. Build the Solution: Build the solution by selecting "Build Solution" from the "Build" menu or by pressing Ctrl+Shift+B.
3. Run the executable `FXExchange.exe` with provided the base currency, target currency, and amount to be exchanged:

`.\FXExchange.ConsoleApp\bin\Debug\net8.0\Exchange EUR/DKK 1000`

## Project Structure Details

- **FXExchange.ConsoleApp**: Contains the console application entry point and application-specific services and models.
- **FXExchange.Core**: Contains core business logic, domain models, and interfaces.
- **FXExchange.Tests**: Contains unit and integration tests.

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

Happy coding!
