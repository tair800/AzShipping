-- SQL script to add DeferredPaymentCondition table to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create DeferredPaymentConditions table
CREATE TABLE IF NOT EXISTS "DeferredPaymentConditions" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(200) NOT NULL,
    "FullText" VARCHAR(1000),
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

