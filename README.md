# Getting started with TripPair
TripPair is a simple web application for travel inspiration. It allows users to save, view and filter resorts they might be interested in visiting. A user can search for a specific resort or a specific location.

The below instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
## ♻️ Installing
```
git clone https://github.com/sasatrajkova/trip-pair-app.git
```
## 🚀 Running
The backend and frontend must be run seperately.

### 💾 Backend (Database and API)
- Open the cloned repository using Rider or Visual Studio
- In [appsettings.json](./api/appsettings.json), adjust the connection string based on your database setup
- To setup the database schema:
    - Open a new command-line interface
    - Enter the api project directory
    - Execute `dotnet ef database update`
- <i>//TODO seed initial data</i>
- Run `tripPairApi`
### 🎨 Frontend (User Interface)
- Open the cloned repository using Visual Studio Code or Intellij
- Open a new command-line interface, enter the app project directory, and execute:
    - `npm start` to run the app in development mode
    - `npm test` to launch the test runner in the intractive watch mode
    - `npm run build` to build the app for production