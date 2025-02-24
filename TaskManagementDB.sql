use TaskManagementDB
go

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(255) UNIQUE NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) UNIQUE NOT NULL,
    Description TEXT
);

CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Description TEXT,
    IsCompleted BIT DEFAULT 0,
    UserId INT,
    CategoryId INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE SET NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE SET NULL
);

CREATE TABLE TaskComments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT,
    UserId INT,
    Content TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE Labels (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE TaskLabels (
    TaskId INT,
    LabelId INT,
    PRIMARY KEY (TaskId, LabelId),
    FOREIGN KEY (TaskId) REFERENCES Tasks(Id) ON DELETE CASCADE,
    FOREIGN KEY (LabelId) REFERENCES Labels(Id) ON DELETE CASCADE
);