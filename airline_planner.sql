-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Dec 11, 2018 at 12:48 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `airline_planner`
--
CREATE DATABASE IF NOT EXISTS `airline_planner` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `airline_planner`;

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `id` int(11) NOT NULL,
  `city` varchar(255) NOT NULL,
  `state` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cities`
--

INSERT INTO `cities` (`id`, `city`, `state`) VALUES
(1, 'Seattle', 'WA'),
(2, 'Austin', 'TX'),
(3, 'New York', 'NY'),
(4, 'Suquamish', 'WA'),
(5, 'Denver', 'CO'),
(6, 'Chicago', 'IL'),
(7, 'Honolulu', 'HI');

-- --------------------------------------------------------

--
-- Table structure for table `cities_flights`
--

CREATE TABLE `cities_flights` (
  `id` int(11) NOT NULL,
  `city_one_id` int(11) NOT NULL,
  `city_two_id` int(11) NOT NULL,
  `flight_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `cities_flights`
--

INSERT INTO `cities_flights` (`id`, `city_one_id`, `city_two_id`, `flight_id`) VALUES
(1, 1, 2, 1),
(2, 4, 1, 1),
(3, 1, 4, 1),
(4, 1, 3, 1),
(5, 6, 1, 1),
(6, 1, 7, 1);

-- --------------------------------------------------------

--
-- Table structure for table `flights`
--

CREATE TABLE `flights` (
  `id` int(11) NOT NULL,
  `time` datetime NOT NULL,
  `status` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `flights`
--

INSERT INTO `flights` (`id`, `time`, `status`) VALUES
(1, '2018-12-04 01:50:00', 'On Time'),
(2, '2019-01-30 19:30:00', 'Delayed'),
(3, '2019-04-07 01:05:00', 'Complete'),
(4, '2019-07-31 04:22:00', 'Delayed'),
(5, '2018-01-06 02:02:00', 'Delayed'),
(6, '2019-06-14 02:02:00', 'On Time');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cities_flights`
--
ALTER TABLE `cities_flights`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `flights`
--
ALTER TABLE `flights`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;
--
-- AUTO_INCREMENT for table `cities_flights`
--
ALTER TABLE `cities_flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `flights`
--
ALTER TABLE `flights`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
