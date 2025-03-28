# Blazor Server App - Reproducible Issue

## Overview
This Blazor Server app demonstrates the issue previously reported. It includes a simple payment form embedded into the Home.razor page, where the error can be reproduced.

## Prerequisites

- .NET 8 SDK (or compatible version)
- Visual Studio 2022 (or later) with Blazor Server support
- Stripe API Key (for testing)

## Setup Instructions

### 1. Clone the Repository

- git clone [Repository URL]
- Open with Visual Studio 2022

### 2. Restore Dependencies

dotnet restore

### 3. Configure the Environment

- Navigate to program.cs
- Update _StripeConfiguration.ApiKey = "API KEY";_
- Navigate to wwwroot/js/checkout.js
- Update _const stripe = Stripe("API KEY");_

### Run the Application

- Run the application in DEBUG using Visual Studio you will notice the form appear proceeded by an Unhandled Exception: Empty Challenge.
- Run the application in RELEASE using Visual Studio, you will notice no crashes. However, navigate away from the Home page and back again (without a refresh), the form never comes back.
- - Refresh the page and it re-renders.

## Notes

- If the issue does not appear immediately, try clearing the browser cache or restarting the application.
- Feel free to reach out if additional information is required.
