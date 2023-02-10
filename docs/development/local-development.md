# Local Development

## Prerequisites

In order to work with this repo in your local development environment, you will need to install the following packages:

- [.Net 6.0](https://dotnet.microsoft.com/en-us/download)
- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Azure Functions Core Tools](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=enterprise&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio)
  - After installation, it needs to be manually started using Azurite commands
- All required extensions will be recommended to be installed when opening the workspace (check extensions.json file in .vscode folder).

## Project Structure

The folder structure for this repo is as below:

- docs: where all technical documentation of the project located in.
- src: contains all source code for the projects.
  - Microsoft.Azure.Functions:
  a project containing the source code of the framework to easily work with Azure functions triggered by event hub messages.
  - Microsoft.Azure.Models:
  containing a few examples of events that are used in this project and shows how to define ant integration event in the project.
  - Microsoft.Azure.Functions.StreamingDataFlow: A sample project which is using the event driven framework to handle all events received by Azure functions.
  This project contains two main functions: StreamingDataChangedFunction and StreamingDataProcessedFunction.
- tests:
containing all projects required for testing including unit-testing and component testing.
All unit/component tests will utilize xUnit framework.
- tools:
any tools and components that may be used during the development (such as event hub event simulator)

## More Links

- The project uses [central package management feature](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management).
So all C# projects that need to to install a nuget package,
they should specify the version of the nuget package in Directory.Packages.props file in solution level.
