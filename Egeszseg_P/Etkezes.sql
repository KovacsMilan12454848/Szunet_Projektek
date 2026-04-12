
CREATE Database Etrend;
Use  Etrend; 

CREATE TABLE Szemelyek (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nev VARCHAR(100) NOT NULL,
    kor INT NOT NULL,
    nem VARCHAR(10)  NOT NULL,   
    vegan BOOLEAN DEFAULT 0,
    suly FLOAT NOT NULL,   
    magassag FLOAT NOT NULL,   
    mozgas VARCHAR(20)  NOT NULL  
    
);


CREATE TABLE Etelek (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nev VARCHAR(100)  NOT NULL,
    kaloria FLOAT NOT NULL,   
    ar DECIMAL(10,2)  NOT NULL,   
    vegan BOOLEAN DEFAULT 0         
    
);

CREATE TABLE Italok (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nev VARCHAR(100)  NOT NULL,
    kaloria FLOAT NOT NULL, 
    ar DECIMAL(10,2)  NOT NULL,    
    vegan BOOLEAN DEFAULT 0
);



CREATE TABLE Desszertek (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nev VARCHAR(100) NOT NULL,
    kaloria FLOAT NOT NULL,   
    ar DECIMAL(10,2)  NOT NULL,
    vegan BOOLEAN DEFAULT 0   
);

CREATE TABLE Etkezesek (
    id INT AUTO_INCREMENT PRIMARY KEY,
    szemely_id INT,
    etel_id  INT,
    ital_id  INT,
    desszert_id  INT,
    nap DATE NOT NULL,   
    napszak VARCHAR(20) NOT NULL,    
    FOREIGN KEY (szemely_id) REFERENCES Szemelyek(id),  
    FOREIGN KEY (etel_id) REFERENCES Etelek(id) ,
    FOREIGN KEY (ital_id) REFERENCES Italok(id),
    FOREIGN KEY (desszert_id) REFERENCES Desszertek(id) 
);


INSERT INTO Szemelyek (nev, kor, nem, vegan, suly, magassag, mozgas) VALUES
('Kovács Péter', 32, 'férfi', 0, 85.5, 182, 'sportol'),
('Nagy Anna', 25, 'nő', 1, 62.0, 168, 'normal'),
('Szabó Gábor', 45, 'férfi', 0, 95.0, 175, 'keves'),
('Tóth Lilla', 28, 'nő', 0, 58.5, 165, 'sportol'),
('Kiss Zoltán', 38, 'férfi', 1, 75.0, 180, 'normal');

INSERT INTO Etelek (nev, kaloria, ar, vegan) VALUES
('Nem kért ételt', 0.0, 0.00, 1),
('Rántott csirkemell rizzsel', 650.0, 2500.00, 0),
('Vegán lencsefőzelék feltéttel', 450.0, 1800.00, 1),
('Marhapörkölt nokedlivel', 820.0, 3200.00, 0),
('Tofus Pad Thai', 550.0, 2600.00, 1),
('Cézár saláta csirkemellel', 380.0, 2200.00, 0);

INSERT INTO Italok (nev, kaloria, ar, vegan) VALUES
('Nem kért italt', 0.0, 0.00, 1),
('Szénsavmentes ásványvíz', 0.0, 400.00, 1),
('Coca-Cola (0.33l)', 139.0, 600.00, 1),
('100%-os Narancslé', 112.0, 750.00, 1),
('Kézműves IPA Sör', 210.0, 1200.00, 1),
('Tejeskávé (Latte)', 120.0, 850.00, 0);

INSERT INTO Desszertek (nev, kaloria, ar, vegan) VALUES
('Nem kért desszertet', 0.0, 0.00, 1),
('Somlói galuska', 480.0, 1500.00, 0),
('Vegán csokis brownie', 350.0, 1400.00, 1),
('Túrós palacsinta (2 db)', 320.0, 1100.00, 0),
('Friss gyümölcssaláta', 150.0, 1200.00, 1),
('Tiramisu', 410.0, 1600.00, 0);


INSERT INTO Etkezesek (szemely_id, etel_id, ital_id, desszert_id, nap, napszak) VALUES
(1, 1, 6, 4, '2026-04-08', 'reggeli'),  
(1, 2, 3, 2, '2026-04-08', 'ebed'),     
(1, 4, 5, 1, '2026-04-08', 'vacsora'),  

(2, 1, 4, 5, '2026-04-08', 'reggeli'),  
(2, 5, 2, 3, '2026-04-08', 'ebed'),      
(2, 3, 2, 1, '2026-04-08', 'vacsora'),   

(3, 4, 3, 2, '2026-04-08', 'ebed'),      
(3, 2, 5, 1, '2026-04-08', 'vacsora'),   

(4, 1, 6, 1, '2026-04-08', 'reggeli'),  
(4, 6, 2, 5, '2026-04-08', 'ebed'),     
(4, 2, 2, 1, '2026-04-08', 'vacsora'),  

(5, 3, 4, 1, '2026-04-08', 'ebed'),     
(5, 5, 5, 3, '2026-04-08', 'vacsora'),

(1, 1, 3, 1, '2026-04-09', 'reggeli'),  
(1, 6, 6, 5, '2026-04-09', 'ebed'),     
(1, 2, 2, 1, '2026-04-09', 'vacsora');
