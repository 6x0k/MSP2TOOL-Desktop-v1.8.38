# MSP2TOOL-Desktop-1.8.38
A modern, privacy-focused desktop toolkit for MovieStarPlanet 2, built with C#/.NET 8, WPF and WebView2

---

## Table of Contents

- [Features](#features)
- [Privacy](#privacy)
- [Architecture](#architecture)
- [Requirements](#requirements)
- [Installation](#installation)
- [Build from Source](#build-from-source)
- [Quick Build](#quick-build)
- [Running](#running)
- [Browser Controls](#browser-controls)
- [Clearing Local Browser Data](#clearing-local-browser-data)
- [Project Structure](#project-structure)
- [Versioning](#versioning)
- [Security](#security)
- [Disclaimer](#disclaimer)
- [License](#license)
- [Contributing](#contributing)
- [Credits](#credits)

---

## Features

### MSP2TOOL Integration

The desktop application loads the privacy-clean MSP2TOOL 1.8.38 extension directly inside Microsoft WebView2.

This means the desktop client can provide the existing MSP2TOOL functionality without requiring a separate browser-extension installation.

Included functionality includes:

- PetClone
- Pet Capture / Pet-Erkennung
- Homes Harvest
- Dynamic Home Catalog
- Dynamic Emoji Pack
- StarQuiz
- DM Spam Shield
- DM Flood Lockdown
- Outfit Copy
- Outfit Restore
- Outfit Emergency
- Avatar synchronization
- Room Image Sync
- Pet Nickname
- Account-State-Cleanup
- MSP2 profile/game tools

The exact behavior of individual tools can depend on the current MovieStarPlanet 2 website and its APIs.

---

## Privacy

The desktop project is based on the privacy-clean MSP2TOOL build.

The previously identified vendor credential-vault and telemetry functionality has been removed from the clean extension, including:

- Password interception through `webRequest`
- Password storage
- External credential-vault uploads
- Account/token synchronization to the vendor
- Hardware fingerprinting
- IP collection through IPify
- Heartbeat/presence reporting
- Remote kill-switches
- Remote configuration/gating
- Remote feedback uploads
- Unnecessary third-party vendor host permissions

The desktop application itself does not implement an external account/password collection service.

### Important

The application still connects to MovieStarPlanet 2 and its required services. Those services may process information according to their own privacy policies and terms.

Only use the application with accounts and services you are authorized to use.

---

## Architecture

```text
MSP2TOOL Desktop
│
├── C# / .NET 8
│   └── WPF
│
├── Microsoft WebView2
│   └── Embedded Chromium browser
│
└── MSP2TOOL Extension 1.8.38
    ├── manifest.json
    ├── app.js
    ├── bg.js
    ├── boot.js
    ├── d1.json
    ├── d2.json
    └── d3.json
```

The extension is installed into the WebView2 profile using the WebView2 browser-extension API.

The application uses a persistent local WebView2 profile:

```text
%LocalAppData%\MSP2TOOL\WebView2
```

This allows normal website session data to remain available between launches.

---

## Requirements

You need:

- Windows 10 or Windows 11
- Windows x64
- .NET 8 SDK for building from source
- Microsoft Edge WebView2 Runtime
- Internet connection
- A MovieStarPlanet 2 account

The project uses:

```text
Microsoft.Web.WebView2
```

The application is intended for Windows desktop systems.

---

## Installation

### Option 1 — GitHub Release

If a compiled release is available:

1. Open the repository's **Releases** page.
2. Download the latest MSP2TOOL Desktop ZIP.
3. Extract the ZIP.
4. Start:

```text
MSP2TOOL.Desktop.exe
```

5. The application initializes WebView2 and opens MovieStarPlanet 2 automatically.

### WebView2

The Microsoft Edge WebView2 Runtime is required.

Most current Windows installations already have WebView2 installed. If it is missing, install the WebView2 Runtime from Microsoft before starting the application.

### Option 2 — Build it yourself

You can also build the application directly from the source code. See [Build from Source](#build-from-source).

---

## Build from Source

Clone the repository:

```powershell
git clone https://github.com/6x0k/MSP2TOOL-Desktop-v1.8.38.git
cd MSP2TOOL-Desktop
```

Restore the NuGet dependencies:

```powershell
dotnet restore
```

Build the project:

```powershell
dotnet build -c Release
```

For a self-contained Windows x64 application:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

The published application will be located in:

```text
bin\Release\net8.0-windows\win-x64\publish\
```

---

## Quick Build

A `BUILD.bat` file is included in the repository.

You can run:

```text
BUILD.bat
```

from Windows to perform the configured build/publish process.

---

## Running

After installation or publishing, start:

```text
MSP2TOOL.Desktop.exe
```

During startup, the application:

1. Initializes WebView2.
2. Enables browser-extension support.
3. Loads the local MSP2TOOL extension.
4. Creates or opens the local MSP2TOOL WebView2 profile.
5. Navigates to MovieStarPlanet 2.
6. Makes the MSP2TOOL interface available inside the application.

---

## Browser Controls

The desktop client provides basic browser controls:

- Back
- Forward
- Refresh
- Address bar
- Go
- MSP2 shortcut
- Clear WebView2 data

The address bar allows navigation to supported web pages through the embedded browser.

---

## Clearing Local Browser Data

The application includes a control for clearing the WebView2 data used by MSP2TOOL.

This can help when:

- MSP2 session data becomes corrupted
- cached website data causes problems
- the website behaves unexpectedly
- you want to start with a fresh browser profile

Clearing browser data can log you out of websites stored inside the application.

---

## Project Structure

```text
MSP2TOOL-Desktop/
│
├── MSP2TOOL.Desktop.csproj
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── app.manifest
├── BUILD.bat
├── README.md
│
└── Resources/
    └── Extension/
        ├── manifest.json
        ├── app.js
        ├── bg.js
        ├── boot.js
        ├── d1.json
        ├── d2.json
        └── d3.json
```

There is no requirement for an empty `Bridge` directory in the current desktop build.

Empty directories are not tracked by Git and therefore do not need to be created in the GitHub repository.

---

## Versioning

Current version:

```text
MSP2TOOL Desktop 1.8.38
```

The desktop version corresponds to the integrated MSP2TOOL extension version.

The privacy-clean desktop build intentionally does not use the original vendor's remote version gate or kill-switch system.

Future update checking can be implemented through a transparent GitHub Releases or public version-file mechanism without collecting account, token, IP, or hardware information.

---

## Security

Security and privacy are core goals of this project.

When integrating future MSP2TOOL versions, the code should be reviewed for:

- Credential interception
- Token exfiltration
- Unexpected external network requests
- Remote configuration
- Hardware fingerprinting
- IP tracking
- Hidden persistence
- Suspicious permissions
- Unauthorized data collection
- Remote execution mechanisms

New network communication should be documented and justified.

The project should not reintroduce the removed vendor credential-vault or telemetry functionality.

---

## Disclaimer

MSP2TOOL Desktop is an independent third-party project.

It is **not affiliated with, endorsed by, sponsored by, or officially connected to MovieStarPlanet, MovieStarPlanet 2, or their respective owners/operators**.

MovieStarPlanet and related trademarks belong to their respective owners.

Use the software at your own risk and make sure your use complies with the applicable game's rules and terms of service.

---

## License

Choose and add an appropriate open-source license before publishing the repository if you want others to reuse, modify, or redistribute the source code.

If you do not want to grant redistribution rights yet, you can leave the repository without a license.

---

## Contributing

Contributions are welcome.

When submitting changes:

1. Keep the application privacy-focused.
2. Do not add credential collection.
3. Do not add hidden telemetry.
4. Do not upload account information to external services.
5. Avoid unnecessary permissions.
6. Document new network communication.
7. Review security-sensitive changes carefully.
8. Test changes before submitting a pull request.

---

## Credits

**MSP2TOOL Desktop**

Built with:

- C#
- .NET 8
- WPF
- Microsoft Edge WebView2
- MSP2TOOL 1.8.38 privacy-clean extension

An independent desktop client focused on functionality, transparency, and privacy.
