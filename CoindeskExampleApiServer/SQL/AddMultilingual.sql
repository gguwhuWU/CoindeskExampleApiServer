IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Copilots] (
    [Code] nvarchar(450) NOT NULL,
    [ZWName] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Copilots] PRIMARY KEY ([Code])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240622155038_InitialCreate', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Copilots].[ZWName]', N'ZWName', N'COLUMN';
GO

ALTER TABLE [Copilots] ADD [JPName] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Copilots] ADD [USName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240623092754_AddMultilingual', N'8.0.6');
GO

COMMIT;
GO

