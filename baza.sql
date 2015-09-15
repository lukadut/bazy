-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               5.6.17 - MySQL Community Server (GPL)
-- Serwer OS:                    Win64
-- HeidiSQL Wersja:              9.2.0.4960
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Zrzut struktury bazy danych projekt
CREATE DATABASE IF NOT EXISTS `projekt` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;
USE `projekt`;


-- Zrzut struktury tabela projekt.cargo
CREATE TABLE IF NOT EXISTS `cargo` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `Name` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa ładunku',
  `Type` enum('Container','Dump','Flatbed','Lowboy','Refrigerated','Tank') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Container' COMMENT 'Rodzaj ładunku, kolejno: kontener, wywrotka, platforma, laweta, chłodnia, cysterna',
  `ADR` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Potrzebna licencja ADR',
  `ADR_Class` set('1','2','3','4.1','4.2','4.3','5.1','5.2','6.1','6.2','7','8','9') COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Typy ADR',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cargo: ~13 rows (około)
/*!40000 ALTER TABLE `cargo` DISABLE KEYS */;
REPLACE INTO `cargo` (`Id`, `Name`, `Type`, `ADR`, `ADR_Class`, `Comment`) VALUES
	(1, 'Gruz', 'Dump', 'False', '', ''),
	(2, 'Żwir', 'Dump', 'False', '', ''),
	(3, 'Owoce', 'Container', 'False', '', ''),
	(4, 'Warzywa', 'Container', 'False', '', ''),
	(5, 'Zboże', 'Dump', 'False', '', ''),
	(6, 'Benzyna', 'Tank', 'True', '3', ''),
	(7, 'Diesel', 'Tank', 'True', '3', ''),
	(8, 'Fajerwerki', 'Container', 'True', '1', ''),
	(9, 'Rury stalowe', 'Flatbed', 'False', '', ''),
	(10, 'Samochody', 'Lowboy', 'False', '', ''),
	(11, 'Mięso', 'Refrigerated', 'False', '', ''),
	(12, 'Lody', 'Refrigerated', 'False', '', ''),
	(13, 'Walec drogowy', 'Lowboy', 'False', '', '');
/*!40000 ALTER TABLE `cargo` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.cars
CREATE TABLE IF NOT EXISTS `cars` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `Number_plate` char(10) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Numer rejestracji',
  `Make` char(40) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Marka',
  `Model` char(40) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Model',
  `Carry` smallint(5) unsigned NOT NULL COMMENT 'Dopuszczalna ładowność',
  `IsUsed` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Czy jest teraz używany',
  `Sold` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Czy zostało auto sprzedane',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Number plate` (`Number_plate`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cars: ~15 rows (około)
/*!40000 ALTER TABLE `cars` DISABLE KEYS */;
REPLACE INTO `cars` (`Id`, `Number_plate`, `Make`, `Model`, `Carry`, `IsUsed`, `Sold`, `Comment`) VALUES
	(1, 'SG 00001', 'Man', 'Tgx', 40000, 'True', 'False', ''),
	(2, 'SG 00002', 'Man', 'Tgx', 40000, 'False', 'False', ''),
	(3, 'SG 00003', 'Man', 'Tgx', 40000, 'False', 'False', ''),
	(4, 'SK 00001', 'Volvo', 'Fh16', 40000, 'False', 'False', ''),
	(5, 'SK 00002', 'Volvo', 'Fh16', 40000, 'True', 'False', ''),
	(6, 'SK 00003', 'Volvo', 'Fh16', 40000, 'True', 'False', ''),
	(7, 'SK 00004', 'Volvo', 'Fh16', 40000, 'True', 'False', ''),
	(8, 'SK 00005', 'Ford', 'Transit', 3499, 'False', 'False', ''),
	(9, 'SK 00006', 'Ford', 'Transit', 3499, 'False', 'False', ''),
	(10, 'SK 00007', 'Ford', 'Transit', 3499, 'False', 'False', ''),
	(11, 'SK 00008', 'Ford', 'Transit', 3499, 'False', 'False', ''),
	(12, 'SK 16546', 'Volvo', 'Fmx', 12000, 'False', 'False', ''),
	(13, 'SK 1698Q', 'Volvo', 'Fmx', 12000, 'True', 'False', ''),
	(14, 'SO 19843', 'Volvo', 'Fmx', 12000, 'False', 'False', 'Za granicą'),
	(15, 'SK 6564G', 'Volvo', 'Fmx', 12000, 'False', 'False', '');
/*!40000 ALTER TABLE `cars` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.cities_list
CREATE TABLE IF NOT EXISTS `cities_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `City` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa miasta',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `City name` (`City`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cities_list: ~8 rows (około)
/*!40000 ALTER TABLE `cities_list` DISABLE KEYS */;
REPLACE INTO `cities_list` (`Id`, `City`) VALUES
	(1, 'Gliwice'),
	(7, 'Katowice'),
	(3, 'Kostrzyn'),
	(5, 'Orlen'),
	(2, 'Płock'),
	(6, 'Ruda Śląska'),
	(8, 'Sieradz'),
	(4, 'Zabrze');
/*!40000 ALTER TABLE `cities_list` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.companies
CREATE TABLE IF NOT EXISTS `companies` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `CityId` smallint(5) unsigned NOT NULL COMMENT 'Id miasta',
  `CompanyId` smallint(5) unsigned NOT NULL COMMENT 'Id firmy',
  `Address` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Adres - ulica, numer',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `CityId` (`CityId`,`CompanyId`,`Address`),
  KEY `Company` (`CompanyId`),
  CONSTRAINT `City` FOREIGN KEY (`CityId`) REFERENCES `cities_list` (`Id`),
  CONSTRAINT `Company` FOREIGN KEY (`CompanyId`) REFERENCES `company_name_list` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.companies: ~10 rows (około)
/*!40000 ALTER TABLE `companies` DISABLE KEYS */;
REPLACE INTO `companies` (`Id`, `CityId`, `CompanyId`, `Address`, `Comment`) VALUES
	(1, 1, 1, 'Pszczyńska 44', ''),
	(2, 2, 2, 'Daleka 6', ''),
	(3, 3, 3, 'Żniwna 5', ''),
	(4, 4, 3, 'Wolności 232', ''),
	(5, 1, 3, 'Chorzowska 1', ''),
	(6, 6, 6, 'DTŚ 4', ''),
	(7, 1, 5, 'Dąbrowskiego 2', ''),
	(8, 1, 7, 'DK 88', ''),
	(9, 7, 7, 'Chorzowska 88', ''),
	(10, 8, 8, 'Łódzka 24', '');
/*!40000 ALTER TABLE `companies` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.company_name_list
CREATE TABLE IF NOT EXISTS `company_name_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Company` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa firmy',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Company name` (`Company`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.company_name_list: ~8 rows (około)
/*!40000 ALTER TABLE `company_name_list` DISABLE KEYS */;
REPLACE INTO `company_name_list` (`Id`, `Company`) VALUES
	(3, 'Biedronka'),
	(5, 'Carefur'),
	(1, 'Cpn'),
	(8, 'Masarnia'),
	(7, 'Opel'),
	(6, 'Orlen'),
	(2, 'Petrochemia'),
	(4, 'Ruda Śląska');
/*!40000 ALTER TABLE `company_name_list` ENABLE KEYS */;


-- Zrzut struktury funkcja projekt.DiffTime
DELIMITER //
CREATE DEFINER=`root`@`localhost` FUNCTION `DiffTime`(`id` SMALLINT) RETURNS int(11)
BEGIN
declare wynik int;
declare scheduledtime datetime;
declare arrivetime datetime;
	set scheduledtime = (select scheduledarrive from freights where freights.Id = id);
	set arrivetime = (select arrivetime from shipping where shipping.freightid = id);
	set wynik = timestampdiff(minute,arrivetime,schedulettime);
	return wynik;
END//
DELIMITER ;


-- Zrzut struktury tabela projekt.drivers
CREATE TABLE IF NOT EXISTS `drivers` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `Name` char(40) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Imię',
  `Surname` char(40) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwisko',
  `Wage` smallint(5) unsigned NOT NULL COMMENT 'Stawka',
  `ADR_License` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Posiada licencje na ładunki niebezpieczne',
  `Employed` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'True' COMMENT 'Czy zatrudniony',
  `Busy` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Czy w trasie',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.drivers: ~8 rows (około)
/*!40000 ALTER TABLE `drivers` DISABLE KEYS */;
REPLACE INTO `drivers` (`Id`, `Name`, `Surname`, `Wage`, `ADR_License`, `Employed`, `Busy`, `Comment`) VALUES
	(1, 'Mietek', 'Wiśniewski', 2500, 'False', 'True', 'True', ''),
	(2, 'Mirek', 'Wiśniewski', 2500, 'False', 'True', 'True', ''),
	(3, 'Tytus', 'Bomba', 7800, 'True', 'True', 'True', ''),
	(4, 'Mariusz', 'Admirał', 5600, 'True', 'True', 'True', ''),
	(5, 'Staszek', 'Bąk', 5600, 'True', 'True', 'True', ''),
	(6, 'Wacław', 'Głuś', 2000, 'False', 'True', 'False', ''),
	(7, 'Bogusław', 'Lee', 4000, 'False', 'True', 'False', ''),
	(8, 'Bogusław', 'Mocarz', 4000, 'False', 'True', 'False', '');
/*!40000 ALTER TABLE `drivers` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.freights
CREATE TABLE IF NOT EXISTS `freights` (
  `Id` smallint(6) unsigned NOT NULL AUTO_INCREMENT,
  `CargoId` smallint(6) unsigned NOT NULL COMMENT 'ID ładunku z listy',
  `From` smallint(6) unsigned NOT NULL COMMENT 'Id nadawcy',
  `To` smallint(6) unsigned NOT NULL COMMENT 'Id odbiorcy',
  `ScheduledArrive` datetime NOT NULL COMMENT 'Planowany czas przyjazdu',
  `Amount` tinyint(3) unsigned NOT NULL COMMENT 'Liczba zleceń',
  `Weight` smallint(5) unsigned NOT NULL COMMENT 'Waga ładunku',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  KEY `Freight` (`CargoId`),
  KEY `CityFrom` (`From`),
  KEY `CityTo` (`To`),
  CONSTRAINT `CityFrom` FOREIGN KEY (`From`) REFERENCES `companies` (`Id`),
  CONSTRAINT `CityTo` FOREIGN KEY (`To`) REFERENCES `companies` (`Id`),
  CONSTRAINT `Freight` FOREIGN KEY (`CargoId`) REFERENCES `cargo` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.freights: ~7 rows (około)
/*!40000 ALTER TABLE `freights` DISABLE KEYS */;
REPLACE INTO `freights` (`Id`, `CargoId`, `From`, `To`, `ScheduledArrive`, `Amount`, `Weight`, `Comment`) VALUES
	(1, 7, 2, 6, '2015-09-16 06:00:00', 0, 38000, ''),
	(2, 7, 2, 1, '2015-09-16 06:00:00', 0, 38000, ''),
	(3, 10, 8, 9, '2015-09-16 09:00:00', 3, 38000, ''),
	(4, 11, 10, 7, '2015-09-13 05:00:00', 1, 38000, ''),
	(5, 11, 10, 3, '2015-09-13 02:00:00', 5, 38000, ''),
	(6, 4, 7, 5, '2015-09-15 06:30:00', 0, 2000, ''),
	(7, 8, 3, 10, '2015-04-01 07:30:00', 0, 11000, '');
/*!40000 ALTER TABLE `freights` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.shipping
CREATE TABLE IF NOT EXISTS `shipping` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `DriverId` smallint(5) unsigned NOT NULL COMMENT 'Id kierowcy',
  `CarId` smallint(5) unsigned NOT NULL COMMENT 'Id pojazdu',
  `FreightId` smallint(5) unsigned NOT NULL COMMENT 'Id zlecenia',
  `DepartTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Czas odjazdu',
  `ArriveTime` datetime DEFAULT NULL COMMENT 'Czas przyjazdu',
  `Delivered` enum('On time','Delayed','Not yet') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Not yet' COMMENT 'Dostarczono',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  KEY `Driver` (`DriverId`),
  KEY `Car` (`CarId`),
  KEY `Freigth` (`FreightId`),
  CONSTRAINT `Car` FOREIGN KEY (`CarId`) REFERENCES `cars` (`Id`),
  CONSTRAINT `Driver` FOREIGN KEY (`DriverId`) REFERENCES `drivers` (`Id`),
  CONSTRAINT `Freigth` FOREIGN KEY (`FreightId`) REFERENCES `freights` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.shipping: ~6 rows (około)
/*!40000 ALTER TABLE `shipping` DISABLE KEYS */;
REPLACE INTO `shipping` (`Id`, `DriverId`, `CarId`, `FreightId`, `DepartTime`, `ArriveTime`, `Delivered`, `Comment`) VALUES
	(1, 3, 1, 1, '2015-09-15 19:08:06', NULL, 'Not yet', ''),
	(2, 5, 6, 2, '2015-09-15 19:08:35', NULL, 'Not yet', ''),
	(3, 4, 13, 7, '2015-09-15 19:08:53', NULL, 'Not yet', ''),
	(4, 2, 5, 3, '2015-09-15 19:09:03', NULL, 'Not yet', ''),
	(5, 1, 7, 3, '2015-09-15 19:09:12', NULL, 'Not yet', ''),
	(6, 7, 10, 6, '2015-09-15 19:09:23', '2015-09-15 19:09:34', 'Delayed', '');
/*!40000 ALTER TABLE `shipping` ENABLE KEYS */;


-- Zrzut struktury wyzwalacz projekt.shipping_before_insert
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='';
DELIMITER //
CREATE TRIGGER `shipping_before_insert` BEFORE INSERT ON `shipping` FOR EACH ROW BEGIN
	set new.DepartTime = now();
	Update drivers set `busy` = 'true' where drivers.Id = new.DriverId;
	UPDATE cars SET `IsUsed`='true' WHERE cars.Id = new.CarId;
	Update freights set `amount` = `amount`-1 where freights.Id = new.FreightId;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;


-- Zrzut struktury wyzwalacz projekt.shipping_before_update
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `shipping_before_update` BEFORE UPDATE ON `shipping` FOR EACH ROW begin
declare scheduledtime datetime;
set scheduledtime = (select scheduledarrive from freights where freights.Id = new.FreightId);

	if timestampdiff(minute,scheduledtime,new.arrivetime) < 15 then
		Update drivers set `busy` = 'false' where drivers.Id = old.DriverId;
		UPDATE cars SET `IsUsed`='false' WHERE cars.Id = old.CarId;
		set new.Delivered = 'On time';
	end if;
	
	
	if timestampdiff(minute,scheduledtime,new.arrivetime) >= 15 then
		Update drivers set `busy` = 'false' where drivers.Id = old.DriverId;
		UPDATE cars SET `IsUsed`='false' WHERE cars.Id = old.CarId;
		set new.Delivered = 'Delayed';
	end if;
	
	
	if new.ArriveTime is null then
		set new.delivered = 'not yet';
	end if;
end//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
