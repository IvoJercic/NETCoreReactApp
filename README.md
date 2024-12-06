NETCoreReactApp
============

Overview
--------

CoreReactApp is a full-stack web application that uses **ASP.NET Core** for the backend and **React** for the frontend. The app leverages SQL Server for data storage and integrates with the **Amadeus API** to fetch flight information.

Prerequisites
-------------

Before you begin, ensure you have the following installed:

*   **SQL Server 2020** or later
    
*   **.NET Core SDK** (for running the server)
    
*   **Node.js and npm** (for managing frontend dependencies)
    
*   **Amadeus API Account** (for flight data access)
    

Setup Instructions
------------------

### 1\. Install SQL Server 2020

First, install **SQL Server 2020** on your local machine or use a remote instance.

### 2\. Configure Database Connection

In your project, navigate to:

CoreReactApp\\CoreReactApp.Server\\appsettings.json or CoreReactApp\\CoreReactApp.Server\\appsettings.Development.json

Add your SQL Server connection string under the ConnectionStrings section:

"ConnectionStrings": {    "DefaultConnection": "Server=<YOUR_SQL_SERVER>;Database=<DATABASE_NAME>;Trusted_Connection=True;TrustServerCertificate=True;"  }   `

*   Replace  <YOUR_SQL_SERVER> with your SQL Server instance address.
    
*   Replace <DATABASE_NAME> with the name of the database you'd like to use (it will be created automatically due to code-first principle).
    

### 3\. Add Amadeus API Credentials

You’ll need an **Amadeus API account** to fetch flight data.

*   Visit Amadeus for Developers to create an account and get your ApiKey and ApiSecret.
    

Add these credentials to your appsettings.Development.json file:

"AmadeusAPI": {    "ApiKey": "<API_KEY>",    "ApiSecret": "<API_SECRET>"  }   `

Replace <API_KEY> and <API_SECRET> with the credentials you received from Amadeus.

### 4\. Update the Database

Open the **Package Manager Console** in Visual Studio and run the following command to apply database migrations:

update-database   `

This will set up the necessary database tables using Entity Framework's code-first approach.

### 5\. Run the Application

Now, you’re ready to run the app!

*   Press F5 or click **Run** in Visual Studio to start the backend.
    
*   Once the server is running, open a web browser and go to **https://localhost:52477/** to access the app.
    

### 6\. Test the Application

You can use the following **IATA airport codes** for testing flight data in the app:

SPU, ZAG, ATL, PEK, LAX, DXB, HND, ORD, LHR, CDG, DFW, DEN, SIN, AMS, ICN, BKK, HKG, JFK, KUL, FRA, IST, GRU, MIA, SYD, YVR, YYZ, MUC, SFO, SEA, LAS, PHX, MCO, IAH, BOS, MAD, BCN, GIG, DEL, BOM, JNB, CPT, CAI, DOH, MEX, EZE, ZRH, DUB, BRU, ARN, HEL, OSL, CPH, VIE, PRG, WAW, LIS, LED, DME, SVO, TPE, MNL, BNE, ADL, AKL, CHC, DPS, CGK, HAN, SGN, KIX, CTS, NRT, GVA, NCE, VCE, FCO, MXP, NAP, ATH, SKG, BLL, GOT, BUD, OTP, SOF, TBS, EVN, GYD, ALA, CCU, BLR, HYD, MAA, KTM, DAC, CMB, MLE, JED, RUH, DMM, AMM, TLV, BEY, AUH, SHJ, KWI, BAH, MCT, ISB, LHE, KHI, PNQ, GOI, TRV, COK, IXC, GAU, IXB, PAT, VNS, JAI, AMD, BDQ, IDR, BHO, NAG, RPR, VGA, IXR, IXT, JMU, SLV, IXD, SXR, IXA, BOM   `

Simply input any of these IATA codes into the app to retrieve flight data.
