# WeatherCheck Web API Project
This project is a WeatherCheck Web API that allows users to get current weather conditions for a specific city and save weather conditions for their favorite cities. The API also includes user registration and token-based authentication.

## Table of Contents
- Project Overview
- Technologies Used
- Project Structure
- Installation and Setup
- API Endpoints
- Testing
- Additional Features
- Development Time
- Known Issues or Limitations

### Project Overview
The WeatherCheck Web API provides the following functionalities:

- **Weather Information:** Users can retrieve the current weather conditions for a specific city by sending a GET request to the **/api/weather/current** endpoint.

- **Saved Weather History:** Users can retrieve their saved weather conditions history for a specific city by sending a GET request to the **/api/weather/savedhistory** endpoint. 

- **Save Weather Condition:** Authenticated users can save the current weather condition for a specific city by sending a POST request to the **/api/weather/savecurrentweather** endpoint.

Users need to be authenticated to access these endpoints.

- **User Registration:** Users can register with the API by sending a POST request to the **/api/authentication/register** endpoint.

- **Token-based Authentication:** The API uses token-based authentication to secure specific endpoints. Users can obtain an authentication token by sending a POST request to the **/api/authentication/generatetoken** endpoint.

### Technologies Used
The project is built using the following technologies and frameworks:

- ASP.NET 7.0: The web framework for building APIs.
- Entity Framework Core: Used for database interactions.
- Microsoft SQL Server: The database to store weather conditions and user information.
- Swagger/OpenAPI: To generate API documentation and provide an interactive API Explorer.
- xUnit: The testing framework used for unit testing the API.

### Project Structure
The project consists of the following main components:

1. **Security:** Contains authentication-related code and business logic.
  
    - **AuthenticationService:** Responsible for user registration and token validation.
    - **TokenService:** Responsible for token generation.
    - **AuthenticationDataManager:** Data manager for user authentication using Entity Framework Core.
    - **User and UserToken:** Entities for representing user information and authentication tokens.
    - **EncryptionService:** Service for hashing passwords and tokens.

 2. **WeatherCheck:** Contains weather-related code and business logic.

    - **CurrentWeatherService:** Service to get and save current weather conditions.
    - **WeatherAPIService:** Implementation for retrieving weather data from an external API.
    - **WeatherConditionDataManager:** Data manager for weather conditions using Entity Framework Core.
    - **CurrentWeatherCondition:** Entity for representing current weather information.

3. **Data:** Contains data access code and interfaces for database interactions.
  
    - **IAuthenticationDataManager:** Interface for authentication data operations.
    - **IWeatherConditionDataManager:** Interface for weather condition data operations.
    - **WeatherConditionDataManager:** Implementation of weather condition data manager using Entity Framework Core.
    - **AuthenticationDataManager:** Implementation of authentication data manager using Entity Framework Core.

4. **Data.Mock:** Contains mock data managers used for testing.
  
    - **AuthenticationDataManager:** Mock data manager for authentication data.
    - **WeatherConditionDataManager:** Mock data manager for weather condition data.

5. **WebApi:** Contains API controllers and middleware.

    - **AuthenticationController:** Controller for user registration and token generation.
    - **WeatherController:** Controller for weather-related endpoints.
    - **AuthenticationMiddleware:** Middleware for token-based authentication.

6. **Common**: Contains common code, entities, and helper classes used across the application.

    - **APIResponseResult and APIResponseStatusCode:** Helper classes for handling API responses.
    - **BaseApiController:** Base controller for common functionality.
   
7. **Testing:** Contains unit tests for the API.
    
    - **WeatherCheckerTest:** Contains unit tests for the Weather API and authentication services.
    
### Installation and Setup
To run the WeatherCheck Web API, follow these steps:

- Ensure you have the .NET 7 SDK installed on your machine.
- Clone this repository to your local machine.
- Open the solution in Visual Studio or your favorite code editor.

### Database Configuration
1. Open the appsettings.json file and update the connection string to your Microsoft SQL Server database.
```
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WeatherCheckDB;Integrated Security=True"
}
```
2. Open the Package Manager Console in Visual Studio, **set the Default Project as Data.EFCore** and run the following command to apply the migrations and create the database:
```
Update Database
```
### Running the API
1. Run the WeatherCheck Web API project (WeatherCheck.WebApi) from Visual Studio or your code editor.

2. The API will be hosted at https://localhost:portNumber/.

3. You can use Swagger at https://localhost:portNumber/swagger to explore the API endpoints.

### API Endpoints
The following endpoints are available in the WeatherCheck Web API:

#### Authentication
- **POST /api/authentication/register:** Register a new user. Requires a JSON body containing Username and Password.

- **POST /api/authentication/generatetoken:** Generate an authentication token for an existing user. Requires a JSON body containing Username and Password.

#### Weather Information
- **GET /api/weather/current:** Get current weather conditions for a specific city. Requires the city query parameter.

- **GET /api/weather/savedhistory:** Get saved weather conditions history for a specific city. Requires the city query parameter. Requires authentication.

- **POST /api/weather/savecurrentweather:** Save current weather conditions for a specific city. Requires a JSON body containing weather information. Requires authentication.


### Testing
The project includes unit tests written using xUnit. You can run the tests by executing the following command in the Package Manager Console:
```
dotnet test
```
### Additional Features
1. **Security and Common Module Reusability:** The Security and Common modules have been designed to be reusable across other applications. These modules can be extracted and shared with other projects to handle user authentication and common utility functions.
2. **Data Storage per User:** The weather conditions are stored in the database on a per-user basis. Each user can save weather conditions for their favorite cities, and the data is segregated based on the user's identity.
3. **User ID from Token:** The user ID for each request is extracted from the token by the authentication middleware. This user ID is used to associate weather conditions with the respective user in the database.

### Development Time
The total time spent on application development was approximately 7 hours.

### Known Issues or Limitations
As of the current implementation, the solution has the following limitations:

- **External API Dependency:** The WeatherCheck API relies on the external weatherapi.com API to fetch real-time weather conditions. If the external API is down or experiences issues, it may affect the response of the WeatherCheck API.
- **Limited Weather Data:** The application stores and retrieves only the current weather conditions for a specific city. There is no historical weather data or forecasts available in the current implementation.
- **Security Considerations:** While the solution includes password hashing and token-based authentication, there could be additional security considerations to address for production deployment, such as rate limiting, SSL certificates, etc.
- **Unit Test Coverage:** While the project includes unit tests for core services and controllers, there may be areas where additional test coverage could be beneficial for further validation.
- **User Authentication Mechanism:** The current solution uses a simple username and password-based authentication mechanism. In a real-world scenario, more robust and secure authentication methods, such as OAuth, should be considered.
- **Logging Functionality:** The current solution does not implement any logging functionality. Implementing proper logging would help in debugging and tracking issues, providing insights into application behavior and performance.
- **Entities History:** The application entities do not have CreatedTime, CreatedBy, LastModifiedTime, and LastModifiedBy fields to track entities' history. Adding these fields would enable auditing and historical tracking of changes to entities.
- **Exception Handling Middleware:** Implementing a uniform exception handling middleware for common responses would enhance the application's robustness and user experience. The middleware could catch and handle exceptions in a consistent manner, providing meaningful error responses to clients.
- **Additional Indexing:** The application would greatly benefit from implementing better indexing strategies to optimize its database performance. By carefully selecting and creating appropriate indexes on key columns in the database tables (For the "WeatherConditions" table, we might add a composite index on the "City" and "UserId" columns to efficiently handle queries that retrieve weather conditions for a specific city and user. For the "UserToken" table, we might consider adding an index on the "Token" column to expedite user token validation. We may also think about adding indexes to the "User" table for columns like "Username".). This can significantly improve the speed and efficiency of data retrieval operations and lead to faster query execution, reduced database load, and ultimately result in a more responsive and performant application, enhancing the overall user experience.
