-- SQL script to add RequestPurpose table to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create RequestPurposes table
CREATE TABLE IF NOT EXISTS "RequestPurposes" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

