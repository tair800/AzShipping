-- SQL script to add DrivingLicenceCategory table to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create DrivingLicenceCategories table
CREATE TABLE IF NOT EXISTS "DrivingLicenceCategories" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(10) NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

