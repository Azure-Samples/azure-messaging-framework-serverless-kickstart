# Event Hub Simulator <!-- omit in toc -->

Simulates a message being sent from the Streaming Azure function or Pipeline.

## Sections <!-- omit in toc -->

- [Configuration](#configuration)
  - [local.settings.json](#localsettingsjson)
  - [Streaming Data Event Changed V1 Config](#streaming-data-event-changed-v1-config)
  - [Streaming Data Event Processed V1 Config](#streaming-data-event-processed-v1-config)
- [Run the Azure Functions locally](#run-the-azure-functions-locally)
  - [Steps](#steps)

## Configuration

### local.settings.json

- **EVENT_HUB_CONNECTION_STRING** [REQUIRED]: The connection string for Azure Event Hub.

- **EVENT_HUB_NAME** [REQUIRED]: The name of the Event Hub where the event will be sent.

- **EVENT_DETAILS_FILE** [REQUIRED]: The file that contains the list of events that will be sent to the Event Hub.

- **EVENT_TYPE** [REQUIRED]: The type of event that will is being sent to Event Hub. The event type can be one of the following:

  - **1**: Streaming Data Event Changed V1
  - **2**: Streaming Event Processed V1

    **NOTE**: The event type should be a number and should **not** be surrounded in quotes.

### Streaming Data Event Changed V1 Config

The file for `Streaming Data Event Changed V1` should contain the following content.

```json
[
    {
        "MachineId": "1",
        "Temperature": 20,
        "Humidity": 30
    }
]
```

Where:

- **MachineId** [REQUIRED]: The unique identifier of the machine.
- **Temperature** [REQUIRED]: The temperature of the machine.
- **Humidity** [REQUIRED]: The humidity of the machine.

**NOTE**: This payload should only have one event to mimic the expected behavior from the Streaming Azure function or pipeline.

### Streaming Data Event Processed V1 Config

The file for `Streaming Data Event Processed V1` should contain the following content.

```json
[
    {
        "MachineId": "1",
        "Temperature": 20,
        "Humidity": 30
    }
]
```

Where:

- **MachineId** [REQUIRED]: The unique identifier of the machine.
- **Temperature** [REQUIRED]: The temperature of the machine.
- **Humidity** [REQUIRED]: The humidity of the machine.

**NOTE**: This payload should only have one event to mimic the expected behavior from the Streaming Azure function or pipeline.

## Run the Azure Functions locally

### Steps

To copy start with the sample configuration:

```bash
cp sample.local.settings.json local.settings.json
```

To run the Program:

```bash
dotnet run
```
