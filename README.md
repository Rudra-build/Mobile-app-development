CodeLingo

CodeLingo is a gamified mobile quiz application built using .NET MAUI (iOS) and ASP.NET Core. It includes user authentication, quizzes, leaderboard, streak tracking, AI-generated questions, subscription system, and premium analytics.

⸻

Features

* User registration and login
* Quiz engine with categories and scoring
* Gamification (points, streaks, leaderboard, badges)
* AI-generated programming quizzes (OpenAI)
* Subscription system (Free vs Premium)
* Premium analytics dashboard
* Stripe test-mode payment integration

⸻

Requirements

* .NET SDK (v10)
* Xcode + iOS Simulator
* Git
* OpenAI API key
* Stripe test secret key

⸻

Running the Project

Backend

cd /Users/rudrarajgohil/Programming/CodeLingoApi
dotnet build
dotnet run --urls "http://0.0.0.0:5257"

⸻

Frontend (iOS Simulator)

open -a Simulator
xcrun simctl boot 08CC77B8-6B2D-48A9-B9C8-E54C5E713DFB
xcrun simctl bootstatus 08CC77B8-6B2D-48A9-B9C8-E54C5E713DFB -b
cd /Users/rudrarajgohil/Programming/Mobile-app-development-clean
rm -rf bin obj
dotnet build CodelingoApp.csproj -f net10.0-ios
xcrun simctl install 08CC77B8-6B2D-48A9-B9C8-E54C5E713DFB ./bin/Debug/net10.0-ios/iossimulator-arm64/CodelingoApp.app

Open the app manually in the simulator.

⸻

Test Users

dion
dion@test.com
pass- abc
aryan
aryan@test.com
pass- abc

You can also create new users using the same format:

name
name@test.com
pass- abc

⸻

Stripe Test Payment

Use the following card for testing:

Card Number: 4242 4242 4242 4242
Expiry: Any future date
CVC: Any 3 digits

Flow

1. Login
2. Go to Subscription page
3. Tap Premium
4. Complete payment in browser
5. Return to app
6. Refresh subscription page

⸻

Notes

* Backend must be running before starting the app
* AI quiz is available only for Premium users
* Analytics is available only for Premium users
* Stripe is in test mode (no real payments)
* API keys are stored in backend .env file

