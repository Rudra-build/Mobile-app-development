CodelingoAppFixed

What changed
- Added a proper .NET 10 MAUI project file.
- Added iOS and MacCatalyst platform startup files.
- Added Info.plist files with bundle identifier.
- Removed the font/debug setup that caused earlier build issues.
- Kept the starter app pages and navigation.

Run on Mac
1. Unzip this project.
2. Open Terminal in the project folder.
3. Run:
   dotnet workload install maui ios maccatalyst
   dotnet restore
   dotnet build -f net10.0-ios
   dotnet run -f net10.0-ios

If simulator does not open
- Open Xcode once.
- Run:
  sudo xcode-select --switch /Applications/Xcode.app/Contents/Developer
  sudo xcodebuild -runFirstLaunch
