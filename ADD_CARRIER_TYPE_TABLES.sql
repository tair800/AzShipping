-- SQL script to add CarrierType table to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create CarrierTypes table
CREATE TABLE IF NOT EXISTS "CarrierTypes" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "IsActive" BOOLEAN NOT NULL,
    "Colour" VARCHAR(7) NOT NULL,
    "IsDefault" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

