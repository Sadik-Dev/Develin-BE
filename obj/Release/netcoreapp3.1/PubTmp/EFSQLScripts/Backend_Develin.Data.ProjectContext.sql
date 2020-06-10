IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Gender] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Token] nvarchar(max) NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE TABLE [Projects] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        [Created] datetime2 NOT NULL,
        [ManagerId] int NULL,
        [Deadline] datetime2 NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Projects_Employees_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE TABLE [ProjectEmployees] (
        [ProjectId] int NOT NULL,
        [EmployeeId] int NOT NULL,
        CONSTRAINT [PK_ProjectEmployees] PRIMARY KEY ([ProjectId], [EmployeeId]),
        CONSTRAINT [FK_ProjectEmployees_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ProjectEmployees_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE TABLE [Tasks] (
        [Id] int NOT NULL IDENTITY,
        [Titel] nvarchar(max) NULL,
        [EmployeeId] int NULL,
        [State] int NOT NULL,
        [dateOfFinish] datetime2 NOT NULL,
        [SpentTime] int NOT NULL,
        [ProjectId] int NULL,
        CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tasks_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Tasks_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [Text] nvarchar(max) NULL,
        [AuthorId] int NULL,
        [Date] datetime2 NOT NULL,
        [TaskId] int NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_Employees_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Comments_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Comments_AuthorId] ON [Comments] ([AuthorId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Comments_TaskId] ON [Comments] ([TaskId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_ProjectEmployees_EmployeeId] ON [ProjectEmployees] ([EmployeeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Projects_ManagerId] ON [Projects] ([ManagerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tasks_EmployeeId] ON [Tasks] ([EmployeeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    CREATE INDEX [IX_Tasks_ProjectId] ON [Tasks] ([ProjectId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200520000320_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200520000320_InitialCreate', N'3.1.3');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200523210625_First')
BEGIN
    ALTER TABLE [Employees] ADD [IsManager] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200523210625_First')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200523210625_First', N'3.1.3');
END;

GO

