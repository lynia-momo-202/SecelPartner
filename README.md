# SecelPartner

## Overview

Application web de gestion des partenariats d'une entreprise du nom de SECEL

## Installation

### Prerequisites

For local development without docker

- [.NET 6 SDK](https://dotnet.microsoft.com/fr-fr/download/dotnet/6.0) version 6.0.42 installed
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) or your databse Instance running
- [Visual Studio 2022](https://visualstudio.microsoft.com/fr/)  / [Rider](https://www.jetbrains.com/fr-fr/rider/) / [VS Code](https://code.visualstudio.com/)

### Setup

- Clone project
```bash
git clone https://github.com/lynia-momo-202/SecelPartner.git && cd SecelPartner
``` 

<br>

1. Restore dotnet tools
```bash
dotnet tool restore
``` 
2. Setup husky
```bash
dotnet husky install
``` 

3. Clean, restore and build solution

```bash
dotnet clean && dotnet restore && dotnet build
``` 


4.


```bash
cd src/SecelPartner.Infrastructure
dotnet ef database update
```

- Run the project
```bash
cd ../SecelPartner.UI
dotnet run
```

## Contributing

// Write a contributing guidelines for your projects

You can read [OSS Place](https://github.com/osscameroon/place-api),  [contributing guidelines](https://github.com/osscameroon/place-api/blob/main/CONTRIBUTING.md) as example.

## Code of Conduct

// Define the code of conduct policy of your project 

You can read [OSS Place](https://github.com/osscameroon/place-api),  [code of conduct](https://github.com/osscameroon/place-api/blob/main/CODE_OF_CONDUCT.md) as example.



## License

Place is a free and open source project, released under the permissible [MIT license](LICENSE).