CREATE TABLE Restaurant (
    id 				SERIAL PRIMARY KEY,
    nom 			VARCHAR(255) NOT NULL,
    description 	TEXT,
    adresse 		VARCHAR(255),
    code_postal 	CHAR(5),
    ville 			VARCHAR(100),
    pays 			VARCHAR(100)
);

CREATE TABLE Plat (
    id 				SERIAL PRIMARY KEY,
    titre 			VARCHAR(255) NOT NULL,
    description 	TEXT,
    prix 			DECIMAL(10, 2) NOT NULL,
	restaurant_id 	INT,
	  
	CONSTRAINT fk_restaurant
	  FOREIGN KEY (restaurant_id)
	  REFERENCES Restaurant (id)
	  ON DELETE CASCADE
);

CREATE TABLE Image (
    id 				SERIAL PRIMARY KEY,
    url 			VARCHAR(255) NOT NULL,
    description 	TEXT,
    plat_id 		INT,
		
    CONSTRAINT fk_plat
      FOREIGN KEY(plat_id) 
      REFERENCES Plat(id)
      ON DELETE SET NULL
);

INSERT INTO Restaurant (nom, description, adresse, code_postal, ville, pays)
VALUES 
	('Le Gourmet', 'Restaurant chic avec une cuisine raffinée.', '12 Rue de la Paix', '75001', 'Paris', 'France'),
	('La Table du Chef', 'Cuisine traditionnelle revisitée avec des produits locaux.', '25 Avenue des Fleurs', '06000', 'Nice', 'France'),
	('El Mexicano', 'Ambiance festive avec des spécialités mexicaines.', '8 Calle de la Luz', '28004', 'Madrid', 'Espagne');

INSERT INTO Plat (titre, description, prix, restaurant_id)
VALUES 
	('Pizza Margherita', 'Pizza classique avec sauce tomate, mozzarella, et basilic.', 12.99, 1),
	('Risotto aux champignons', 'Risotto crémeux avec des champignons frais.', 18.50, 1),
	('Tacos au poulet', 'Tacos croustillants avec du poulet grillé, salsa et fromage.', 9.99, 3),
	('Salade César', 'Salade romaine avec poulet grillé, croutons, et parmesan.', 11.99, 2),
	('Boeuf bourguignon', 'Plat traditionnel français avec du boeuf mijoté dans du vin rouge.', 22.00, 2),
	('Fajitas de boeuf', 'Fajitas servies avec du boeuf grillé, poivrons et oignons.', 16.50, 3),
	('Burger au cheddar', 'Burger avec du boeuf, du cheddar, laitue, et tomates.', 14.00, 1),
	('Paella Valenciana', 'Plat espagnol traditionnel avec du riz, des fruits de mer et du poulet.', 19.50, 3),
	('Tagliatelles au saumon', 'Pâtes fraîches avec une sauce crémeuse au saumon fumé.', 17.00, 1),
	('Soupe à l oignon', 'Soupe traditionnelle à base d oignons caramélisés et gratinée au fromage.', 8.99, 2);

INSERT INTO Image (url, description, plat_id)
VALUES 
	('https://example.com/images/pizza-margherita.jpg', null, 1),
	('https://example.com/images/risotto-champignons.jpg', 'Un risotto crémeux aux champignons avec du parmesan.', 1),
	('https://example.com/images/tacos-poulet.jpg', 'Tacos croustillants remplis de poulet grillé et salsa.', 3),
	('https://example.com/images/salade-cesar.jpg', 'Salade César avec du poulet grillé, croutons et parmesan.', 4),
	('https://example.com/images/boeuf-bourguignon.jpg', 'Boeuf bourguignon mijoté avec des légumes et du vin rouge.', 5),
	('https://example.com/images/fajitas-boeuf.jpg', null, 6),
	('https://example.com/images/burger-cheddar.jpg', 'Burger au cheddar avec du boeuf, laitue, et tomates.', 7),
	('https://example.com/images/paella-valenciana.jpg', 'Paella valencienne avec fruits de mer, poulet, et riz épicé.', 8),
	('https://example.com/images/tagliatelles-saumon.jpg', 'Tagliatelles fraîches avec une sauce au saumon fumé.', 9),
	('https://example.com/images/soupe-oignon.jpg', null, 10);



