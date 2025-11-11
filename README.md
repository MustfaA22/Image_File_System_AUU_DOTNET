# Image File System 
-------------------

## Project Description
-------------------

This project implements a secure, traceable file upload system using Dockerized microservices with pre-signed URL functionality. The system simulates a scenario where sellers can upload product images through a secure and scalable workflow.

## Architecture Overview
--------------------

The system consists of three main components:

1. App Service (AppServiceControllers)
   - Generates pre-signed URLs for secure file uploads
   - Acts as the main entry point for client applications

2. Product Service (ProductController)
   - Handles product creation with image validation
   - Validates image existence before product creation
   - Stores product information (in-memory)

3. Storage Service (StorageController)
   - validates pre-signed URLs
   - Handles secure file uploads with signature verification
   - Stores uploaded images and returns unique Image IDs

## Key Features
------------

- Secure Pre-signed URL Workflow: Ensures only authorized uploads
- Signature Validation: Verifies upload requests for security
- Microservice Architecture: Decoupled services for scalability
- Comprehensive Logging: Full observability with structured logging
- Docker Support: Containerized deployment
- RESTful APIs: Clean API design following best practices

## Workflow
--------

1. Request Upload URL: Seller requests a pre-signed URL with file metadata
2. Upload Image: Seller uploads file directly to Storage Service using the pre-signed URL
3. Create Product: Seller creates product with the obtained Image ID


## Prerequisites
-------------
- .NET 8.0 SDK or later
- Git
  #### OR
- Docker Desktop

## Steps
-----

 1. Clone the repository
git clone <repository-url>
cd ImageFileSystem_AUU_Test

 2. Build the Docker image
docker build -t imagefilesystemtest .

 3. Run the container
docker run -d -p 8080:8080 --name imagefilesystem imagefilesystemtest




## API Endpoints
-------------

App Service:
- POST /api/generate-presigned-url - Generate pre-signed URL for upload
- POST /api/products - Create a new product
- GET /api/get-all-products - Retrieve all products

Storage Service:
- POST /api/Storage/upload - Upload file using pre-signed URL


## Testing the Workflow
====================
### From Swagger UI:
1. Generate Pre-signed URL
2. Upload File (using the signature and token returned from step 1)
3. Create Product
  
