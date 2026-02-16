-- SQL script to add WorkerPost tables to existing PostgreSQL database
-- Run this script in your PostgreSQL database (AzShippingDb)
-- You can run this in pgAdmin Query Tool or psql

-- Create WorkerPosts table
CREATE TABLE IF NOT EXISTS "WorkerPosts" (
    "Id" UUID PRIMARY KEY,
    "IsActive" BOOLEAN NOT NULL,
    "CreatedAt" TIMESTAMP NOT NULL,
    "UpdatedAt" TIMESTAMP
);

-- Create WorkerPostTranslations table
CREATE TABLE IF NOT EXISTS "WorkerPostTranslations" (
    "Id" UUID PRIMARY KEY,
    "WorkerPostId" UUID NOT NULL,
    "LanguageCode" VARCHAR(10) NOT NULL,
    "Name" VARCHAR(200) NOT NULL,
    CONSTRAINT "FK_WorkerPostTranslations_WorkerPosts_WorkerPostId" 
        FOREIGN KEY ("WorkerPostId") 
        REFERENCES "WorkerPosts" ("Id") 
        ON DELETE CASCADE
);

-- Create unique index for WorkerPostId + LanguageCode combination
CREATE UNIQUE INDEX IF NOT EXISTS "IX_WorkerPostTranslations_WorkerPostId_LanguageCode" 
    ON "WorkerPostTranslations" ("WorkerPostId", "LanguageCode");

