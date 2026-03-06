# MunitS hub

MunitS Hub is a .NET 10 backend service that acts as a gateway and orchestration layer between [MunitS Hub Client](https://github.com/AndriiS1/munits-hub-client) and [MunitS](https://github.com/AndriiS1/MunitS) (object storage). The main purpose of this service is authentication and objects access control.

It exposes a RESTful API for user management, bucket operations, and object handling, while communicating with a backend storage service via gRPC. It uses a clean architecture with CQRS to separate concerns and manage business logic effectively.

## Features

### User Management

Sign-up (**POST** `/users/sign-up`): Register a new user account.

Login (**POST** `/users/login`): Authenticate a user and receive JWT access and refresh tokens.

Refresh Token (**POST** /users/refresh-token): Obtain a new access token using a valid refresh token.

Get User Email (**GET** /users/email): Retrieve the email of the currently authenticated user.

### Bucket Management

Create Bucket (**POST** `/buckets/`): Create a new storage bucket with optional versioning.

Delete Bucket (**DELETE** `/buckets/{id}`): Remove a bucket.

Get Bucket Details (**GET** `/buckets/{id}` or GET `/buckets/by-name/{bucketName}`): Retrieve information about a specific bucket.

List Buckets (**POST** `/buckets/filter`): Get a paginated list of buckets owned by the user.

Search Buckets (**POST** `/buckets/search`): Search for user-owned buckets by name.

Check Bucket Existence (**POST** `/buckets/exists`): Verify if a bucket name is already in use.

Get Bucket Metrics (**GET** `/buckets/{id}/metrics`): Fetch usage metrics for a bucket.

### Object Management

Initiate Multipart Upload (**POST** `/buckets/{bucketId}/objects/uploads/initiate`): Begin the process of uploading a large object in parts.

Get Upload Signed URLs (**GET** `/buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/signed-urls`): Generate pre-signed URLs for uploading individual parts of a multipart upload.

Complete Multipart Upload (**POST** `/buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/`complete): Finalize a multipart upload after all parts have been uploaded.

Abort Multipart Upload (**POST** `/buckets/{bucketId}/objects/{objectId}/uploads/{uploadId}/abort`): Cancel an in-progress multipart upload.

List Objects (**POST** /buckets/{bucketId}/objects/filter): List objects within a bucket, filterable by a prefix.

Get Object Details (**GET** /buckets/{bucketName}/objects/{objectId}): Retrieve metadata for a specific object and its versions.

Delete Object/Version (**DELETE** /buckets/{bucketId}/objects/{\*fileKey}): Delete an entire object or a specific version of an object.

## Getting started

### Prerequisites

- .NET 10 SDK or later
- Docker (optionally for cassandra cluster)

### Configuration

To successfully start the project you have to add env variables. Full example:

```JSON
{
	"Storage": {
	"ConnectionUrl": "address_of_run_storage_service" // https://localhost:7055
	},
	"DataBase": {
	"Name": "your_database_name", // MunitSHub
	"ConnectionString": "your_mongodb_connection_string" // mongodb+srv://...
	},
	"Clients": {
	"ClientsUrls": ["munits_client_url"] // http://localhost:3000
	},
	"Jwt": {
	"Audience": "your_audience", // https://localhost:3000
	"Issuer": "your_issuer", // https://localhost:7172
	"Key": "your_super_secret_jwt_key_that_is_long_enough",
	"TokenValidityInMinutes": 10
	}
}
```

### Running the Application

1. Clone the repository
   - git clone https://github.com/AndriiS1/munits-hub.git
   - cd munits-hub
   - Configure your settings in src/MunitSHub/appsettings.Development.json or through user secrets.
2. Navigate to the main project directory and run the application
   - cd src/MunitSHub
   - dotnet run
   - The API will be available at the URLs specified in launchSettings.json (e.g., http://localhost:5092).

# Links

- [MunitS](https://github.com/AndriiS1/munits)
- [MunitS Hub Client](https://github.com/AndriiS1/munits-hub-client)
