-- SQL script to add LoadingMethod tables to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create LoadingMethods table
CREATE TABLE IF NOT EXISTS "LoadingMethods" (
    "Id" UUID PRIMARY KEY,
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

-- Create LoadingMethodTranslations table
CREATE TABLE IF NOT EXISTS "LoadingMethodTranslations" (
    "Id" UUID PRIMARY KEY,
    "LoadingMethodId" UUID NOT NULL,
    "LanguageCode" VARCHAR(10) NOT NULL,
    "Name" VARCHAR(200) NOT NULL,
    CONSTRAINT "FK_LoadingMethodTranslations_LoadingMethods_LoadingMethodId" 
        FOREIGN KEY ("LoadingMethodId") 
        REFERENCES "LoadingMethods" ("Id") 
        ON DELETE CASCADE
);

-- Create unique index for LoadingMethodId + LanguageCode combination
CREATE UNIQUE INDEX IF NOT EXISTS "IX_LoadingMethodTranslations_LoadingMethodId_LanguageCode" 
    ON "LoadingMethodTranslations" ("LoadingMethodId", "LanguageCode");
