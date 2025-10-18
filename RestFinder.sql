CREATE DATABASE RestaurantFinder;
USE RestaurantFinder;


CREATE TABLE UserRoles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE,  -- ej 'Admin', 'Restaurant'
    Description NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleID INT NOT NULL,  -- FK to UserRoles
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES UserRoles(RoleID)
);


CREATE TABLE Restaurants (
    RestaurantID INT IDENTITY(1,1) PRIMARY KEY,
	UserID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Website NVARCHAR(100),
	WazeLink NVARCHAR(500),
	GoogleMapsLink NVARCHAR(500),
	Latitude DECIMAL(9,6),   -- ej 9.9333
    Longitude DECIMAL(9,6),  -- ej -84.0833,
    Rating DECIMAL(2,1),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
);


CREATE TABLE Schedules (
    ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
    RestaurantID INT NOT NULL,
    DayOfWeek TINYINT NOT NULL, -- 1 = Monday, 7 = Sunday
    OpenTime TIME NOT NULL,
    CloseTime TIME NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RestaurantID) REFERENCES Restaurants(RestaurantID)
);

CREATE TABLE Menus (
    MenuID INT IDENTITY(1,1) PRIMARY KEY,
    RestaurantID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RestaurantID) REFERENCES Restaurants(RestaurantID)
);

CREATE TABLE MenuItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    MenuID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(10,2) NOT NULL,
    Category NVARCHAR(50),
    ImageUrl NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MenuID) REFERENCES Menus(MenuID)
);

--Inserts

INSERT INTO UserRoles (Name, Description)
VALUES 
('Admin', 'Has full access to all restaurants and users'),
('Restaurant', 'Can manage their own restaurant information');

-- Users
INSERT INTO Users (FullName, Email, PasswordHash, RoleID)
VALUES
('System Admin', 'admin@example.com', 'hashed_admin_pass', 1),
('Soda Tica Owner', 'owner@sodatica.cr', 'hashed_pass_1', 2),
('La Parrilla Owner', 'owner@laparrilla.cr', 'hashed_pass_2', 2),
('Pizza Roma Owner', 'owner@pizzaroma.cr', 'hashed_pass_3', 2);

-- Restaurants
INSERT INTO Restaurants (UserID, Name, Description, Address, Phone, Email, Website, Rating, Latitude, Longitude)
VALUES
(2, 'Soda Tica', 'Typical Costa Rican food.', 'San José, Costa Rica', '2222-3333', 'info@sodatica.cr', 'www.sodatica.cr', 4.5, 9.9333, -84.0833),
(3, 'La Parrilla', 'Grill and BBQ specialists.', 'Heredia, Costa Rica', '2233-4444', 'info@laparrilla.cr', 'www.laparrilla.cr', 4.7, 9.9987, -84.1193),
(4, 'Pizza Roma', 'Authentic Italian pizzas.', 'Alajuela, Costa Rica', '2244-5555', 'info@pizzaroma.cr', 'www.pizzaroma.cr', 4.3, 10.0167, -84.2117);

-- Schedules
INSERT INTO Schedules (RestaurantID, DayOfWeek, OpenTime, CloseTime)
VALUES
(1, 1, '08:00', '22:00'),
(1, 2, '08:00', '22:00'),
(1, 7, '09:00', '20:00'),
(2, 1, '11:00', '23:00'),
(2, 7, '11:00', '23:00'),
(3, 1, '12:00', '22:30'),
(3, 5, '12:00', '23:30');

-- Menus
INSERT INTO Menus (RestaurantID, Name, Description)
VALUES
(1, 'Lunch Menu', 'Traditional Costa Rican lunch dishes'),
(1, 'Drinks', 'Fresh juices and beverages'),
(2, 'Main Menu', 'Steaks, ribs, and BBQ dishes'),
(3, 'Pizzas', 'Artisan pizzas baked in stone oven'),
(3, 'Desserts', 'Italian desserts and coffee');

-- Menu Items
INSERT INTO MenuItems (MenuID, Name, Description, Price, Category, ImageUrl)
VALUES
(1, 'Casado with Chicken', 'Rice, beans, plantain, salad, and grilled chicken', 4500, 'Main Dish', NULL),
(1, 'Gallo Pinto', 'Rice and beans with eggs and tortillas', 3000, 'Breakfast', NULL),
(2, 'Pineapple Juice', 'Fresh tropical pineapple juice', 1500, 'Drinks', NULL),
(3, 'BBQ Ribs', 'Smoked pork ribs with BBQ sauce', 8500, 'Main Dish', NULL),
(3, 'Grilled Steak', 'Beef steak with vegetables and fries', 8900, 'Main Dish', NULL),
(4, 'Margherita Pizza', 'Classic pizza with mozzarella and tomato', 7000, 'Pizza', NULL),
(4, 'Pepperoni Pizza', 'Spicy pepperoni slices with mozzarella', 7500, 'Pizza', NULL),
(5, 'Tiramisu', 'Classic Italian dessert with mascarpone and coffee', 4500, 'Dessert', NULL);
