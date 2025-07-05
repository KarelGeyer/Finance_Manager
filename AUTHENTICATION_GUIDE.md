# Authentication Implementation Test Guide

## Overview
This document describes how to test the authentication system that has been implemented for the Finance Manager project.

## Backend Authentication (Already Implemented)
The backend UserService already provides:
- POST `/api/auth/Login` - Authenticates user and returns JWT token
- GET `/api/auth/VerifyToken` - Verifies JWT token validity
- JWT Bearer token authentication
- User blocking/verification logic
- Password hashing and validation

## Frontend Authentication (Newly Implemented)

### 1. AuthenticationService
- **File**: `AppUI/Services/AuthenticationService.cs`
- **Purpose**: Communicates with backend authentication endpoints
- **Methods**:
  - `LoginAsync(username, password)` - Calls backend login API
  - `VerifyTokenAsync(token)` - Validates token with backend

### 2. Enhanced UserState
- **File**: `AppUI/State/UserState.cs`
- **Purpose**: Manages authentication state throughout the application
- **Features**:
  - `IsAuthenticated` - Boolean indicating auth status
  - `Token` - Stores JWT token
  - `Username` - Stores authenticated username
  - `SetAuthenticated()` - Sets user as authenticated
  - `Logout()` - Clears authentication state
  - Error handling for failed authentication

### 3. Login Page
- **File**: `AppUI/Components/Pages/Login.razor`
- **Route**: `/login`
- **Features**:
  - Username/password form with validation
  - Loading state during authentication
  - Error message display
  - Redirects to home on successful login
  - Uses MudBlazor components for consistent UI

### 4. Navigation Integration
- **File**: `AppUI/Components/Layout/NavMenu.razor`
- **Features**:
  - Shows main navigation only when authenticated
  - Displays current username when logged in
  - Login link for unauthenticated users
  - Logout functionality

### 5. Route Protection
- **File**: `AppUI/Components/Layout/MainLayout.razor`
- **Features**:
  - Redirects unauthenticated users to login page
  - Shows full application layout only when authenticated
  - Special handling for login page (no sidebar/navigation)

### 6. Authentication Wrapper Component
- **File**: `AppUI/Components/Generic/AuthenticationWrapper.razor`
- **Purpose**: Additional component for protecting specific routes
- **Usage**: Wrap any content that requires authentication

## Testing Instructions

### Manual Testing (once MAUI workloads are available):

1. **Start the UserService**:
   ```bash
   cd Services/UserService
   dotnet run
   ```

2. **Start the AppUI**:
   ```bash
   cd AppUI
   dotnet run
   ```

3. **Test Authentication Flow**:
   - Navigate to the application
   - Should automatically redirect to `/login`
   - Enter valid credentials (must exist in UserService database)
   - Should redirect to home page with navigation visible
   - Click logout to clear authentication state

### API Testing (backend):

1. **Test Login Endpoint**:
   ```bash
   curl -X POST https://localhost:7001/api/auth/Login \
     -H "Content-Type: application/json" \
     -d '{"username":"testuser","password":"testpass"}'
   ```

2. **Test Token Verification**:
   ```bash
   curl "https://localhost:7001/api/auth/VerifyToken?token=YOUR_JWT_TOKEN"
   ```

## Configuration Notes

### Backend Configuration (UserService):
- JWT secret key configured in `Program.cs` (should be moved to configuration)
- JWT Bearer authentication enabled
- Token expiration set to 30 minutes

### Frontend Configuration (AppUI):
- AuthenticationService configured to call `https://localhost:7001/api/auth`
- Service registered in DI container in `MauiProgram.cs`
- UserState registered as singleton for application-wide state

## Security Considerations

1. **JWT Secret**: Currently hardcoded, should be moved to configuration
2. **HTTPS**: Ensure backend runs on HTTPS in production
3. **Token Storage**: Currently in memory only, consider persistent storage for mobile app
4. **Token Refresh**: Not implemented, tokens expire after 30 minutes
5. **Error Handling**: Basic error handling implemented, could be enhanced

## Future Enhancements

1. **Token Persistence**: Store tokens in secure storage for mobile platforms
2. **Token Refresh**: Implement automatic token refresh
3. **Password Reset**: Add forgot password functionality
4. **Registration**: Add user registration flow
5. **Role-based Authorization**: Implement role-based access control
6. **Biometric Authentication**: Add fingerprint/face ID for mobile