-- phpMyAdmin SQL Dump
-- version 2.11.11.3
-- http://www.phpmyadmin.net
--
-- Хост: localhost
-- Время создания: Фев 15 2012 г., 11:54
-- Версия сервера: 5.1.57
-- Версия PHP: 5.2.17

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- База данных: `zenitdb`
--

-- --------------------------------------------------------

--
-- Структура таблицы `cashiers`
--

DROP TABLE IF EXISTS `cashiers`;
CREATE TABLE IF NOT EXISTS `cashiers` (
  `ID` int(11) NOT NULL,
  `Surname` char(50) NOT NULL,
  `Name` char(50) NOT NULL,
  `patronymic` char(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Surname` (`Surname`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 COMMENT='Кассиры';

--
-- Дамп данных таблицы `cashiers`
--

INSERT INTO `cashiers` (`ID`, `Surname`, `Name`, `patronymic`) VALUES
(1, 'Крис', 'Ольга', 'Васильевна'),
(2, 'Макарова', 'Надежда', 'Петровна');

-- --------------------------------------------------------

--
-- Структура таблицы `clients`
--

DROP TABLE IF EXISTS `clients`;
CREATE TABLE IF NOT EXISTS `clients` (
  `ID` int(11) NOT NULL,
  `Surname` char(50) NOT NULL,
  `Name` char(50) NOT NULL,
  `patronymic` char(50) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Surname` (`Surname`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `clients`
--

INSERT INTO `clients` (`ID`, `Surname`, `Name`, `patronymic`) VALUES
(1, 'Иванов', 'Кирилл', 'Андреевич'),
(2, 'Борман', 'Максим', 'Игоревич');

-- --------------------------------------------------------

--
-- Структура таблицы `orders`
--

DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `ID` int(11) NOT NULL,
  `clientID` int(11) NOT NULL,
  `productID` int(11) NOT NULL,
  `cashierID` int(11) NOT NULL,
  `orderDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `clientID` (`clientID`,`productID`,`cashierID`,`orderDate`),
  KEY `productID` (`productID`),
  KEY `cashierID` (`cashierID`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `orders`
--

INSERT INTO `orders` (`ID`, `clientID`, `productID`, `cashierID`, `orderDate`) VALUES
(1, 1, 1, 1, '2012-02-01 00:59:18'),
(3, 1, 3, 1, '2012-02-14 00:59:46'),
(2, 2, 2, 2, '2012-02-02 00:59:26'),
(4, 2, 4, 2, '2012-02-15 01:00:00');

-- --------------------------------------------------------

--
-- Структура таблицы `products`
--

DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `ID` int(11) NOT NULL,
  `product` char(50) NOT NULL COMMENT 'товар',
  `price` float NOT NULL COMMENT 'цена',
  PRIMARY KEY (`ID`),
  KEY `product` (`product`)
) ENGINE=InnoDB DEFAULT CHARSET=cp1251;

--
-- Дамп данных таблицы `products`
--

INSERT INTO `products` (`ID`, `product`, `price`) VALUES
(1, 'часы Siemens S50', 1500),
(2, 'Телевизор SONY BRAVIA KD32', 26489),
(3, 'Веб-камера Genius SLIM 32C', 890),
(4, 'Монитор Samsung S234', 5660);

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_4` FOREIGN KEY (`clientID`) REFERENCES `clients` (`ID`),
  ADD CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`productID`) REFERENCES `products` (`ID`),
  ADD CONSTRAINT `orders_ibfk_3` FOREIGN KEY (`cashierID`) REFERENCES `cashiers` (`ID`);

DELIMITER $$
--
-- Процедуры
--
DROP PROCEDURE IF EXISTS `getOrders`$$
$$

DELIMITER ;
