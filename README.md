# MSP2TOOL Desktop

A modern Windows desktop client for **MSP2TOOL**, built with **C# / WPF / .NET 8** and **Microsoft WebView2**.

The desktop version provides a dedicated application window for MovieStarPlanet 2 while loading the privacy-clean MSP2TOOL browser extension directly inside the embedded WebView2 browser.

> **Version:** 1.8.38  
> **Platform:** Windows 10/11 x64  
> **Framework:** .NET 8  
> **UI:** WPF  
> **Browser:** Microsoft Edge WebView2

---

## Features

### MSP2TOOL integration

The desktop application loads the cleaned MSP2TOOL 1.8.38 extension inside WebView2.

This keeps the extension's existing MSP2 functionality available without requiring the user to install the browser extension separately.

Included extension functionality includes:

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

The exact functionality depends on the current MSP2 website and the extension implementation.

---

## Privacy

This repository is based on the MSP2TOOL build extension.

The cleaned extension does **not intentionally include the previously identified vendor credential-vault and telemetry components**, including:

- password interception through `webRequest`
- password storage
- external credential vault uploads
- account/token synchronization to the vendor
- hardware fingerprinting
- IP collection through IPify
- heartbeat/presence reporting
- remote kill-switches
- remote configuration/gating
- remote feedback uploads
- third-party vendor host permissions

The desktop application itself does not implement an external account/password collection service.

### Important

The application still loads MovieStarPlanet 2 and its required services. MSP2 itself may process data according to its own privacy policy and terms.

Only use the application with accounts and services you are authorized to use.

I removed all crap that MSP2Soft is collecting from users cuz they dont want to listen

---

## Architecture

```text
MSP2TOOL Desktop
│
├── WPF / .NET 8
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   └── App.xaml
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

A persistent WebView2 profile is stored locally under:

```text
%LocalAppData%\MSP2TOOL\WebView2
```

This allows the desktop application to retain normal browser session data between launches.

---

## Requirements

You need:

- Windows 10 or Windows 11
- x64 system
- .NET 8 SDK
- Microsoft Edge WebView2 Runtime
- Internet connection
- A MovieStarPlanet 2 account

For development, install the .NET 8 SDK.

The project uses the following NuGet package:

```text
Microsoft.Web.WebView2
```

---

## Build from source

Clone the repository:

```powershell
git clone https://github.com/6x0k/MSP2TOOL-Desktop.git
cd MSP2TOOL-Desktop
```

Restore dependencies:

```powershell
dotnet restore
```

Build:

```powershell
dotnet build -c Release
```

Publish a self-contained Windows x64 build:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

The published application can be found under:

```text
bin\Release\net8.0-windows\win-x64\publish\
```

---

## Quick build

The repository also contains:

```text
BUILD.bat
```

Run it from Windows to perform the configured build/publish process.

---

## Running

After building, start:

```text
MSP2TOOL.Desktop.exe
```

The application will:

1. Initialize WebView2.
2. Enable browser-extension support.
3. Load the local MSP2TOOL extension.
4. Create/use the local MSP2TOOL WebView2 profile.
5. Open MovieStarPlanet 2.
6. Make the MSP2TOOL interface available inside the application.

---

## Browser controls

The desktop client includes basic browser controls:

- Back
- Forward
- Refresh
- Address bar
- Go
- MSP2 shortcut
- Clear WebView2 data

The address bar allows navigation to other web pages supported by the embedded browser.

---

## Clearing local browser data

The application provides a button for clearing the WebView2 data used by MSP2TOOL.

This can be useful when:

- MSP2 login/session data becomes corrupted
- the website behaves unexpectedly
- cached website data needs to be reset
- you want to start with a fresh local WebView2 profile

Clearing data may log you out of websites stored in the application profile.

---

## Project structure

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

---

## Versioning

Current desktop release:

```text
MSP2TOOL Desktop 1.8.38
```

The desktop version follows the integrated MSP2TOOL extension version.

The privacy-clean desktop build intentionally does not use the original vendor's remote version gate or kill-switch system.

Future update checks can be implemented through a transparent public GitHub release mechanism.

---

## Security

Security and privacy are important parts of this project.

The desktop application is designed to avoid unnecessary collection and transmission of user information.

The repository should remain transparent and auditable. If new upstream MSP2TOOL versions are integrated, the code should be reviewed for:

- credential interception
- token exfiltration
- unexpected network requests
- remote configuration
- hardware fingerprinting
- tracking
- hidden persistence
- suspicious permissions
- unauthorized data collection

Do not reintroduce the removed vendor credential-vault or telemetry functionality.

---

## Disclaimer

MSP2TOOL Desktop is an independent third-party project.

It is **not affiliated with, endorsed by, sponsored by, or officially connected to MovieStarPlanet, MovieStarPlanet 2, or their respective owners/operators**.

MovieStarPlanet and related trademarks belong to their respective owners.

Use the software at your own risk and make sure your usage complies with the applicable game's rules and terms of service.

---

## License

Choose and add a license before publishing the repository if you want others to reuse, modify, or redistribute the source code.

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
7. Test changes before submitting a pull request.

---

## Credits

**MSP2TOOL Desktop**

Desktop implementation using:

- C#
- .NET 8
- WPF
- Microsoft Edge WebView2
- MSP2TOOL 1.8.38 privacy-clean extension

Built as an independent desktop client focused on functionality, transparency, and privacy.
