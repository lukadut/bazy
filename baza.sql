-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Wersja serwera:               5.5.21-log - MySQL Community Server (GPL)
-- Serwer OS:                    Win32
-- HeidiSQL Wersja:              9.1.0.4867
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Zrzut struktury bazy danych projekt
CREATE DATABASE IF NOT EXISTS `projekt` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci */;
USE `projekt`;


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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cars: ~5 rows (około)
DELETE FROM `cars`;
/*!40000 ALTER TABLE `cars` DISABLE KEYS */;
INSERT INTO `cars` (`Id`, `Number_plate`, `Make`, `Model`, `Carry`, `IsUsed`, `Sold`, `Comment`) VALUES
	(1, 'fssdgsdg', 'fgdf', 'dfgfdg', 65535, 'False', 'False', ''),
	(2, 'resa3', 'marka', 'model', 5, 'False', 'False', ''),
	(4, 'aa', 'dasdsad', 'asdas', 0, 'True', 'False', ''),
	(5, 'aa .', 'dddd', 'asdas', 0, 'False', 'False', ''),
	(6, 'aa1', 'asds', 'asdsad', 4, 'False', 'False', '');
/*!40000 ALTER TABLE `cars` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.cities_list
CREATE TABLE IF NOT EXISTS `cities_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `City` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa miasta',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `City name` (`City`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cities_list: ~2 rows (około)
DELETE FROM `cities_list`;
/*!40000 ALTER TABLE `cities_list` DISABLE KEYS */;
INSERT INTO `cities_list` (`Id`, `City`) VALUES
	(1, 'las vegas'),
	(2, 'majami');
/*!40000 ALTER TABLE `cities_list` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.companies
CREATE TABLE IF NOT EXISTS `companies` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `CityId` smallint(5) unsigned NOT NULL COMMENT 'Id miasta',
  `CompanyId` smallint(5) unsigned NOT NULL COMMENT 'Id firmy',
  `Address` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Adres - ulica, numer',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  KEY `City` (`CityId`),
  KEY `Company` (`CompanyId`),
  CONSTRAINT `City` FOREIGN KEY (`CityId`) REFERENCES `cities_list` (`Id`),
  CONSTRAINT `Company` FOREIGN KEY (`CompanyId`) REFERENCES `company_name_list` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.companies: ~2 rows (około)
DELETE FROM `companies`;
/*!40000 ALTER TABLE `companies` DISABLE KEYS */;
INSERT INTO `companies` (`Id`, `CityId`, `CompanyId`, `Address`, `Comment`) VALUES
	(1, 1, 1, 'holiłud', ''),
	(2, 2, 1, 'bicz', '');
/*!40000 ALTER TABLE `companies` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.company_name_list
CREATE TABLE IF NOT EXISTS `company_name_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Company` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa firmy',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Company name` (`Company`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.company_name_list: ~1 rows (około)
DELETE FROM `company_name_list`;
/*!40000 ALTER TABLE `company_name_list` DISABLE KEYS */;
INSERT INTO `company_name_list` (`Id`, `Company`) VALUES
	(1, 'snoop dogg');
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
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.drivers: ~17 rows (około)
DELETE FROM `drivers`;
/*!40000 ALTER TABLE `drivers` DISABLE KEYS */;
INSERT INTO `drivers` (`Id`, `Name`, `Surname`, `Wage`, `ADR_License`, `Employed`, `Busy`, `Comment`) VALUES
	(1, 'Jan', 'Ja', 12345, 'True', 'True', 'False', 'sample text sample text \r\n\r\nsample text sample text sample text sample text '),
	(2, 'jasoi', 'qwertyza', 999, 'True', 'True', 'True', ''),
	(3, 'kierowca', '4', 1000, 'True', 'False', 'True', ''),
	(4, 'kierowca', '4', 1000, 'True', 'False', 'True', ''),
	(5, 'kierowca', '5', 1001, 'True', 'True', 'True', ''),
	(6, 'test numer 6', 'test', 1, 'False', 'True', 'False', ''),
	(8, 'test numer 8', 'test2', 1500, 'True', 'True', 'False', ''),
	(9, 'test', 'test2', 1500, 'True', 'True', 'False', ''),
	(10, 'test 10', 'test2', 1500, 'True', 'True', 'False', ''),
	(11, 'test', 'test2', 1500, 'True', 'True', 'False', ''),
	(12, 'test 12 a', 'test2', 1500, 'True', 'True', 'False', ''),
	(13, 'abcd', 'def', 3334, 'False', 'False', 'False', ''),
	(14, '1234567890123456789012345678901234567890', '1234567890123456789012345678901234567890', 1, 'False', 'False', 'True', ''),
	(15, '1234567890123456789012345678901234567890', '1234567890123456789012345678901234567890', 2, 'False', 'True', 'False', ''),
	(16, '1234567890123456789012345678901234567890', '1234567890123456789012345678901234567890', 1, 'False', 'True', 'False', ''),
	(17, 'sample', 'texte', 2, 'True', 'True', 'False', ''),
	(18, 'test', 'dwa', 0, 'True', 'True', 'True', '');
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.freights: ~1 rows (około)
DELETE FROM `freights`;
/*!40000 ALTER TABLE `freights` DISABLE KEYS */;
INSERT INTO `freights` (`Id`, `CargoId`, `From`, `To`, `ScheduledArrive`, `Amount`, `Weight`, `Comment`) VALUES
	(1, 1, 1, 2, '2015-07-08 14:52:13', 254, 1, '');
/*!40000 ALTER TABLE `freights` ENABLE KEYS */;


-- Zrzut struktury tabela projekt.cargo
CREATE TABLE IF NOT EXISTS `cargo` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `Name` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa ładunku',
  `Type` enum('Container','Dump','Flatbed','Lowboy','Refrigerated','Tank') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'Container' COMMENT 'Rodzaj ładunku, kolejno: kontener, wywrotka, platforma, laweta, chłodnia, cysterna',
  `ADR` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Potrzebna licencja ADR',
  `ADR_Class` set('1','2','3','4.1','4.2','4.3','5.1','5.2','6.1','6.2','7','8','9') COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Typy ADR',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.cargo: ~8 rows (około)
DELETE FROM `cargo`;
/*!40000 ALTER TABLE `cargo` DISABLE KEYS */;
INSERT INTO `cargo` (`Id`, `Name`, `Type`, `ADR`, `ADR_Class`, `Comment`) VALUES
	(1, 'weed', 'Tank', 'True', '4.1', 'marihunanen w szczykawkach'),
	(2, 'nazwa', 'Lowboy', 'True', '2,4.2,4.3', ''),
	(3, 'dziwne rzeczy', 'Dump', 'True', '2,9', 'hava nagila'),
	(4, 'mleko', 'Refrigerated', 'False', '', 'hava nagila'),
	(5, 'dzem', 'Flatbed', 'False', '', ''),
	(6, 'aló€esczz', 'Dump', 'False', '', ''),
	(7, 'ę?óąśłżźć', 'Lowboy', 'True', '2,4.2,4.3', ''),
	(8, 'ę€óąśłżźćń', 'Dump', 'True', '2', 'wywrotka wypełniona niebezpiecznym O2');
/*!40000 ALTER TABLE `cargo` ENABLE KEYS */;


-- Zrzut struktury widok projekt.hired_drivers
-- Tworzenie tymczasowej tabeli aby przezwyciężyć błędy z zależnościami w WIDOKU
CREATE TABLE `hired_drivers` (
	`name` CHAR(40) NOT NULL COMMENT 'Imię' COLLATE 'utf8_unicode_ci',
	`surname` CHAR(40) NOT NULL COMMENT 'Nazwisko' COLLATE 'utf8_unicode_ci',
	`adr_license` ENUM('True','False') NOT NULL COMMENT 'Posiada licencje na ładunki niebezpieczne' COLLATE 'utf8_unicode_ci'
) ENGINE=MyISAM;


-- Zrzut struktury tabela projekt.shipping
CREATE TABLE IF NOT EXISTS `shipping` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `DriverId` smallint(5) unsigned NOT NULL COMMENT 'Id kierowcy',
  `CarId` smallint(5) unsigned NOT NULL COMMENT 'Id pojazdu',
  `FreightId` smallint(5) unsigned NOT NULL COMMENT 'Id zlecenia',
  `DepartTime` datetime NOT NULL COMMENT 'Czas odjazdu',
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Zrzucanie danych dla tabeli projekt.shipping: ~9 rows (około)
DELETE FROM `shipping`;
/*!40000 ALTER TABLE `shipping` DISABLE KEYS */;
INSERT INTO `shipping` (`Id`, `DriverId`, `CarId`, `FreightId`, `DepartTime`, `ArriveTime`, `Delivered`, `Comment`) VALUES
	(1, 1, 1, 1, '2015-07-06 18:10:13', '2015-07-07 11:08:35', 'On time', ''),
	(3, 16, 6, 1, '2015-07-07 09:12:43', '2015-07-07 11:09:36', 'On time', ''),
	(5, 15, 4, 1, '2015-07-07 09:20:55', '2015-07-06 14:10:40', 'On time', ''),
	(6, 15, 5, 1, '2015-07-07 11:20:54', '2015-07-07 11:31:44', 'On time', ''),
	(7, 14, 2, 1, '2015-07-07 11:22:00', '2015-07-09 11:49:00', 'Delayed', ''),
	(8, 14, 5, 1, '2015-07-08 14:49:12', '2015-07-08 12:20:02', 'Delayed', ''),
	(9, 8, 1, 1, '2015-07-08 14:58:30', '2015-07-08 15:01:57', 'On time', ''),
	(10, 6, 5, 1, '2015-07-08 15:00:25', '2015-07-08 15:01:49', 'On time', ''),
	(11, 14, 4, 1, '2015-07-08 15:07:28', NULL, 'Not yet', '');
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
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='';
DELIMITER //
CREATE TRIGGER `shipping_before_update` BEFORE UPDATE ON `shipping` FOR EACH ROW begin
declare scheduledtime datetime;
set scheduledtime = (select scheduledarrive from freights where freights.Id = id);

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


-- Zrzut struktury widok projekt.hired_drivers
-- Usuwanie tabeli tymczasowej i tworzenie ostatecznej struktury WIDOKU
DROP TABLE IF EXISTS `hired_drivers`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` VIEW `hired_drivers` AS select name,surname,adr_license from drivers
where employed = 'true' ;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
