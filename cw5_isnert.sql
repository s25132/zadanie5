INSERT INTO trip.Client (IdClient, FirstName, LastName, Email, Telephone, Pesel)
VALUES
    (1, 'John', 'Doe', 'john.doe@example.com', '+123456789', '12345678901'),
    (2, 'Jane', 'Smith', 'jane.smith@example.com', '+987654321', '98765432109'),
    (3, 'Michael', 'Johnson', 'michael.johnson@example.com', '+111222333', '11122233311');

INSERT INTO trip.Trip (IdTrip, Name, Description, DateFrom, DateTo, MaxPeople)
VALUES
    (1, 'Summer Adventure in Italy', 'Explore the beautiful landscapes of Italy in summer.', '2024-07-01', '2024-07-10', 20),
    (2, 'Skiing in the Alps', 'Enjoy skiing in the majestic Alps mountains.', '2024-12-15', '2024-12-25', 15),
    (3, 'Safari in Africa', 'Experience the wildlife in African savannas.', '2025-03-01', '2025-03-10', 10);

INSERT INTO trip.Country (IdCountry, Name)
VALUES
    (1, 'Italy'),
    (2, 'France'),
    (3, 'Kenya');

INSERT INTO trip.Client_Trip (IdClient, IdTrip, RegisteredAt, PaymentDate)
VALUES
    (1, 1, '2024-05-01', NULL),
    (2, 1, '2024-05-05', '2024-05-06'),
    (3, 3, '2024-04-20', NULL);

INSERT INTO trip.Country_Trip (IdCountry, IdTrip)
VALUES
    (1, 1),
    (1, 2),
    (3, 3);
