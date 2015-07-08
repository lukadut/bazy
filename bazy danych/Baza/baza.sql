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
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Number plate` (`Number_plate`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury tabela projekt.cities_list
CREATE TABLE IF NOT EXISTS `cities_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `City` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa miasta',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury tabela projekt.companies
CREATE TABLE IF NOT EXISTS `companies` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `CityId` smallint(5) unsigned NOT NULL COMMENT 'Id miasta',
  `CompanyId` smallint(5) unsigned NOT NULL COMMENT 'Id firmy',
  `Addres` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Adres - ulica, numer',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  KEY `City` (`CityId`),
  KEY `Company` (`CompanyId`),
  CONSTRAINT `City` FOREIGN KEY (`CityId`) REFERENCES `cities_list` (`Id`),
  CONSTRAINT `Company` FOREIGN KEY (`CompanyId`) REFERENCES `company_name_list` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury tabela projekt.company_name_list
CREATE TABLE IF NOT EXISTS `company_name_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `Company` char(50) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa firmy',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury tabela projekt.freights
CREATE TABLE IF NOT EXISTS `freights` (
  `Id` smallint(6) unsigned NOT NULL AUTO_INCREMENT,
  `FreightId` smallint(6) unsigned NOT NULL COMMENT 'ID ładunku z listy',
  `From` smallint(6) unsigned NOT NULL COMMENT 'Id nadawcy',
  `To` smallint(6) unsigned NOT NULL COMMENT 'Id odbiorcy',
  `ScheduledArrive` datetime NOT NULL COMMENT 'Planowany czas przyjazdu',
  `Amount` tinyint(3) unsigned NOT NULL COMMENT 'Liczba zleceń',
  `Weight` smallint(5) unsigned NOT NULL COMMENT 'Waga ładunku',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`),
  KEY `Freight` (`FreightId`),
  KEY `CityFrom` (`From`),
  KEY `CityTo` (`To`),
  CONSTRAINT `CityFrom` FOREIGN KEY (`From`) REFERENCES `companies` (`Id`),
  CONSTRAINT `CityTo` FOREIGN KEY (`To`) REFERENCES `companies` (`Id`),
  CONSTRAINT `Freight` FOREIGN KEY (`FreightId`) REFERENCES `freights_list` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury tabela projekt.freights_list
CREATE TABLE IF NOT EXISTS `freights_list` (
  `Id` smallint(5) unsigned NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `Name` char(30) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nazwa ładunku',
  `Type` enum('Container','Dump','Flatbed','Lowboy','Refrigerated','Tank') COLLATE utf8_unicode_ci NOT NULL COMMENT 'Rodzaj ładunku, kolejno: kontener, wywrotka, platforma, laweta, chłodnia, cysterna',
  `ADR` enum('True','False') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'False' COMMENT 'Potrzebna licencja ADR',
  `ADR_Class` set('1','2','3','4.1','4.2','4.3','5.1','5.2','6.1','6.2','7','8','9') COLLATE utf8_unicode_ci NOT NULL COMMENT 'Typy ADR',
  `Comment` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT 'Komentarz',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


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
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- Data exporting was unselected.


-- Zrzut struktury wyzwalacz projekt.Przydziel
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='';
DELIMITER //
CREATE TRIGGER `Przydziel` BEFORE INSERT ON `shipping` FOR EACH ROW BEGIN
	set new.DepartTime = now();
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
		set new.Delivered = 'On time';
	end if;
	if timestampdiff(minute,scheduledtime,new.arrivetime) >= 15 then
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
